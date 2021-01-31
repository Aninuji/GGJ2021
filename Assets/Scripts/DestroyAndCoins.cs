using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAndCoins : MonoBehaviour
{
    public GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision obj){
        if(obj.gameObject.tag == "Player_Arrow" || obj.gameObject.tag == "Player_Melee" || obj.gameObject.tag == "Damage"){
            int totalCoins = (int)Random.Range(1.0f, 4.0f);
            
            for (int i = 0; i < totalCoins; i++){
                Vector3 newPosition =  new Vector3(Random.Range(0.0f, 2.5f), -0.95f, Random.Range(0.0f ,2.5f));
                Instantiate (coin,transform.position + newPosition, Quaternion.identity);    
            }
            Destroy(gameObject);
        }
    } 
}
