using UnityEngine;

public class Newspaper : BaseInteractable, Interactable.IActionInteractable
{
    [SerializeField] private GameObject newspaper;
    
    public void PerformAction()
    {
        newspaper.SetActive(true);
        IsInteracting();
    }
}
