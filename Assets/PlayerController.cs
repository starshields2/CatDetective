using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 movetarget;

    void Start()
    {
        movetarget = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            movetarget = new Vector3(
                mousePos.x,
                transform.position.y,
                transform.position.z
            );
        }

        // This stays exactly the same
        transform.position = Vector3.MoveTowards(
            transform.position,
            movetarget,
            speed * Time.deltaTime
        );
    }
}
