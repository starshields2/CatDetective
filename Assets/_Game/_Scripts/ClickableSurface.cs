using UnityEngine;

public class ClickableSurface : MonoBehaviour
{
    [HideInInspector] public float surfaceY;
    public float heightLevel; // 0 = table, 2 = shelf, 3 = high place
    
    private GameObject player; // player reference
    private float playerHeight; // get the Y scale of the player
    
    private BoxCollider2D col; // get box collider of gameobject it is attatched to

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
        // if no collision is detected then stop function
        if (col == null) return;

        // get top part of collision box, and use playerheight to set proper Y value for player movement
        float topY = col.bounds.max.y;
        surfaceY = topY + (playerHeight / 2);
    }
}
