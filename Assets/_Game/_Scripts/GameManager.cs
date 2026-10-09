using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioManager musicSource;
    [SerializeField] private AudioClip defaultTrack;
    [SerializeField] private AudioClip basementTrack;
    
    void Start()
    {
        if(musicSource != null)
        {
            musicSource.PlayMusic(defaultTrack);
        }
       
    }
    
    public void SwapMusic()
    {
        musicSource.PlayMusic(basementTrack);
    }

    public void StartCaseOne()
    {
        SceneManager.LoadScene("Case 1");
    }

    public void StartMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
