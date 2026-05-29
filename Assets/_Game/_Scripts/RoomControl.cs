using Unity.VisualScripting;
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
    public GameObject crossfader;
    public Animation xFade;

    private GameObject player;
    private PlayerController playerController;

    void Start()
    {
        rooms = FindObjectsOfType<Room>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        
    }

   // [ContextMenu("ChangeRoom")]
    public void ChangeRoom(int roomID)
    {
        playerController.moving = false;
        roomID = changeRoomID;
        _mainCamTransform.position = rooms[roomID].camPosition.position;
        //player.transform.position = rooms[roomID].playerStart.position;
    }

    public void EnterRoom(int roomID, Transform exitPoint)
    {
        if (!exitPoint) return;
        
        playerController.moving = false;

        Room targetRoom = null;

        foreach (Room room in rooms)
        {
            if (room.roomNumber == roomID)
            {
                targetRoom = room;
                break;
            }
        }
        
        if (!targetRoom)
        {
            Debug.LogError("No room found with ID: " + roomID);
            return;
        }
        
        _mainCamTransform.position = targetRoom.camPosition.position;
        player.transform.position = exitPoint.position;
    }
}
