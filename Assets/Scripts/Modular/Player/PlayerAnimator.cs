using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerAnimator : RepeatMonoBehaviour
    {
        private Animator animator;
        [SerializeField] private PlayerController playerController;
        
        private enum AnimationParameters
        {
            IsWalking
        }
        
        private void Start()
        {
            animator = GetComponent<Animator>();
            playerController = FindComponentInParent<PlayerController>();
        }

        private void Update()
        {
            animator.SetBool(AnimationParameters.IsWalking.ToString(), playerController.PlayerMovement.GetIsWalking());
        }
    }
    
}