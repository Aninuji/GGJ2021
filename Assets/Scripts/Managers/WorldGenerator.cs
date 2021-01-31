using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Cinemachine;
using Random = UnityEngine.Random;         //Tells Random to use the Unity Engine random number generator.

public class WorldGenerator : MonoBehaviour
{
    public static WorldGenerator Instance;


    // Using Serializable allows us to embed a class with sub properties in the inspector.
    [Serializable]
    public class Count
    {
        public int minimum;             //Minimum value for our Count class.
        public int maximum;             //Maximum value for our Count class.


        //Assignment constructor.
        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    [Range(0, 1)]
    public float normalTileThreshold = 0.5f;
    public float offset = 1f;

    public float scale = 2;
    public int columns = 8;                                         //Number of columns in our game board.
    public int rows = 8;                                            //Number of rows in our game board.
    public Count wallCount = new Count(5, 9);                        //Lower and upper limit for our random number of walls per level.
    public Count propCount = new Count(1, 5);                        //Lower and upper limit for our random number of prop items per level.

    public Count treasureCount = new Count(1, 5);                        //Lower and upper limit for our random number of treasure items per level.

    public CinemachineVirtualCamera cam;
    public GameObject player;

    [HideInInspector]
    public GameObject _playerInstance;

    public GameObject exit;                                            //Prefab to spawn for exit.
    public GameObject[] floorTiles;                                    //Array of floor prefabs.
    public GameObject[] snowTiles;                                    //Array of floor prefabs.
    public GameObject[] wallTiles;                                    //Array of wall prefabs.
    public GameObject[] propTiles;                                    //Array of prop prefabs.
    public GameObject[] treasureTiles;                                    //Array of prop prefabs.
    public GameObject[] enemyTiles;                                    //Array of enemy prefabs.
    public GameObject[] outerWallTiles;                                //Array of outer tile prefabs.

    private Transform boardHolder;                                    //A variable to store a reference to the transform of our Board object.
    private List<Vector3> gridPositions = new List<Vector3>();    //A list of possible locations to place tiles.
    private NavMeshSurface surface;

    //Clears our list gridPositions and prepares it to generate a new board.
    void InitialiseList()
    {
        //Clear our list gridPositions.
        gridPositions.Clear();

        //Loop through x axis (columns).
        for (int x = 1; x < columns - 1; x++)
        {
            //Within each column, loop through y axis (rows).
            for (int y = 1; y < rows - 1; y++)
            {
                //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                gridPositions.Add(new Vector3(x * offset, offset, y * offset));
            }
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //Sets up the outer walls and floor (background) of the game board.
    void BoardSetup()
    {
        //Instantiate Board and set boardHolder to its transform.
        boardHolder = new GameObject("Board").transform;
        surface = boardHolder.gameObject.AddComponent<NavMeshSurface>();
        surface.collectObjects = CollectObjects.Children;
        //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
        for (int x = -1; x < columns + 1; x++)
        {
            //Loop along y axis, starting from -1 to place floor or outerwall tiles.
            for (int y = -1; y < rows + 1; y++)
            {
                Vector3 finalPos = new Vector3(x * offset, 0f, y * offset);
                //Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
                GameObject toInstantiate = PerlinTile(new Vector2(x, y));
                //Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                    finalPos.y = offset;
                }

                //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                GameObject instance =
                    Instantiate(toInstantiate, finalPos, Quaternion.identity) as GameObject;
                //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                instance.transform.SetParent(boardHolder);
            }
        }

    }


    //RandomPosition returns a random position from our list gridPositions.
    Vector3 RandomPosition()
    {
        //Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
        int randomIndex = Random.Range(0, gridPositions.Count);

        //Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
        Vector3 randomPosition = gridPositions[randomIndex];

        //Remove the entry at randomIndex from the list so that it can't be re-used.
        gridPositions.RemoveAt(randomIndex);

        //Return the randomly selected Vector3 position.
        return randomPosition;
    }


    //LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        //Choose a random number of objects to instantiate within the minimum and maximum limits
        int objectCount = Random.Range(minimum, maximum + 1);

        //Instantiate objects until the randomly chosen limit objectCount is reached
        for (int i = 0; i < objectCount; i++)
        {
            //Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
            Vector3 randomPosition = RandomPosition();

            //Choose a random tile from tileArray and assign it to tileChoice
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

            //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
            GameObject instance = Instantiate(tileChoice, randomPosition, Quaternion.identity);

            instance.transform.SetParent(boardHolder);

        }
    }


    //SetupScene initializes our level and calls the previous functions to lay out the game board
    public void SetupScene(int level)
    {
        //Creates the outer walls and floor.
        BoardSetup();

        //Reset our list of gridpositions.
        InitialiseList();

        //Instantiate a random number of wall tiles based on minimum and maximum, at randomized positions.
        LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);

        //Instantiate a random number of prop tiles based on minimum and maximum, at randomized positions.
        LayoutObjectAtRandom(treasureTiles, treasureCount.minimum, treasureCount.maximum);

        //Instantiate a random number of prop tiles based on minimum and maximum, at randomized positions.
        LayoutObjectAtRandom(propTiles, propCount.minimum, propCount.maximum);


        //Before adding the enemies, but after we added anything else; we build the navmesh
        surface.BuildNavMesh();


        //Instantiate the exit tile in the upper right hand corner of our game board
        Instantiate(exit, RandomPosition(), Quaternion.identity);

        _playerInstance = Instantiate(player, new Vector3(columns - 1, 2.5f, rows - 1), Quaternion.identity);
        cam.Follow = _playerInstance.transform;
        GameManager.Instance._playerInstance = _playerInstance;
        //Determine number of enemies based on current level number, based on a logarithmic progression
        int enemyCount = (int)Mathf.Log(level, 2f);
        //Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);


    }

    // Calculates Perlin Noise based tiles for the ground.
    public GameObject PerlinTile(Vector2 perlinPos)
    {
        float xCoord = perlinPos.x / columns * scale;
        float yCoord = perlinPos.y / rows * scale;

        float perlinNoise = Mathf.PerlinNoise(xCoord, yCoord);
        GameObject toInstantiate = null;

        if (perlinNoise <= normalTileThreshold)
        {
            toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
        }
        else
        {
            toInstantiate = snowTiles[Random.Range(0, snowTiles.Length)];
        }

        //placeholder para evitar errores
        return toInstantiate;
    }

    void Start()
    {
        columns += GameManager.Instance.level;
        treasureCount.minimum += GameManager.Instance.level;
        treasureCount.maximum += GameManager.Instance.level;

        wallCount.minimum += GameManager.Instance.level;
        wallCount.maximum += GameManager.Instance.level + 1;

        propCount.minimum += GameManager.Instance.level;
        propCount.maximum += GameManager.Instance.level + 1;


        scale += GameManager.Instance.level;

        WorldGenerator.Instance.SetupScene(GameManager.Instance.difficulty);

    }
}
