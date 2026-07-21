using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum InteractableType
    {
        Action, Item, Sequence
    }
    
    [Header("General")]
    public InteractableType interactableType;

    public bool isKeyItem;

    public GameObject actionReference;
    public bool canInteract = true;

    public void Interact()
    {
        if (!canInteract) return;

        switch (interactableType)
        {
            case InteractableType.Action:
                HandleAction();
                break;

            case InteractableType.Item:
                HandleItem();
                break;

            case InteractableType.Sequence:
                HandleSequence();
                break;
        }

        void HandleAction()
        {
            IActionInteractable action = actionReference.GetComponent<IActionInteractable>();

            if (action != null)
                action.PerformAction();
        }

        void HandleItem()
        {
            // Later:
            // Quinn picks up object
            // Follow player
            // Deliver to Peter
        }

        void HandleSequence()
        {
            ISequenceListener listener = actionReference.GetComponent<ISequenceListener>();

            if (listener != null)
                listener.SequenceTriggered();
        }

    }
    
    public interface IActionInteractable
    {
        void PerformAction();
    }
    
    public interface ISequenceListener
    {
        void SequenceTriggered();
    }
}
