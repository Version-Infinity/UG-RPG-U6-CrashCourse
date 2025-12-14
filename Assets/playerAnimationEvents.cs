using UnityEngine;

namespace Assets
{
    public class playerAnimationEvents : MonoBehaviour
    {
        private PlayerCharacter player;

        private void Awake()
        {
            player = GetComponentInParent<PlayerCharacter>();
        }

        private void AttackStarted()
        {
            player.SetMovementAndJump(false);
            player.SetCanAttack(false);
        }

        private void AttackEnded()
        {
            player.SetCanAttack(true);
        }

        private void EnableMobility()
        {
            player.SetMovementAndJump(true);
        }

    }
}
