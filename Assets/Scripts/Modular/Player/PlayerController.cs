using UnityEngine;

public class PlayerController : RepeatMonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    
    [SerializeField] private PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement => playerMovement;
    
    [SerializeField] private PlayerInteraction playerInteraction;
    public PlayerInteraction PlayerInteraction => playerInteraction;

    protected override void Awake()
    {
        if(Instance != null) Debug.LogError("PlayerController is already initialized");
        Instance = this;
        base.Awake();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        playerMovement = GetComponentInChildren<PlayerMovement>();
        playerInteraction = GetComponentInChildren<PlayerInteraction>();
    }
}
