using System.Collections;
using UnityEngine;

public class Crossfade : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    [SerializeField] private float fadeDuration;

    void Awake()
    {
        fadeGroup = GetComponent<CanvasGroup>();
    }

    public IEnumerator FadeOut()
    {
        float timer = 0f;
        
        fadeGroup.blocksRaycasts = true;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            
            fadeGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);

            yield return null;
        }
        fadeGroup.alpha = 1f;
    }

    public IEnumerator FadeIn()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            
            fadeGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            
            yield return null;
        }
        fadeGroup.alpha = 0f;
        
        fadeGroup.blocksRaycasts = false;
    }
}
