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

    [Header("Interaction")]
    [SerializeField] private Transform interactablePoint;
    [SerializeField] private float interactionRange = 2;
    private SurfaceManager surfaceManager;
    public float InteractionRange => interactionRange;

    [HideInInspector] public GameObject player;
    [HideInInspector] public PlayerController playerController;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        
        surfaceManager = FindObjectOfType<SurfaceManager>();
    }
    
    private void OnMouseEnter()
    {
        HoverInteract();
    }

    private void OnMouseExit()
    {
        UnhoverInteract();
    }

    public Vector3 GetInteractionPoint(Transform player)
    {
        Vector3 target;
        if (!interactablePoint) { target = transform.position; }
        else { target = interactablePoint.position; }

        // Find which surface this object is standing on
        ClickableSurface surface = surfaceManager.ResolveSurface(target, true);

        if (surface)
        {
            Collider2D groundCol = surface.GetComponent<Collider2D>();

            // Stand on top of the ground
            target.y = groundCol.bounds.max.y;

            // Add half the player's height so their feet are on the ground
            SpriteRenderer playerRenderer = player.GetComponent<SpriteRenderer>();
            target.y += playerRenderer.bounds.extents.y;
        }

        Vector3 direction = (target - player.position).normalized;

        return target - direction * interactionRange;
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
