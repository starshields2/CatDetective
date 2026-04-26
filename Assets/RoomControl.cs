using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour
{
    //The room control should: 
    //get the main camera and hold transforms for each camera spot. Should also hold a canvas with a crossfade material on it. 
    //when function is triggered, play the crossfade, then move the camera.
    // Start is called before the first frame update
    [Header("Room Details")]
    public Room[] rooms;
    
    public int changeRoomID;
    public Transform _mainCamTransform;
    public Transform[] roomTransforms;
    public GameObject crossfader;
    public Animation xFade;

    public Transform player;
    public Transform playerStartingPoint;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   // [ContextMenu("ChangeRoom")]
    public void ChangeRoom(int roomID)
    {
        roomID = changeRoomID;
        _mainCamTransform.position = roomTransforms[roomID].position;
    }

}
