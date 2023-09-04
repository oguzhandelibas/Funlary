using System;
using System.Collections;
using System.Collections.Generic;
using Funlary.Unit5.OpponentModule;
using Funlary.Unit5.OpponentModule.Controller;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    public class StepManager : MonoBehaviour, IColor
    {
        public int ownerID;
        public ColorType bridgeColorType = ColorType.None;
        public int ActiveStepCount { private get; set; } = 0;
        public List<IStep> _stepList = new List<IStep>();
        public List<IStep> _usedStepList = new List<IStep>();
        [SerializeField] private ColorData colorData;
        [SerializeField] private GameObject bridgeShieldWall;
        private Bridge _bridge;

        public void CreateStep
            (Bridge bridge, IStep step, IStep nextStep, Vector3 stepPosition, Vector3 stepScale, int index)
        {
            _bridge = bridge;
            _stepList.Add(step);
            step.InitializeStep(this, nextStep,stepPosition, stepScale, index);
            step.SetActiveness(false,true);
        }

        public bool ActivateStep(int stepIndex, Color color, ColorType colorType)
        {
            print("buurdayım be burdayım:" + stepIndex);
            //Debug.Log($"Index: {stepIndex} & Total List Count: {_stepList.Count}");
            if (stepIndex > _stepList.Count || _stepList[stepIndex].Used) return false;
            
            ActiveStepCount++;

            _stepList[stepIndex].Used = true;
            _stepList[stepIndex].SetActiveness(true, false);

            _stepList[stepIndex].SetColor(color, colorType);
            _usedStepList.Add(_stepList[stepIndex]);

            
            if (stepIndex + 1 < _stepList.Count)
            {
                _stepList[stepIndex + 1].SetActiveness(false, true);
            }


            return true;
        }

        public bool CheckColor(ColorType targetColor) => targetColor == this.bridgeColorType;
        public Color GetColor() => colorData.ColorType[bridgeColorType];
        /*
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent(out OpponentPhysicController opponentPhysicController) && opponentPhysicController.opponent.HasStack)
            {
                bool hasStack = opponentPhysicController.opponent.HasStack;
                bridgeShieldWall.SetActive(!hasStack);
                if (hasStack && !Used)
                {
                    ActivateStep(-1);
                    opponentPhysicController.opponent.stackController.RemoveStack(_stepList[0].Position());
                    Used = true;
                }

                if (ActiveStepCount <= 0)
                {
                    // Yeni başlıyor, beyazdan asıl renge geçiş
                    // Opponent'ten veri al, step oluştur.
                }
                else
                {
                    // Yabancı geldi üzerine yazıyor, smooth renk değişimi
                }


            }
            // trigger eden karakterin id'si kontrol edilsin, aynı ise bir şey yok
            // değil ise index sıfırlansın ve objeler yeniden renklendirilmeye başlansın.
        }
        */
        private void OnTriggerEnter(Collider other)
        {
            BrdigeConstruction(other);
        }

        private void BrdigeConstruction(Collider other)
        {
            if (other.transform.TryGetComponent(out OpponentPhysicController opponentPhysicController) && opponentPhysicController.opponent.HasStack)
            {
                Debug.Log("<color=yellow>Has Stack</color>");
                ColorType opponentColorType = opponentPhysicController.opponent.ColorType;

                if (this.bridgeColorType == ColorType.None)
                {
                    // Sıfırdan inşa süreci
                    bridgeShieldWall.SetActive(false);
                    bridgeColorType = opponentColorType;
                    ActivateStep(0, GetColor(), bridgeColorType);
                    opponentPhysicController.opponent.stackController.RemoveStack(_stepList[0].Position());

                    Debug.Log("<color=green>Bridge Creation is Started</color>");
                }
            }
            else
            {
                Debug.Log("<color=black>No Stack</color>");
            }
        }
    }
}
