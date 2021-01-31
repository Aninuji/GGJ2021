using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class DestroyAndCoins : MonoBehaviour
{
    public GameObject coin;
    public GameObject particles;
    private AudioSource source;
    
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "Player_Arrow" || obj.gameObject.tag == "Player_Melee")
        {
            int totalCoins = (int)Random.Range(1.0f, 4.0f);

            for (int i = 0; i < totalCoins; i++)
            {
                Vector3 newPosition = new Vector3(Random.Range(0.0f, 2.5f), -0.95f, Random.Range(0.0f, 2.5f));
                Instantiate(coin, transform.position + newPosition, Quaternion.identity);
            }
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            particles.SetActive(true);
            source.Play();
            Destroy(gameObject.transform.parent.gameObject, 1);
        }
    }
}
