using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void GoGame()
    {
        SceneManager.LoadScene("選車畫面");
    }

}
