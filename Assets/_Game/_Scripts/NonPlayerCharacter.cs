using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : BaseInteractable
{
    public Dialogue _dialogue;
    public OverheadDialogueTemplate _overheadDialogue;
    public GeneralGameManager generalGameManager;
    public Inventory _inventorySystem;
    public GameObject ItemPickedUp;
    public PlayerController _playerController;
    public int storyIndex; //feed this into overhead dialogue template so it knows which story to load. 
    [SerializeField] private float catMoveDuration = 0.35f;

    public bool stopsPlayer; //use this to stop Quinn if she gets too close
    [SerializeField] private Transform moveToLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
          

            if (stopsPlayer)
            {
                StartCoroutine(RepelCat());
              
            }
            else
            {
                StartTriggeredDialogue();
                _dialogue.InGameDialogue();
            }
        }
        if(other.tag == "Item")
        {
            ItemPickedUp = other.gameObject;

            StartCoroutine(StartItemCollection());

        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {

    }

    private IEnumerator RepelCat()
    {
      
        _playerController.moving = false;
        StopAllCoroutines();
        Debug.Log("Move Quinn somewhere else.");
        _dialogue.InGameDialogue();
        yield return StartCoroutine(MoveCat(player.transform.position, moveToLocation.position));
        //how to make quinn stop moving.
        yield return null;
        yield return new WaitForSeconds(1f);
        StopAllCoroutines();
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _dialogue.ClearDialogue();
        }
    }

    void StartTriggeredDialogue()
    {
        Debug.Log("Triggering Dialogues");
        TriggerOverhead();
    }

    void TriggerOverhead()
    {
        _overheadDialogue.preloadedStory = generalGameManager._storyAsset[storyIndex];
        _overheadDialogue.LoadStory();
    }

    public IEnumerator StartItemCollection()
    {
        InventoryPickup _itemInfo = ItemPickedUp.GetComponent<InventoryPickup>();
       
        _inventorySystem.ItemTemplate = _itemInfo._itemDataGameObject;
        GameObject itemFromPickup = _itemInfo._itemDataGameObject;

        InventoryItem inventoryData = itemFromPickup.GetComponent<InventoryItem>();

        TextAsset newStory;
        newStory = inventoryData._associatedDialogue;
        _overheadDialogue.preloadedStory = newStory;

        _inventorySystem.AddToInventory();
       
        
        
        StartPlayItemDialogue();
        yield return new WaitForSeconds(3f);
        DestroyItemPickedUp();
    }

    void DestroyItemPickedUp()
    {
        Destroy(ItemPickedUp);
        ItemPickedUp = null;
        _playerController.itemGrabbed = false;
    }

    void StartPlayItemDialogue()
    {
        _overheadDialogue.LoadStory();
        
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
