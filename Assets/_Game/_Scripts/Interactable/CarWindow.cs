using UnityEngine;
using System.Collections;

public class CarWindow : BaseInteractable, Interactable.IActionInteractable
{
    [SerializeField] private Transform window;
    [SerializeField] private Car car;

    [Header("Animation")]
    [SerializeField] private float rollDistance = .75f;
    [SerializeField] private float rollDuration = .5f;
    
    private bool opened;

    public void PerformAction()
    {
        if (opened)
            return;

        StartCoroutine(OpenWindow());
        
        GetComponent<Interactable>().enabled = false;
    }

    IEnumerator OpenWindow()
    {
        opened = true;

        // Play unlock sound

        yield return new WaitForSeconds(0.3f);

        yield return StartCoroutine(RollWindow());

        // Tell the car the cat can escape
        car.WindowOpened();
        interactable.canInteract = false;
    }
    
    IEnumerator RollWindow()
    {
        Vector3 start = window.localPosition;
        Vector3 end = start + Vector3.down * rollDistance;

        float timer = 0;

        while(timer < rollDuration)
        {
            timer += Time.deltaTime;

            window.localPosition =
                Vector3.Lerp(
                    start,
                    end,
                    timer / rollDuration);

            yield return null;
        }
        
        window.localPosition = end;
    }
}
