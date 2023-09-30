using System;
using System.Collections;
using System.Collections.Generic;
using Funlary.InventoryModule;
using Funlary.UIModule.Game;
using Funlary.Unit5.OpponentModule;
using Funlary.Unit5.OpponentModule.Controller;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    public class StepManager : MonoBehaviour, IColor
    {
        #region FIELDS
        [SerializeField] private ColorData colorData;
        [SerializeField] private GameObject bridgeShieldWall;
        [HideInInspector] public Bridge bridge;
        #endregion

        #region VARIABLES
        public int ownerID;
        public ColorType bridgeColorType = ColorType.None;
        public int ActiveStepCount { private get; set; } = 0;
        private List<IStep> _stepList = new List<IStep>();
        private List<IStep> _usedStepList = new List<IStep>();
        #endregion

        #region PROPERTIES

        public bool CheckColor(ColorType targetColor) => targetColor == this.bridgeColorType;

        public Material GetColor
        {
            get => colorData.ColorType[bridgeColorType];
        }

        #endregion

        #region UNITY FUNCTIONS
        private void OnTriggerEnter(Collider other)
        {
            BrdigeConstruction(other);
        }
        #endregion

        #region BRIDGE CONSTRUCTION

        private void BrdigeConstruction(Collider other)
        {
            if (other.transform.TryGetComponent(out OpponentPhysicsController opponentPhysicController))
            {
                ColorType opponentColorType = opponentPhysicController.opponent.ColorType;

                if (bridgeColorType == opponentColorType) // Karakter ile köprü rengi denk [Karakterde stack olmasa bile kullanabilmesi için var]
                {
                    bridgeShieldWall.SetActive(false);
                    return;
                }
                else if (bridgeColorType == ColorType.None) // Köprü daha önce hiç kullanılmamış
                {
                    if (!opponentPhysicController.opponent.HasStack) return;
                    SetFirstStep(opponentColorType, opponentPhysicController.opponent);
                }
                else if (this.bridgeColorType != opponentColorType) // Köprü başka opponent tarafından oluşturulmuş
                {
                    if (!opponentPhysicController.opponent.HasStack)
                    {
                        bridgeShieldWall.SetActive(true);
                    }
                    else
                    {
                        SetFirstStep(opponentColorType, opponentPhysicController.opponent);
                    }
                }
            }
        }

        private void SetFirstStep(ColorType opponentColorType, Opponent opponent)
        {
            bridge.ChangeBridgeColor(colorData.ColorType[opponentColorType]);
            bridgeShieldWall.SetActive(false);
            bridgeColorType = opponentColorType;

            ActivateStep(-1, 10, GetColor, bridgeColorType);
            opponent.OpponentStackController.RemoveStack(_stepList[0].Position());
        }

        public void CreateStep(IStep step, IStep nextStep, Vector3 stepPosition, Vector3 stepScale, int index)
        {
            _stepList.Add(step);
            step.InitializeStep(this, nextStep, stepPosition, stepScale, index);
            step.SetActiveness(false, true);
        }

        public bool ActivateStep(int stepIndex, int remainingStepCount, Material material, ColorType colorType)
        {
            stepIndex++;

            if (stepIndex >= _stepList.Count)
            {
                return false;
            }

            _stepList[stepIndex].Used = false;

            if (stepIndex == 0)
            {
                _stepList[stepIndex].Used = false;
                _stepList[stepIndex].SetActiveness(true, true);
            }
            else if ((stepIndex + 1) <= _stepList.Count)
            {
                _stepList[stepIndex - 1].Used = true;
                _stepList[stepIndex].SetActiveness(true, remainingStepCount < 2 && (stepIndex + 1) != _stepList.Count);
                _stepList[stepIndex - 1].SetActiveness(true, false);
            }

            _stepList[stepIndex].StepColorType = colorType;
            _usedStepList.Add(_stepList[stepIndex]);

            return true;
        }

        #endregion
    }
}