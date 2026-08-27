using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioManager musicSource;
    [SerializeField] private AudioClip musicClip;
    
    void Start()
    {
        musicSource.PlayMusic(musicClip);
    }
    
    public void StartCaseOne()
    {
        SceneManager.LoadScene("Case 1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
