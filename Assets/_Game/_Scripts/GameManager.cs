using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void StartCaseOne()
    {
        SceneManager.LoadScene("Case 1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
