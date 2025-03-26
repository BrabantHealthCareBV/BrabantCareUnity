using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLogic : MonoBehaviour
{
    public void LoadGameScene()
    {

        SceneManager.LoadScene("GameScene");
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
