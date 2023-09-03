using System;
using System.Collections;
using System.Collections.Generic;
using Funlary.Unit5.ColourBridge.BridgeModule;
using UnityEngine;
using Funlary.Unit5.OpponentModule.Controller;
using Funlary.Unit5.StackModule;

namespace Funlary.Unit5.OpponentModule
{
    public class Opponent : MonoBehaviour, IColor
    {
        public int ID;
        public ColorType ColorType;
        public enum OpponentType { AI, PLAYER }
        public OpponentType opponentType = OpponentType.AI;
        public Transform character;
        public Transform stackParent;
        public StackController stackController;
        public OpponentMovement opponentMovement;

        private IControl _IControl;
        private int stackCount = 0;
        
        
        public bool HasStack { get => stackCount > 0; }
        public int StackCount { get => stackCount; set=> stackCount = value; }

        private void Start()
        {
            stackController = new StackController(this);
            CreateOpponent();
            //stackCount++;
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

        public bool CheckColor(ColorType targetColor) => targetColor == this.ColorType;

        public Color GetColor()
        {
            Color color;
            switch (this.ColorType)
            {
                case ColorType.Red:
                    color = Color.red;
                    break;
                case ColorType.Green:
                    color = Color.green;
                    break;
                case ColorType.Blue:
                    color = Color.blue;
                    break;
                case ColorType.Yellow:
                    color = Color.yellow;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return color;
        }
    }
}
