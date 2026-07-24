using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour
{
    protected Interactable interactable;
    protected GameObject player;
    protected PlayerController playerController;
    protected SpriteRenderer playerRenderer;

    protected virtual void Awake()
    {
        interactable = GetComponent<Interactable>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        playerRenderer = player.GetComponent<SpriteRenderer>();
    }

    protected void IsInteracting()
    {
        interactable.playerController.interacting = true;
    }

    protected void NotInteracting()
    {
        interactable.playerController.interacting = false;
    }
}
