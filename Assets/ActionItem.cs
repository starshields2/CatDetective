using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionItem : MonoBehaviour, Interactable.IActionInteractable
{
    [System.Serializable]
    public struct Actions
    {
        public bool audioCue;
        public bool dialogueCue;
        public bool animation;
    }

    public AudioSource _audio;
    public OverheadDialogueTemplate _dialogue;
    public TextAsset _associatedDialogue;
    public Animation _animationToPlay;

    [SerializeField] private Actions _actions;   // this is what shows up
    void Start()
    {
       
    }
    public void PerformAction()
    {
        if (_actions.audioCue)
            _audio.Play();

        if (_actions.dialogueCue)
        {
            _dialogue.preloadedStory = _associatedDialogue;
            _dialogue.LoadStory();
        }

        if (_actions.animation)
        {
            _animationToPlay.Play();
        }
    }

    public void TriggerNewAction()
    {
        Debug.Log("Action Triggered.");
    }
}