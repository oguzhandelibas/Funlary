using System;
using System.Collections;
using System.Collections.Generic;
using Funlary.Unit5.OpponentModule;
using Funlary.Unit5.OpponentModule.Controller;
using UnityEngine;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    public class StepManager : MonoBehaviour
    {
        private Bridge bridge;
        private List<IStep> StepList = new List<IStep>();
        [SerializeField] private GameObject BridgeWall;
        public int ownerID;
        private bool Used;
        public int ActiveStepCount { private get; set; } = 0;
        public BridgeColor BridgeColor;
        
        
        public void CreateStep(Bridge _bridge, IStep step, IStep nextStep, Vector3 stepPosition, Vector3 stepScale, int index)
        {
            bridge = _bridge;
            StepList.Add(step);
            step.InitializeStep(this, nextStep,stepPosition, stepScale, index);
            step.SetActiveness(false,true);
        }
        
        
        public bool ActivateStep(int stepIndex)
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
            

            if (stepIndex+1 < StepList.Count) 
                StepList[stepIndex+1].SetActiveness(true, true);
            
            return true;
        }
        
        public bool IsSameOpponent(int x)
        {
            //eğer eşse color ataması gerekli yerden yapılsın
            bool value = (x == ownerID);
            
            if (!value)
            {
                ownerID = x;
                ActivateStep(0);
            }
            
            return value;
        }
        
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
    }
}
