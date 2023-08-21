using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerController : RepeatMonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        public PlayerMovement PlayerMovement => playerMovement;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            playerMovement = FindComponentInChildren<PlayerMovement>();
        }
    }
}