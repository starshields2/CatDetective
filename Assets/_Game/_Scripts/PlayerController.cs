using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float maxJumpDistance;
    public bool moving;
    
    private Vector3 targetPosition;
    private float currentHeightLevel = 0;
    [Header("Inventory")]
    public bool itemGrabbed; //whether or not quinn is carrying an item atm.

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        HandleClick();
        Move();
    }

    private void HandleClick()
    {
        // if no mouse input end function
        if (!Input.GetMouseButtonDown(0)) return;
        
        // get position from mouse click
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // send raycast to position gotten from mouse click
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        // if nothing hit with a collision, end function
        if (hit.collider == null) return;
        
        // non fully implemented yet, but set up for character facing direction
        float direction = Mathf.Sign(mousePos.x - transform.position.x);
        Vector2 rayOrigin = (Vector2)transform.position + Vector2.right * (direction * 0.5f);
        
        // see if object hit has this script, if not end function
        ClickableSurface surface = hit.collider.GetComponent<ClickableSurface>();
        if (!surface) return;
        
        targetPosition = new Vector3(hit.point.x, surface.surfaceY, transform.position.z);
        
        // if CanMoveTo returns true, set moving to true
        if(CanMoveTo(surface))
            moving = true;

        // if CanJumpTo returns true, start coroutine for jumping
        if(CanJumpTo(surface, targetPosition))
        {
            currentHeightLevel = surface.heightLevel;
            StartCoroutine(JumpTo(targetPosition));
        }
    }

    private void Move()
    {
        // if moving is false, end function
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
        }
    }

    bool CanMoveTo(ClickableSurface targetSurface)
    {
        // compare height level to surface height level
        float diff = targetSurface.heightLevel - currentHeightLevel;
        
        // if same level, set true
        if (diff == 0) return true;
        
        return false;
    }

    bool CanJumpTo(ClickableSurface targetSurface, Vector3 target)
    {
        float diff = targetSurface.heightLevel - currentHeightLevel;

        // horizontal distance to clicked point
        float distance = Mathf.Abs(target.x - transform.position.x);
        
        // too far away
        if (distance > maxJumpDistance)
            return false;
        
        // if levels are different and not the same, return true
        if ((diff <= 2 && diff > 0) || diff < 0) return true;

        return false;
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
}
