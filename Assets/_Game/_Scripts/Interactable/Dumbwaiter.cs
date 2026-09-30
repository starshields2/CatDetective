using System.Collections;
using UnityEngine;

public class Dumbwaiter : BaseInteractable, Interactable.IActionInteractable
{
    [Header("Room")]
    [SerializeField] private int targetRoomID;
    [SerializeField] private Transform destinationDumbwaiter;

    [Header("Movement")]
    [SerializeField] private Transform bottomPoint;
    [SerializeField] private float moveDuration = 1f;

    private RoomControl roomControl;

    private bool moving;

    protected override void Awake()
    {
        base.Awake();
        roomControl = GameObject.Find("RoomController").GetComponent<RoomControl>();
    }

    public void PerformAction()
    {
        if (moving)
            return;

        StartCoroutine(UseDumbwaiter());
    }

    private IEnumerator UseDumbwaiter()
    {
        moving = true;

        // Lock Quinn's controls
        playerController.interacting = true;
        playerController.moving = false;

        playerRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        
        // Move Quinn into the destination room
        roomControl.EnterRoom(targetRoomID, destinationDumbwaiter);

        // Put Quinn inside the destination dumbwaiter
        playerController.transform.SetParent(destinationDumbwaiter);

        // Make sure Quinn starts at the dumbwaiter's position
        playerController.transform.localPosition = Vector3.zero;

        // Move the dumbwaiter down
        Vector3 startPosition = destinationDumbwaiter.position;
        Vector3 targetPosition = bottomPoint.position;

        float time = 0f;

        while (time < moveDuration)
        {
            float t = time / moveDuration;

            destinationDumbwaiter.position = Vector3.Lerp(startPosition, targetPosition, t);

            time += Time.deltaTime;
            yield return null;
        }

        destinationDumbwaiter.position = targetPosition;

        // Take Quinn out of the dumbwaiter hierarchy
        playerController.transform.SetParent(null);

        // Return control to Quinn
        playerController.interacting = false;

        // Prevent this dumbwaiter from being used again
        interactable.canInteract = false;

        moving = false;
        
        playerRenderer.maskInteraction = SpriteMaskInteraction.None;
    }
}