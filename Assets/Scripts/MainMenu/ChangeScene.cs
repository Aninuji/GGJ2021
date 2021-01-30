using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void changeScene(string sceneNameTarget){
        SceneManager.LoadScene(sceneNameTarget);
    }
}
