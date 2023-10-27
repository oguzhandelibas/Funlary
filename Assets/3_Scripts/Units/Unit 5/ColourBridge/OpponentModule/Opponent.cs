using System;
using System.Collections;
using System.Collections.Generic;
using Funlary.SoundModule.Signals;
using Funlary.UIModule.Game;
using Funlary.Unit5.ColourBridge.BridgeModule;
using Funlary.Unit5.OpponentModule.Animation;
using UnityEngine;
using Funlary.Unit5.OpponentModule.Controller;
using Funlary.Unit5.StackModule;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine.AI;

namespace Funlary.Unit5.OpponentModule
{
    public class Opponent : MonoBehaviour, IColor
    {
        #region FIELDS

        public Transform character;
        public Transform stackParent;
        public StackManager currentStackManager;
        public AnimationController animationController;
        public OpponentStackController OpponentStackController;
        public OpponentMovement opponentMovement;
        public OpponentPhysicsController opponentPhysicsController;
        [SerializeField] private Joystick joystick;
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

        public void SetColor(ColorType colorType)
        {
            if (opponentType == OpponentType.PLAYER)
            {
                GameUIManager.Instance.gameUI.
                    SetLevelColorText(colorType, ColorManager.Instance.GetColor(colorType));
            }
            ColorType = colorType;
            skinnedMeshRenderer.material = colorData.ColorType[ColorType];
        }
        public Material GetColor => colorData.ColorType[ColorType];
        public bool CheckColor(ColorType targetColor) => targetColor == this.ColorType;
        public bool HasStack { get => _stackCount > 0; }
        public bool CanCollectStack { get; set; }
        public bool CanMove { get; set; }
        public int StackCount { get => _stackCount; set => _stackCount = value; }

        #endregion

        #region UNITY FUNCTIONS

        private void Start()
        {
            OpponentStackController = new OpponentStackController(this);
            CreateOpponent();
            CanCollectStack = true;
            CanMove = true;
        }

        #endregion

        #region OPPONENT FUNCTIONS

        private void CreateOpponent()
        {
            if (opponentType == OpponentType.AI)
            {
                NavMeshAgent navMeshAgent = character.parent.AddComponent<NavMeshAgent>();
                navMeshAgent.acceleration = 4;
                navMeshAgent.angularSpeed = 500;
                navMeshAgent.speed = opponentMovement.MovementSpeed;

                _IControl = new AIController(this, animationController, navMeshAgent);
            }
            else if (opponentType == OpponentType.PLAYER)
            {
                PlayerController playerController = new PlayerController();
                _IControl = playerController;

                character.AddComponent<JoystickController>();
                
                playerController.joystickController = GetComponentInChildren<JoystickController>();
                playerController.joystickController.joystick = joystick;
            }

            opponentMovement._IControl = _IControl;
        }

        public void DropAllStacks(bool canCollectStack, bool destroyAfter)
        {
            OpponentStackController.DropAllStack(canCollectStack, destroyAfter);
        }

        #endregion

    }
}
