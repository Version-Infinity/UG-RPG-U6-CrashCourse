using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Assets
{

    public static class PlayerIntros
    {
        public static void JumpIntro(PlayerCharacter player) {
            float ogJumpForce = player.jumpForce;
            player.jumpForce = 18;
            player.TryJump(llinearVelocityYOverride: 1);
            player.jumpForce = ogJumpForce;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class IntroActionReferenceAttribute : Attribute { }


    [Serializable]
    public abstract class IntroAction : PlayerAction
    {

        public IntroAction()
        {
            isOneTimeUse = true;
        }
    }

    [Serializable]
    public class IntroJumpAction : IntroAction
    {
        private const int DEFAULT_JUMP_FORCE_OVERRIDE = 18;
        [SerializeField] private int jumpForceOverride = DEFAULT_JUMP_FORCE_OVERRIDE;

        public IntroJumpAction(int jumpForceOverride = DEFAULT_JUMP_FORCE_OVERRIDE)
        {
            this.jumpForceOverride = jumpForceOverride;
        }

        public override void Invoke(PlayerCharacter player)
        {
            float ogJumpForce = player.jumpForce;
            player.jumpForce = jumpForceOverride;
            player.TryJump(llinearVelocityYOverride: 1);
            player.jumpForce = ogJumpForce;
        }
    }
}
