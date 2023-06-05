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
        public int ID { get { return ID;} set { ID = value; } }
        public int ActiveStepCount { private get; set; } = -1;
        public BridgeColor BridgeColor;
        
        
        public void CreateStep(Bridge _bridge, IStep step, Vector3 stepPosition, Vector3 stepScale, int index)
        {
            bridge = _bridge;
            StepList.Add(step);
            step.InitializeStep(this, stepPosition, stepScale, index);
            step.SetActiveness(false,false);
        }
        
        public bool CheckId(int x)
        {//eğer eşse color ataması gerekli yerden yapılsın
            bool value = (x == ID);
            
            if (!value)
            {
                ID = x;
                ActiveStepCount = -1;
                //ActivateNextStep();
            }
            
            return value;
        }

        public void ActivateNextStep(int stepIndex)
        {
            if(stepIndex >= StepList.Count) return;
            StepList[stepIndex].SetActiveness(true, false);
            StepList[stepIndex].SetColor(bridge.GetBridgeColorMaterial(BridgeColor));
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent(out OpponentPhysicController opponentPhysicController) && opponentPhysicController.opponent.HasStack)
            {
                bool hasStack = opponentPhysicController.opponent.HasStack;
                BridgeWall.SetActive(!hasStack);
                if (hasStack)
                {
                    ActivateNextStep(0);
                    opponentPhysicController.opponent.StackCount--;
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
