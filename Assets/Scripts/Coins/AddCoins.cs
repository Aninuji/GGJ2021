using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCoins : MonoBehaviour
{
    TotalCoins totalCoinsObj;
    // Start is called before the first frame update
    void Start()
    {
        totalCoinsObj = GameObject.FindGameObjectWithTag("GlobalCoins").GetComponent<TotalCoins>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f,30f,0f) * Time.deltaTime);
        
    }

     private void OnCollisionEnter(Collision obj){
        if(obj.gameObject.tag == "Player"){
            Debug.Log("entr");
            if(totalCoinsObj != null ){
                totalCoinsObj.addTotalCoins(1);
            }
            Destroy(gameObject);
        }
    } 
}
