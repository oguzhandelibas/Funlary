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
        private Bridge bridge;
        private List<IStep> StepList = new List<IStep>();
        [SerializeField] private GameObject BridgeWall;
        public int ownerID;
        private bool Used;
        public int ActiveStepCount { private get; set; } = 0;
        public ColorType BridgeColor = ColorType.None;
        [SerializeField] private ColorData colorData;
        public void CreateStep(Bridge _bridge, IStep step, IStep nextStep, Vector3 stepPosition, Vector3 stepScale, int index)
        {
            bridge = _bridge;
            StepList.Add(step);
            step.InitializeStep(this, nextStep,stepPosition, stepScale, index);
            step.SetActiveness(false,true);
        }
        
        
        public bool ActivateStep(int stepIndex, Color color)
        {
            //Debug.Log($"Index: {stepIndex} & Total List Count: {StepList.Count}");
            ActiveStepCount++;
            Debug.Log("Active Step Count : " + ActiveStepCount);
            if (stepIndex == -1)
            {
                StepList[stepIndex+1].SetActiveness(true, true);
                return true;
            }
            
            if(stepIndex > StepList.Count || StepList[stepIndex].Used) return false;
            
            StepList[stepIndex].Used = true;
            StepList[stepIndex].SetActiveness(true, false);
            StepList[stepIndex].SetColor(color);

            if (stepIndex+1 < StepList.Count) 
                StepList[stepIndex+1].SetActiveness(true, true);
            
            return true;
        }
        
        public bool CheckColor(ColorType targetColor) => targetColor == this.BridgeColor;

        public Color GetColor() => colorData.ColorType[BridgeColor];
        /*
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent(out OpponentPhysicController opponentPhysicController) && opponentPhysicController.opponent.HasStack)
            {
                bool hasStack = opponentPhysicController.opponent.HasStack;
                BridgeWall.SetActive(!hasStack);
                if (hasStack && !Used)
                {
                    ActivateStep(-1);
                    opponentPhysicController.opponent.stackController.RemoveStack(StepList[0].Position());
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
            if (other.transform.TryGetComponent(out OpponentPhysicController opponentPhysicController) && opponentPhysicController.opponent.HasStack)
            {
                Debug.Log("<color=yellow>Has Stack</color>");
                if (this.BridgeColor == ColorType.None)
                {
                    // Sıfırdan inşa süreci
                    BridgeWall.SetActive(false);
                    BridgeColor = opponentPhysicController.opponent.ColorType;
                    ActivateStep(-1, GetColor());
                    opponentPhysicController.opponent.stackController.RemoveStack(StepList[0].Position());
                    
                    Debug.Log("<color=green>Bridge Creation is Started</color>");
                }
                else if(CheckColor(opponentPhysicController.opponent.ColorType))
                {
                    // Başlatılan inşa süreci devam ettirilir
                    Debug.Log("<color=green>Same Color</color>");
                }
                else
                {
                    // İnşa başka renk ile başlatılmış, üzerine yazılacak
                    Debug.Log("<color=red>Different Color</color>");
                }

            }
            else
            {
                Debug.Log("<color=black>No Stack</color>");
            }
        }
    }
}
