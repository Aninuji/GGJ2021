using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAndCoins : MonoBehaviour
{
    public GameObject coin;
    public GameObject particles;


    private void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "Player_Arrow")
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
            Destroy(gameObject.transform.parent.gameObject, 1);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player_Melee")
        {
            int totalCoins = (int)Random.Range(1.0f, 4.0f);

            for (int i = 0; i < totalCoins; i++)
            {
                Vector3 newPosition = new Vector3(Random.Range(0.0f, 2.5f), -0.95f, Random.Range(0.0f, 2.5f));
                Instantiate(coin, transform.position + newPosition, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
