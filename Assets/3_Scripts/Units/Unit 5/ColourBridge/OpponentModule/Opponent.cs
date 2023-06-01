using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Funlary.Unit5.OpponentModule.Controller;

namespace Funlary.Unit5.OpponentModule
{
    public class Opponent : MonoBehaviour
    {
        public enum OpponentType { AI, PLAYER }
        public OpponentType opponentType = OpponentType.AI;

        [SerializeField] private OpponentMovement opponentMovement;
        
        private IControl _IControl;

        private void Start()
        {
            CreateOpponent();
        }

        private void CreateOpponent()
        {
            if (opponentType == OpponentType.AI)
            {
                _IControl = new AIController();
            }
            else if (opponentType == OpponentType.PLAYER)
            {
                PlayerController playerController = new PlayerController();
                _IControl = playerController;
                
                playerController.joystickController = FindObjectOfType<JoystickController>();
            }
            
            opponentMovement._IControl = _IControl;
        }
        
        
    }
}
