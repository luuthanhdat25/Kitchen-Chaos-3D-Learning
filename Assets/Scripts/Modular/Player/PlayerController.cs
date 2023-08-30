using UnityEngine;

public class PlayerController : RepeatMonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerInteraction playerInteraction;

    protected override void Awake()
    {
        if(Instance != null) 
            Debug.LogError("PlayerController is already initialized");
        Instance = this;
        base.Awake();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        GetPlayerMovementComponent();
        GetPlayerInteractionComponent();
    }
    
    private void GetPlayerMovementComponent()
    {
        if(this.playerMovement != null) return;
        playerMovement = GetComponentInChildren<PlayerMovement>();
    }

    private void GetPlayerInteractionComponent()
    {
        if (playerInteraction != null) return;
        playerInteraction = GetComponentInChildren<PlayerInteraction>();
    }
    
    public PlayerMovement GetPlayerMovement() => playerMovement;
    public PlayerInteraction GetPlayerInteraction() => playerInteraction;
}
