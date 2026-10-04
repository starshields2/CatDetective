using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trowel : BaseInteractable, Interactable.IActionInteractable
{
    [Header("References")]
    [SerializeField] private GameObject window;
    [SerializeField] private Transform windowClosedPoint;
    [SerializeField] private GameObject makeInteractable;

    [Header("Window")]
    [SerializeField] private float windowCloseSpeed = 5f;
    
    private Rigidbody2D rb;
    
    private bool trowelFalling;
    private bool windowClosing;

    private new void Awake()
    {
        base.Awake();
        
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        if (windowClosing)
        {
            window.transform.position = Vector3.MoveTowards(window.transform.position,
                windowClosedPoint.position,windowCloseSpeed * Time.deltaTime);

            if (Vector3.Distance(window.transform.position, windowClosedPoint.position) < 0.01f)
            {
                window.transform.position = windowClosedPoint.position;
                windowClosing = false;
            }
        }
    }
    
    public void PerformAction()
    {
        if (trowelFalling)
            return;

        // Unlodge the trowel
        trowelFalling = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(new Vector2 (Vector2.left.x * 2f, Vector2.up.y * 0.5f), ForceMode2D.Impulse);

        // Close the window
        windowClosing = true;
    }
}
