using Manager;
using UnityEngine;

namespace Modular.Player
{
    public class PlayerSound : MonoBehaviour
    {
        [SerializeField] private float volume = 1f;
        private float footstepTimer;
        private float footstepTimerMax = 0.1f;

        private void FixedUpdate()
        {
            footstepTimer -= Time.fixedDeltaTime;
            
            if (footstepTimer > 0) return;
            if (!PlayerController.Instance.GetPlayerMovement().GetIsWalking()) return;
            footstepTimer = footstepTimerMax;
            SoundManager.Instance.PlayFootstepSound(PlayerController.Instance.transform.position,volume);
        }
    }
}