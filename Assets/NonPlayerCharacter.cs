using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public Dialogue _dialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            StartTriggeredDialogue();
        }
    }

    void StartTriggeredDialogue()
    {
        Debug.Log("Triggering Dialogues");
        _dialogue.NextLine();
    }
}
