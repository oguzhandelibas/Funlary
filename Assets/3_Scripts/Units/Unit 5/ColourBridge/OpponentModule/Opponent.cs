using System;
using System.Collections;
using System.Collections.Generic;
using Funlary.Unit5.ColourBridge.BridgeModule;
using UnityEngine;
using Funlary.Unit5.OpponentModule.Controller;
using Funlary.Unit5.StackModule;
using NaughtyAttributes;

namespace Funlary.Unit5.OpponentModule
{
    public class Opponent : MonoBehaviour, IColor
    {
        #region FIELDS

        public Transform character;
        public Transform stackParent;
        public OpponentStackController OpponentStackController;
        public OpponentMovement opponentMovement;
        [SerializeField] private ColorData colorData;
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
        #endregion

        #region VARIABLES

        public int ID;
        public enum OpponentType { AI, PLAYER }
        public OpponentType opponentType = OpponentType.AI;

        public ColorType ColorType;
        private IControl _IControl;

        private int _stackCount = 0;

        #endregion

        #region PROPERTIES

        public Material GetColor => colorData.ColorType[ColorType];
        public bool CheckColor(ColorType targetColor) => targetColor == this.ColorType;
        public bool HasStack { get => _stackCount > 0; }
        public int StackCount { get => _stackCount; set => _stackCount = value; }

        #endregion

        #region UNITY FUNCTIONS

        private void Start()
        {
            skinnedMeshRenderer.material = colorData.ColorType[ColorType];
            OpponentStackController = new OpponentStackController(this);
            CreateOpponent();
        }

        #endregion

        #region OPPONENT FUNCTIONS

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

        #endregion

    }
}
