using System.Collections;
using UnityEngine;

public class WateringCan : BaseInteractable, Interactable.IActionInteractable
{
    private int clickCounter;

    [Header("Push")]
    [SerializeField] private float pushDistance;
    [SerializeField] private float pushDuration;
    [SerializeField] private Vector2 pushDirection;

    [Header("KnockOver")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float fallDuration;
    [SerializeField] private float knockRotation;
    
    public void PerformAction()
    {
        clickCounter++;

        if (clickCounter < 3)
        {
            StartCoroutine(PushObject());
        }
        else
        {
            StartCoroutine(KnockOver());
        }
    }

    IEnumerator PushObject()
    {
        Vector3 start = transform.position;
        Vector3 end = start + (Vector3)(pushDirection.normalized * pushDistance);

        float timer = 0f;
        while (timer < pushDuration)
        {
            timer += Time.deltaTime;
            
            float t = timer / pushDuration;
            
            transform.position = Vector3.Lerp(start, end, t);
            
            yield return null;
        }

        transform.position = end;
    }

    IEnumerator KnockOver()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos;
        endPos.y = GetGroundY();

        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(0f, 0f, knockRotation);

        float timer = 0f;

        while (timer < fallDuration)
        {
            timer += Time.deltaTime;

            float t = timer / fallDuration;

            transform.position = Vector3.Lerp(startPos, endPos, t);
            transform.rotation = Quaternion.Lerp(startRot, endRot, t);

            yield return null;
        }

        transform.position = endPos;
        transform.rotation = endRot;

        interactable.canInteract = false;
    }

    private float GetGroundY()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 10f, groundLayer);

        if (hit.collider)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            
            return hit.point.y + sr.bounds.extents.y;
        }
        return transform.position.y;
    }
}
