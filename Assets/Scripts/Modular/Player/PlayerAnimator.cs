using UnityEngine;

public class PlayerAnimator : RepeatMonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private Animator animator;
    
    private enum AnimationParameters
    {
        IsWalking
    }
    
    private void Start()
    {
        GetAnimatorComponent();
        GetPlayerControllerComponent();
    }

    private void GetAnimatorComponent()
    {
        if (this.animator != null) return;
        animator = GetComponent<Animator>();
    }

    private void GetPlayerControllerComponent()
    {
        if (this.playerController != null) return;
        playerController = FindComponentInParent<PlayerController>();
    }

    private void Update() => SetAnimations();

    private void SetAnimations()
    {
        animator.SetBool(AnimationParameters.IsWalking.ToString(), 
                        playerController.GetPlayerMovement().GetIsWalking());
    }
}
