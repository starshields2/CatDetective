using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorConnector : MonoBehaviour
{
    public CursorManagement _cursorConnect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseEnter()
    {
        _cursorConnect.OnMoveCursorEnter();
    }

    public void OnMouseExit()
    {
        _cursorConnect.OnDefaultCursorEnter();
    }
}
