using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManagement : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Texture2D searchCursor;
    public Texture2D talkCursor;
    public Texture2D moveCursor;

    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        // ForceSoftware bypasses hardware limitations to respect texture size
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.ForceSoftware);
    }
    // Start is called before the first frame update
 
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSearchCursorEnter()
    {
        Cursor.SetCursor(searchCursor, hotSpot, CursorMode.ForceSoftware);
    }
    public void OnTalkCursorEnter()
    {
        Cursor.SetCursor(talkCursor, hotSpot, CursorMode.ForceSoftware);
    }
    public void OnMoveCursorEnter()
    {
        Cursor.SetCursor(moveCursor, hotSpot, CursorMode.ForceSoftware);
    }
    public void OnDefaultCursorEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.ForceSoftware);
    }

}
