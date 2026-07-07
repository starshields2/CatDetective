using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour, Interactable.IActionInteractable
{
    public enum CarState
    {
        CatInside,
        CatEscaping,
        CatOut
    }

    [Header("References")]
    [SerializeField] private Transform carTransform;
    [SerializeField] private Transform player;
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private Transform exitPoint;

    [Header("Jump Settings")]
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float jumpDuration = 0.5f;

    [Header("Sorting Orders")]
    [SerializeField] private int behindCar = 1;
    [SerializeField] private int aboveCar = 10;

    [Header("Shake")]
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeAngle;      // Degrees
    [SerializeField] private float shakeSpeed;     // Higher = faster wobble
    
    public CarState currentState = CarState.CatInside;
    
    public bool windowOpen;
    
    public void PerformAction()
    {
        if (currentState != CarState.CatInside) return;
        if (!windowOpen)
            StartCoroutine(ShakeCar());
    }

    IEnumerator ShakeCar()
    {
        Quaternion startRotation = carTransform.rotation;

        float timer = 0f;

        while (timer < shakeDuration)
        {
            timer += Time.deltaTime;

            // Oscillate between -maxAngle and +maxAngle
            float angle = Mathf.Sin(timer * shakeSpeed) * shakeAngle;

            carTransform.rotation = Quaternion.Euler(0f, 0f, angle);

            yield return null;
        }

        carTransform.rotation = startRotation;
    }
    
    public void WindowOpened()
    {
        if (windowOpen)
            return;

        windowOpen = true;

        StopAllCoroutines();

        currentState = CarState.CatEscaping;

        StartCoroutine(CatExit());
    }
    
    IEnumerator CatExit()
    {
        yield return new WaitForSeconds(.15f);

        player.gameObject.SetActive(true);

        player.position = carTransform.position;

        playerRenderer.sortingOrder = behindCar;

        Vector3 start = player.position;
        Vector3 end = exitPoint.position;

        float timer = 0;

        while (timer < jumpDuration)
        {
            timer += Time.deltaTime;

            float t = timer / jumpDuration;

            Vector3 pos = Vector3.Lerp(start,end,t);

            pos.y += Mathf.Sin(t * Mathf.PI) * jumpHeight;

            player.position = pos;

            yield return null;
        }

        player.position = end;

        playerRenderer.sortingOrder = aboveCar;

        currentState = CarState.CatOut;
    }
}