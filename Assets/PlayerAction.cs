using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
    [Serializable]
    public abstract class PlayerAction
    {
        [SerializeField] protected bool isOneTimeUse = false;
       
        [SerializeField] private int _schemaVersion = 1;
        public int SchemaVersion => _schemaVersion;
 
        public bool IsOneTimeUse {  get { return isOneTimeUse; } }

        public PlayerAction(bool isOneTimeUse = false)
        {
            this.isOneTimeUse = isOneTimeUse;
        }

        public abstract void Invoke(PlayerCharacter player);
        
    }

    public class PlayerActionLoop
    {
        private PlayerCharacter player;
        private List<PlayerAction> playerActions = new List<PlayerAction>();

        public PlayerActionLoop(PlayerCharacter player)
        {
            this.player = player ?? throw new ArgumentNullException(nameof(player));
        }

        public void AppendAction(PlayerAction action) => playerActions.Add(action);

        public void ProcessActions()
        {
            foreach (PlayerAction action in playerActions)
                action.Invoke(player);

            playerActions = playerActions.Where(pa => !pa.IsOneTimeUse).ToList();
        }
    }
}
