using UnityEngine;

public class ClickableSurface : MonoBehaviour
{
    [HideInInspector] public float surfaceY;
    public float heightLevel; // 0 = table, 2 = shelf, 3 = high place
    
    private GameObject player;

    private float playerHeight;
    
    private BoxCollider2D col;

    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }
    
    void Start()
    {
        player = GameObject.Find("Player");
        
        playerHeight = player.transform.localScale.y;
        
        RecalculateSurface();
    }

    public void RecalculateSurface()
    {
        if (col == null) return;

        float topY = col.bounds.max.y;

        surfaceY = topY + (playerHeight / 2);
    }
}
