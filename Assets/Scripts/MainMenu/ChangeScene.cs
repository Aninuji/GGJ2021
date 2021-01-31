using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour
{

    public void changeScene(string sceneNameTarget){
        //IEnumerator coroutine = Waiter(sceneNameTarget);
        Debug.Log("changescente");
        StartCoroutine(Waiter(sceneNameTarget));
       
    }

    private IEnumerator Waiter(string sceneNameTarget)
    {
        Debug.Log("waiter");
       yield return new WaitForSeconds(0.5f);
       Debug.Log("after wait");
        SceneManager.LoadScene(sceneNameTarget);
    }
}
