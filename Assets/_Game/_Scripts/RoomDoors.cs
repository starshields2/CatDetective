using UnityEngine;

public class RoomDoors : MonoBehaviour
{
    private RoomControl roomControl;
    public int targetRoomID;
    public Transform exitPoint;

    void Start()
    {
        roomControl = GameObject.Find("RoomController").GetComponent<RoomControl>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        SendToRoom();
        
    }

    void SendToRoom()
    {
        roomControl.EnterRoom(targetRoomID, exitPoint);
    }
}
