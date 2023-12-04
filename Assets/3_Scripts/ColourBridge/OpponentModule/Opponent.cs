using System;
using System.Collections.Generic;
using Funlary.UIModule.Game;
using Funlary.Unit5.ColourBridge.BridgeModule;
using Funlary.Unit5.OpponentModule.Animation;
using UnityEngine;
using Funlary.Unit5.OpponentModule.Controller;
using Funlary.Unit5.StackModule;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

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
        public ColorData colorData;

        [SerializeField] private Joystick joystick;
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
        [SerializeField] private OpponentAudioController audioControllerPrefab;
        [SerializeField] private GameObject opponentUiPanel;
        private OpponentAudioController _opponentAudioController;
        #endregion

        #region VARIABLES
        public int ID;
        public enum OpponentType { AI, PLAYER }
        public OpponentType opponentType = OpponentType.AI;

        public ColorType ColorType;
        public IControl OpponentController;

        public Bridge targetBridge;
        public List<Bridge> bridges;

        private int _stackCount = 0;
        
        #endregion

        #region PROPERTIES

        public Vector3 GetStackAreaSize { get => currentStackManager.stackAreaSize; }
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
        private bool _canMove;
        public bool CanMove
        {
            get => _canMove;
            set
            {
                _canMove = value;
            } 
        }
        public int StackCount { get => _stackCount; set => _stackCount = value; }

        public void SetUIPanelActiveness(bool activeness)
        {
            opponentUiPanel.SetActive(activeness);
        }
        
        #endregion

        #region UNITY FUNCTIONS

        private void Start()
        {
        }

        #endregion

        #region OPPONENT FUNCTIONS

        public void InitializeOpponent(StackManager stackManager)
        {
            currentStackManager = stackManager;
            OpponentStackController = new OpponentStackController(this);
            CreateOpponent();
        }

        public void SetBridges(List<Bridge> bridges)
        {
            this.bridges = bridges;
            targetBridge = this.bridges[Random.Range(0, this.bridges.Count-1)];
        }

        private void CreateOpponent()
        {
            CanMove = true;
            CanCollectStack = true;
            
            if (opponentType == OpponentType.AI)
            {
                AIController aiController = new AIController(this, animationController);
                OpponentController = aiController;
            }
            else if (opponentType == OpponentType.PLAYER)
            {
                PlayerController playerController = new PlayerController();
                OpponentController = playerController;

                _opponentAudioController = Instantiate(audioControllerPrefab, transform);

                character.AddComponent<JoystickController>();
                
                playerController.joystickController = GetComponentInChildren<JoystickController>();
                playerController.joystickController.joystick = joystick;
            }
            
            opponentMovement._IControl = OpponentController;
        }

        public void DropAllStacks(bool canCollectStack, bool destroyAfter)
        {
            OpponentStackController.DropAllStack(canCollectStack, destroyAfter);
        }

        public void PlaySound(OpponentAudioType opponentAudioType)
        {
            if(!_opponentAudioController) return;
            _opponentAudioController.PlaySound(opponentAudioType);
        }

        #endregion
    }
}
