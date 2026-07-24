using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum InteractableType
    {
        Action, Item, Sequence
    }
    
    [Header("General")]
    public InteractableType interactableType;
    public GameObject actionReference;
    public bool canInteract = true;
    public bool isKeyItem;
    
    [Header("On Hover")]
    [Tooltip("Put in a prefab with empty Sprite Renderer on it")] 
    [SerializeField] private GameObject hoverObject;
    [SerializeField] private Sprite hoverSprite;
    private GameObject hoverInstance;

    [Header("Interaction")] [SerializeField]
    private Transform interactablePoint;

    public GameObject player;
    public PlayerController playerController;

    void Awake()
    {
        
        playerController = FindObjectOfType<PlayerController>();
    }
    
    private void OnMouseEnter()
    {
        HoverInteract();
    }

    private void OnMouseExit()
    {
        UnhoverInteract();
    }

    public Vector3 GetInteractionPoint()
    {
        if (interactablePoint)
            return interactablePoint.position;
        
        return transform.position;
    }
    
    public void Interact()
    {
        if (!canInteract) return;

        switch (interactableType)
        {
            case InteractableType.Action:
                HandleAction();
                break;

            case InteractableType.Item:
                HandleItem();
                break;

            case InteractableType.Sequence:
                HandleSequence();
                break;
        }

        void HandleAction()
        {
            IActionInteractable action = actionReference.GetComponent<IActionInteractable>();

            if (action != null)
                action.PerformAction();
        }

        void HandleItem()
        {
            // Later:
            // Quinn picks up object
            // Follow player
            // Deliver to Peter
        }

        void HandleSequence()
        {
            ISequenceListener listener = actionReference.GetComponent<ISequenceListener>();

            if (listener != null)
                listener.SequenceTriggered();
        }

    }
    
    public interface IActionInteractable
    {
        void PerformAction();
    }
    
    public interface ISequenceListener
    {
        void SequenceTriggered();
    }

    public void HoverInteract()
    {
        if (!canInteract) return;
        if (!hoverObject || !hoverSprite) return;
        
        hoverInstance = Instantiate(hoverObject, transform.position, Quaternion.identity);
        SpriteRenderer hoverRenderer = hoverInstance.GetComponent<SpriteRenderer>();
        hoverRenderer.sprite = hoverSprite;
        hoverRenderer.sortingOrder = actionReference.GetComponent<SpriteRenderer>().sortingOrder-1;
    }

    public void UnhoverInteract()
    {
        if(!hoverInstance) return;
        
        Destroy(hoverInstance);
        
        hoverInstance = null;
    }
}
