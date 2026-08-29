using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxJumpDistance;
    
    [Header("References")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private SurfaceManager surfaceManager;
    [SerializeField] private Animator playerAnim;
    
    [Header("Inventory")]
    public bool itemGrabbed; //whether quinn is carrying an item atm.

    public bool interacting;
    
    private SpriteRenderer sr;
    private Vector3 targetPosition;
    private int currentHeightLevel = 0;
    [HideInInspector] public bool moving;
    
    private bool pendingJump;
    private Vector3 pendingJumpTarget;
    private ClickableSurface pendingJumpSurface;
    private Interactable pendingInteractable;
    
    [HideInInspector] public bool currentlyInsideCar;
    
    void Start()
    {
        targetPosition = transform.position;
        sr = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (interacting) return;
        
        HandleClick();
        Move();

             if (!moving)
        {
            playerAnim.SetBool("isRunning", false);
        }
    }

    private void HandleClick()
    {
        if (!Input.GetMouseButtonDown(0) || moving) return; // if no mouse input end function
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // get position from mouse click

        Collider2D hit = Physics2D.OverlapPoint(mousePos);
        if (hit)
        {
            Interactable interactable = hit.GetComponent<Interactable>();

            if (interactable)
            {
                HandleInteractable(interactable);
                return;
            }
        }

        if (currentlyInsideCar) return;
        
        ClickableSurface surface = surfaceManager.ResolveSurface(mousePos, preferLower: true);

        if (!surface) return;
        
        FaceDirection(mousePos.x);

        Vector3 destination = GetDestination(mousePos, surface);

        ExecuteMovement(surface, destination);
    }
    
    private Vector3 GetDestination(Vector2 mousePos, ClickableSurface surface)
    {
        Collider2D col = surface.GetComponent<Collider2D>();

        Vector2 point = new Vector2(mousePos.x, col.bounds.max.y);

        float halfHeight = sr.bounds.extents.y;

        return new Vector3(point.x, point.y + halfHeight, transform.position.z);
    }
    
    private void ExecuteMovement(ClickableSurface surface, Vector3 destination)
    {
        int diff = surface.heightLevel - currentHeightLevel;
        float distance = Mathf.Abs(destination.x - transform.position.x);

        bool isSameLevel = diff == 0;
        bool isJumpUp = diff > 0;
        bool isJumpDown = diff < 0;

        if (isJumpUp)
        {
            bool canJumpNow = distance <= maxJumpDistance;

            if (canJumpNow)
            {
                currentHeightLevel = surface.heightLevel;
                StartCoroutine(JumpTo(destination));
                return;
            }

            targetPosition = GetJumpApproachPoint(destination);
            moving = true;

            pendingJump = true;
            pendingJumpTarget = destination;
            pendingJumpSurface = surface;

            return;
            
        }

        if (isJumpDown)
        {
            bool directDrop = IsDirectDrop(destination);

            if (directDrop)
            {
                currentHeightLevel = surface.heightLevel;
                StartCoroutine(JumpTo(destination));
                return;
            }
            
            Vector3 landingPoint = GetClampedDropPoint(destination);

            currentHeightLevel = surface.heightLevel;
            StartCoroutine(JumpTo(landingPoint));

            // AFTER jump completes, THEN continue walking
            pendingJump = false; // IMPORTANT: disable auto chaining

            targetPosition = destination; // final walk target
            moving = true;

            return;
        }
        
        if (isSameLevel)
        {
            targetPosition = destination;
            moving = true;
        }
    }
    
    private void Move()
    {
        // if moving is false, end function
        playerAnim.SetBool("isRunning", true);
        if (!moving) return;
        
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime);

        // if distance between character position and target position is less than 0.01
        // set moving to false
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            moving = false;
            
            if (pendingJump)
            {
                pendingJump = false;

                currentHeightLevel = pendingJumpSurface.heightLevel;

                StartCoroutine(JumpTo(pendingJumpTarget));
            }
            
            if (pendingInteractable != null)
            {
                pendingInteractable.Interact();
                pendingInteractable = null;
            }
        }

   
    }

    IEnumerator JumpTo(Vector3 target)
    {
        Vector3 start = transform.position;
        float time = 0;
        float duration = 0.35f;

        while (time < duration)
        {
            float t = time / duration;

            // horizontal movement
            Vector3 pos = Vector3.Lerp(start, target, t);

            // vertical arc
            float arc = Mathf.Sin(t * Mathf.PI) * 1.5f;

            // set new y value
            pos.y += arc;

            // move character to calculated position
            transform.position = pos;

            time += Time.deltaTime;
            yield return null;
        }

        // makes sure character is in the right position after movement
        transform.position = target;
    }
    
    private void FaceDirection(float mouseX)
    {
        float diff = mouseX - transform.position.x;

        if (diff > 0.01f)
            sr.flipX = false;
        else if (diff < -0.01f)
            sr.flipX = true;
    }
    
    private Vector3 GetJumpApproachPoint(Vector3 target)
    {
        float direction = Mathf.Sign(target.x - transform.position.x);

        return new Vector3(
            target.x - direction * maxJumpDistance * 0.9f,
            transform.position.y,
            transform.position.z
        );
    }
    
    private bool IsDirectDrop(Vector3 target)
    {
        float xDistance = Mathf.Abs(target.x - transform.position.x);
        float yDistance = transform.position.y - target.y;

        // tweak these numbers to taste
        return xDistance < 1.5f && yDistance > 0.5f;
    }
    
    private Vector3 GetClampedDropPoint(Vector3 target)
    {
        float direction = Mathf.Sign(target.x - transform.position.x);

        // clamp horizontal distance to jump range
        float clampedX = transform.position.x + direction * maxJumpDistance;

        return new Vector3(
            clampedX,
            target.y,
            transform.position.z
        );
    }

    void HandleInteractable(Interactable interactable)
    {
        if (!interactable.canInteract) return;
        
        if (currentlyInsideCar)
        {
            // Only allow interactions, never movement.
            interactable.Interact();
            return;
        }
        
        float distance = Vector2.Distance(transform.position, interactable.transform.position);

        if (distance <= interactable.InteractionRange)
        {
            interactable.Interact();
            return;
        }
        
        pendingInteractable = interactable;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // get position from mouse click
        FaceDirection(mousePos.x);
        targetPosition = interactable.GetInteractionPoint(transform);
        
        moving = true;
    }
}
