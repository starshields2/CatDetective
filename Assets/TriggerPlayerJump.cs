using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayerJump : MonoBehaviour
{
    public GameObject ledgePrompt;
    public Transform promptLocation;
    public PlayerController pControl;
    public enum Type
    {
        Small,
        Medium,
        Tall
    }

    public Type _type;
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
            if (_type == Type.Small)
            {
                pControl.isClimbing = true;
            }
            if (_type == Type.Medium)
            {
                ledgePrompt.SetActive(true);

            }
            if (_type == Type.Tall)
            {
                pControl.highJump = true;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            pControl.isClimbing = false;
            if (_type == Type.Medium)
            {
                ledgePrompt.SetActive(false);

            }
        }
    }
}
