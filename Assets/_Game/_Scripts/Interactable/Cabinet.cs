using System.Collections;
using UnityEngine;

public class Cabinet : BaseInteractable, Interactable.IActionInteractable
{
    [Header("Cabinet")]
    public Cabinet otherCabinet;
    public Cabinet thisCabinet;

    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;
    private SpriteRenderer sr;

    [SerializeField] private Transform catInsidePoint;
    [SerializeField] private Transform catOutsidePoint;

    [Header("Movement")]
    [SerializeField] private float catMoveDuration = 0.35f;

    [SerializeField] private bool isOpen;
    [SerializeField] private bool insideCabinet;

    public TextAsset _associatedDialogue;
    public OverheadDialogueTemplate _dialogue;
    public bool firstTime;
    public NonPlayerCharacter _ward;

    protected override void Awake()
    {
        base.Awake();
        
        sr = GetComponent<SpriteRenderer>();
     
        sr.sprite = closedSprite;
        isOpen = false;
    }

    public void SwapCabinetOpening()
    {
        otherCabinet.isOpen = true;
    }
    
    public void PerformAction()
    {
        if (firstTime)
        {
            firstTime = false;
            _dialogue.preloadedStory = _associatedDialogue;
            _dialogue.LoadStory();
        }


        if (isOpen)
        {

            StartCoroutine(CloseCabinet()); // if the cabinet is open and quinn is already inside, close the door.
            
          
        }
        else if (!isOpen)
        {
            StartCoroutine(OpenCabinet());
        }
    }
   

    private IEnumerator CloseCabinet()
    {
        sr.sprite = openSprite;
        Debug.Log("Cat go out.");
        yield return new WaitForSeconds(0.5f);
       // Cat comes out
        playerRenderer.sortingLayerName = "Foreground";
        playerRenderer.sortingOrder = 2;
        yield return StartCoroutine(MoveCat(player.transform.position, catOutsidePoint.position));
        // close cabinet
        sr.sprite = closedSprite;
        _ward.stopsPlayer = true;
        BoxCollider2D _wardBC = _ward.GetComponent<BoxCollider2D>();
        _wardBC.enabled = true;
        yield return null;

 

        isOpen = false;
        //insideCabinet = false;
    }

    private IEnumerator OpenCabinet()
    {  
        sr.sprite = openSprite;
        Debug.Log("Cat go in.");
        yield return new WaitForSeconds(0.2f);
        // Cat goes in
        yield return StartCoroutine(MoveCat(player.transform.position, catInsidePoint.position));
        playerRenderer.sortingLayerName = "Background";
        playerRenderer.sortingOrder = -50;
        // Then close cabinet
        isOpen = true;
        //insideCabinet = true;
        yield return new WaitForSeconds(0.2f);
        _ward.stopsPlayer = false;
        BoxCollider2D _wardBC = _ward.GetComponent<BoxCollider2D>();
        _wardBC.enabled = false;
        sr.sprite = closedSprite;
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
