using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinRound : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.WinLevel();
        }
    }
}
