using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHigher : MonoBehaviour
{
    public PlayerController _playerControl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        _playerControl = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked");
        _playerControl.HandleLedges();
        
    }
}
