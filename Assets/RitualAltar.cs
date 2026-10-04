using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualAltar : BaseInteractable, Interactable.IActionInteractable
{
    public Transform RitualLocation;
    public GameObject demoEndMenu;
    public float catMoveDuration = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
  void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("RITUAL TIME!");
            StartRitual();
        }
    }
    public void PerformAction()
    {
        StartRitual();
    }
    public void StartRitual()
    {
        StartCoroutine(RitualSequence());
        
    }

    private IEnumerator RitualSequence()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(MoveCat(player.transform.position, RitualLocation.transform.position));
        yield return new WaitForSeconds(4f);
        demoEndMenu.SetActive(true);
    }

    private IEnumerator MoveCat(Vector3 start, Vector3 end)
    {
        float timer = 0f;

        while (timer < catMoveDuration)
        {
            timer += Time.deltaTime;

            float t = timer / catMoveDuration;

            player.transform.position = Vector3.Lerp(start, end, t);

            yield return null;
        }

        player.transform.position = end;
    }
}
