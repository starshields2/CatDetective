using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayerJump : MonoBehaviour
{
    public PlayerController pControl;
    // Start is called before the first frame update
    void Start()
    {
        pControl = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            pControl.isClimbing = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            pControl.isClimbing = false;
        }
    }
}
