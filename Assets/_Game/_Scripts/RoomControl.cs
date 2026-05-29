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

    [Header("Crossfade")]
    public GameObject crossfader;
    public Animation xFade;

    private GameObject player;
    private PlayerController playerController;

    void Start()
    {
        rooms = FindObjectsOfType<Room>(); // automatically get all rooms in the scene and add to array

        // get player references
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    public void EnterRoom(int roomID, Transform exitPoint)
    {
        // if exitPoint doesn't exist stop function
        if (!exitPoint) return;
        
        // stop player movement upon entering a door
        playerController.moving = false;

        // take out any previous references
        Room targetRoom = null;

        // filter through each room, and if the room number matches the room ID, set targetRoom to that ID
        foreach (Room room in rooms)
        {
            if (room.roomNumber == roomID)
            {
                targetRoom = room;
                break;
            }
        }
        
        // if target room doesn't match any ID's, stop function
        if (!targetRoom)
        {
            Debug.LogError("No room found with ID: " + roomID);
            return;
        }
        
        // teleport player to next destination
        _mainCamTransform.position = targetRoom.camPosition.position;
        player.transform.position = exitPoint.position;
    }
}
