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
    private bool catInside;
    private bool isMoving;

    protected override void Awake()
    {
        base.Awake();
        
        sr = GetComponent<SpriteRenderer>();
        
        sr.sprite = closedSprite;
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

    private IEnumerator OpenCabinet()
    {
        isMoving = true;

        // Open cabinet
        sr.sprite = openSprite;

        yield return null;

        // Cat goes inside
        yield return StartCoroutine(MoveCat(player.transform.position, catInsidePoint.position));
        playerRenderer.sortingLayerName = "Default";

        catInside = true;
        isOpen = true;
        isMoving = false;
    }

    private IEnumerator CloseCabinet()
    {
        isMoving = true;

        // Cat comes out first
        playerRenderer.sortingLayerName = "Foreground";
        yield return StartCoroutine(MoveCat(player.transform.position, catOutsidePoint.position));

        catInside = false;

        // Then close cabinet
        sr.sprite = closedSprite;

        isOpen = false;
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
