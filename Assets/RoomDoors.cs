using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoors : MonoBehaviour
{
    public RoomControl roomControl;
    public int targetRoomID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SendToRoom();
        }
    }

    void SendToRoom()
    {
        roomControl.changeRoomID = targetRoomID;
        roomControl.ChangeRoom(roomControl.changeRoomID);
    }
}
