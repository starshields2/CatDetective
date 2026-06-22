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
        targetPosition = new Vector3 (player.transform.position.x, offset.y, offset.z);
        
        float halfWidth = Camera.main.orthographicSize * Camera.main.aspect;

        targetPosition.x = Mathf.Clamp(
            targetPosition.x,
            leftBound.position.x + halfWidth,
            rightBound.position.x - halfWidth
        );
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
