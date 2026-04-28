using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public Vector3 movetarget;
    public bool isClimbing;
    public bool isClimbingLedge;
    public bool highJump;
    private Rigidbody2D rb;

    void Start()
    {
        movetarget = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            movetarget = new Vector3(
                mousePos.x,
                -4.24f,
                transform.position.z
            );
        }

        
        transform.position = Vector3.MoveTowards(
            transform.position,
            movetarget,
            speed * Time.deltaTime
        );

        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }


    public void HandleLedges()
    {
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce+20f);

    }
}
