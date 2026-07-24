using UnityEngine;

public class RoomDoors : MonoBehaviour, Interactable.IActionInteractable
{
    private RoomControl roomControl;
    public int targetRoomID;
    public Transform exitPoint;

    void Start()
    {
        roomControl = GameObject.Find("RoomController").GetComponent<RoomControl>();
    }

    public void PerformAction()
    {
        roomControl.EnterRoom(targetRoomID, exitPoint);
    }
}