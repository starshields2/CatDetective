using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGameManager : MonoBehaviour
{
    //Array containing ink dialogue files needed for the scene.
    public TextAsset[] _storyAsset = null;
    public CanvasGroup _inkCanvas;
    public OverheadDialogueTemplate _dialogueTemplate;
    public int storyAssetIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("Load Overhead")]
    public void LoadOverheadDialogue()
    {
        //set the canvas active
        _inkCanvas.alpha = 1;
        _dialogueTemplate.inkJSONAsset = _storyAsset[storyAssetIndex];
        _dialogueTemplate.StartStory();
    }

    public void UnloadOverheadDialogue()
    {
        _inkCanvas.alpha = 0;
    }
}
