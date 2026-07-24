using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] public Transform leftBound;
    [SerializeField] public Transform rightBound;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followSpeed;
    
    private Vector3 targetPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.transform.position + offset;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        targetPosition = GetCameraPosition(player.transform.position);
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
    
    private Vector3 GetCameraPosition(Vector3 playerPosition)
    {
        Vector3 pos = new Vector3(playerPosition.x, offset.y, offset.z);

        float halfWidth = Camera.main.orthographicSize * Camera.main.aspect;

        pos.x = Mathf.Clamp(
            pos.x,
            leftBound.position.x + halfWidth,
            rightBound.position.x - halfWidth);

        return pos;
    }

    public void SnapToPlayer()
    {
        transform.position = GetCameraPosition(player.transform.position);
    }
}
