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
        public int ID;
        private bool Used;
        public int ActiveStepCount { private get; set; } = -1;
        public BridgeColor BridgeColor;
        
        
        public void CreateStep(Bridge _bridge, IStep step, Vector3 stepPosition, Vector3 stepScale, int index)
        {
            bridge = _bridge;
            StepList.Add(step);
            step.InitializeStep(this, stepPosition, stepScale, index);
            step.SetActiveness(false,true);
        }
        
        
        public bool ActivateStep(int stepIndex)
        {
            if(stepIndex >= StepList.Count || StepList[stepIndex].Used) return false;
            
//            if (stepIndex == 0)
            StepList[stepIndex].Used = true;
            StepList[stepIndex].SetActiveness(true, false);
            StepList[stepIndex+1].SetActiveness(true, true);
            StepList[stepIndex].SetColor(bridge.GetBridgeColorMaterial(BridgeColor));
            return true;
        }
        
        public bool CheckId(int x)
        {
            //eğer eşse color ataması gerekli yerden yapılsın
            bool value = (x == ID);
            
            if (!value)
            {
                ID = x;
            }
            
            ActivateStep(0);
            
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
                    ActivateStep(0);
                    opponentPhysicController.opponent.stackController.RemoveStack(StepList[0].Position());
                    Used = true;
                }
                
                if (ActiveStepCount <= 0)
                {
                    // Yeni başlıyor, beyazdan asıl renge geçiş
                    //opponent'ten veri al, step oluştur.
                }
                else
                {
                    // Üzerine yazıyor, smooth renk değişimi
                    
                }

            }
            // trigger eden karakterin id'si kontrol edilsin, aynı ise bir şey yok
            // değil ise index sıfırlansın ve objeler yeniden renklendirilmeye başlansın.
        }
    }
}
