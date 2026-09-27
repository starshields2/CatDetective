using System.Collections;
using UnityEngine;

public class Cabinet : BaseInteractable, Interactable.IActionInteractable
{
    [Header("Cabinet")]
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;
    private SpriteRenderer sr;

    [SerializeField] private Transform catInsidePoint;
    [SerializeField] private Transform catOutsidePoint;

    [Header("Movement")]
    [SerializeField] private float catMoveDuration = 0.35f;

    private bool isOpen;
    private bool isMoving;

    protected override void Awake()
    {
        base.Awake();
        
        sr = GetComponent<SpriteRenderer>();
        
        sr.sprite = openSprite;
        isOpen = true;
    }
    
    public void PerformAction()
    {
        if (isMoving)
            return;

        if (isOpen)
        {
            StartCoroutine(CloseCabinet());
        }
        else
        {
            StartCoroutine(OpenCabinet());
        }
    }

    private IEnumerator CloseCabinet()
    {
        isMoving = true;

        // Open cabinet
        sr.sprite = closedSprite;

        yield return null;

        // Cat comes out
        playerRenderer.sortingLayerName = "Foreground";
        yield return StartCoroutine(MoveCat(player.transform.position, catOutsidePoint.position));

        isOpen = false;
        isMoving = false;
    }

    private IEnumerator OpenCabinet()
    {
        isMoving = true;

        // Cat goes in
        yield return StartCoroutine(MoveCat(player.transform.position, catInsidePoint.position));
        playerRenderer.sortingLayerName = "Default";

        // Then close cabinet
        sr.sprite = openSprite;

        isOpen = true;
        isMoving = false;
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
