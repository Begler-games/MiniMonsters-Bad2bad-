using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManager2 : MonoBehaviour
{
  public void changeScene(int sceneOrder)
    {
        SceneManager.LoadScene(sceneOrder);
    }
  
}
