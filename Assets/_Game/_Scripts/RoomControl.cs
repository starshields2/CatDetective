using System.Collections;
using UnityEngine;

public class RoomControl : MonoBehaviour
{
    //The room control should: 
    //get the main camera and hold transforms for each camera spot. Should also hold a canvas with a crossfade material on it. 
    //when function is triggered, play the crossfade, then move the camera.
    // Start is called before the first frame update
    [Header("Room Details")]
    public Room[] rooms;

    [Header("Crossfade")]
    [SerializeField] private Crossfade crossfader;

    private GameObject player;
    private PlayerController playerController;
    private CameraController cameraController;

    void Start()
    {
        rooms = FindObjectsOfType<Room>(); // automatically get all rooms in the scene and add to array

        // get player references
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    public void EnterRoom(int roomID, Transform exitPoint)
    {
        // if exitPoint doesn't exist stop function
        if (!exitPoint) return;

        StartCoroutine(ChangeRoom(roomID, exitPoint));
    }

    private IEnumerator ChangeRoom(int roomID, Transform exitPoint)
    {
        yield return StartCoroutine(crossfader.FadeOut());
        
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
        
        if(!targetRoom)
        {
            Debug.LogError("No room found with ID: " + roomID);
            yield break;
        }
        
        // teleport player to next destination
        player.transform.position = exitPoint.position;

        cameraController.leftBound = targetRoom.leftBound;
        cameraController.rightBound = targetRoom.rightBound;
        
        cameraController.SnapToPlayer();

        yield return new WaitForSeconds(0.1f);
        
        yield return StartCoroutine(crossfader.FadeIn());
    }
}
