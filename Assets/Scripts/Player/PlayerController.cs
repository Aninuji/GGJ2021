using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Private Members
    [SerializeField]
    [Tooltip("Player's movement speed")]

    private float _playerSpeed = 0;
    [SerializeField]
    [Tooltip("Player's Ranged Weapon Rate Of Fire")]
    private float _rof = 1.0f;

    [SerializeField]
    [Tooltip("Player's Melee weapon Rate Of Fire")]
    private float _meleeROF = 0.5f;

    [SerializeField]
    private float _proyectileSpeed = 10.0f;

    [SerializeField]
    [Tooltip("barrel for instantiating proyectiles")]
    private Transform _barrel;
    private Rigidbody _rb;
    private bool _shooting = false;
    private bool _isRanged = true;
    #endregion


    #region Public Members
    public GameObject bulletPrefab;

    public GameObject meleeHitBox;
    #endregion

    public AudioSource playAudio;
    public AudioClip stepClip;
    public AudioClip attackClip;
    public AudioClip coinClip;
    private bool auxiliar = false;

    //    public bool isSoundPlayed;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        playAudio = GetComponent<AudioSource>();

        _playerSpeed = UpgradeManager.Instance.Speed.value;
        _rof -= UpgradeManager.Instance.rangeROF.value;

        // isSoundPlayed = false;
    }

    private void Update()
    {
        //MovePlayer();
        Aim();
        WeaponSelect();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        //Get Player actual position
        Vector3 playerPosition = transform.position;

        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            //Move Player
            _rb.MovePosition(new Vector3(playerPosition.x + Input.GetAxis("Horizontal") * _playerSpeed * Time.deltaTime, playerPosition.y, playerPosition.z + Input.GetAxis("Vertical") * _playerSpeed * Time.deltaTime));
            //transform.position = new Vector3(playerPosition.x + Input.GetAxis("Horizontal") * _playerSpeed * Time.deltaTime, playerPosition.y, playerPosition.z + Input.GetAxis("Vertical") * _playerSpeed * Time.deltaTime);     
            if (playAudio.isPlaying == false)
            {
                playAudio.PlayOneShot(stepClip, 0.5f);
                StartCoroutine(WaitForSound(0.7f));
            }
            auxiliar = true;
        }
    }

    public IEnumerator WaitForSound(float time)
    {
        // yield return new WaitUntil(() => playAudio.isPlaying == false);
        yield return new WaitForSeconds(time);
    }

    private void WeaponSelect()
    {
        if (Input.GetKeyDown("1"))
        {
            Debug.Log("Ranged Weapon Selected");
            _isRanged = true;
        }
        else if (Input.GetKeyDown("2"))
        {
            _shooting = false;
            Debug.Log("Melee Weapon Selected");
            _isRanged = false;
        }

        if (_isRanged)
        {
            Ranged();
        }
        else
        {
            Melee();
        }
    }

    private void Aim()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float raylength;

        if (groundPlane.Raycast(cameraRay, out raylength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(raylength);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    private void Ranged()
    {
        if (Input.GetButton("Fire1"))
        {
            if (!_shooting)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    private void Melee()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!_shooting)
            {
                StartCoroutine(Hit());
            }

        }
    }

    private IEnumerator Shoot()
    {
        _shooting = true;
        GameObject newBullet = Instantiate(bulletPrefab, _barrel.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * _proyectileSpeed, ForceMode.Impulse);
        playAudio.PlayOneShot(attackClip, 0.5f);
        yield return new WaitForSeconds(_rof);
        Destroy(newBullet, 3);
        _shooting = false;
    }

    private IEnumerator Hit()
    {
        _shooting = true;
        GameObject hitBox = Instantiate(meleeHitBox, _barrel.position, transform.rotation, transform);
        playAudio.PlayOneShot(attackClip, 0.5f);
        Destroy(hitBox, 0.5f);
        yield return new WaitForSeconds(_meleeROF);

        _shooting = false;
    }

    private void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "coin")
        {
            playAudio.PlayOneShot(coinClip, 0.5f);
        }
    }
}


