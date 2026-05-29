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
        if (!Input.GetMouseButtonDown(0)) return;
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider == null) return;
        
        float direction = Mathf.Sign(mousePos.x - transform.position.x);
        Vector2 rayOrigin = (Vector2)transform.position + Vector2.right * (direction * 0.5f);
        
        ClickableSurface surface = hit.collider.GetComponent<ClickableSurface>();
        if (!surface) return;
        
        targetPosition = new Vector3(hit.point.x, surface.surfaceY, transform.position.z);
        
        if(CanMoveTo(surface))
            moving = true;

        if(CanJumpTo(surface, targetPosition))
        {
            currentHeightLevel = surface.heightLevel;
            StartCoroutine(JumpTo(targetPosition));
        }
    }

    private void Move()
    {
        if (!moving) return;
        
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            moving = false;
        }
    }

    bool CanMoveTo(ClickableSurface targetSurface)
    {
        float diff = targetSurface.heightLevel - currentHeightLevel;
        
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

            pos.y += arc;

            transform.position = pos;

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
    }
}
