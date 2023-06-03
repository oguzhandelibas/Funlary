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
        private List<IStep> StepList = new List<IStep>();
        
        public int ID { get { return ID;} set { ID = value; } }
        public int ActiveStepCount { private get; set; } = -1;
        public Color StepColor { set => StepColor = value; get => StepColor; }
        [SerializeField] private GameObject wallObject;
        public void CreateStep(IStep step, Vector3 stepPosition, Vector3 stepScale)
        {
            StepList.Add(step);
            step.InitializeStep(stepPosition, stepScale);
            step.SetActiveness(false,false);
            wallObject.SetActive(true);
        }
        
        public bool CheckId(int x)
        {//eğer eşse color ataması gerekli yerden yapılsın
            bool value = (x == ID);
            
            if (!value)
            {
                ID = x;
                ActiveStepCount = -1;
                ActivateNextStep();
            }
            
            return value;
        }

        private void ActivateNextStep()
        {
            if (ActiveStepCount >= StepList.Count) return;
            
            ActiveStepCount++;
            
            if(ActiveStepCount > 0) StepList[ActiveStepCount].SetActiveness(false,true);
            StepList[ActiveStepCount].SetActiveness(true, true);
            StepList[ActiveStepCount].SetColor(StepColor);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            print("a");
            if (other.transform.TryGetComponent(out OpponentPhysicController opponentPhysicController) && opponentPhysicController.opponent.HasStack)
            {
                print("b");
                wallObject.SetActive(!opponentPhysicController.opponent.HasStack);
                if (ActiveStepCount <= 0)
                {
                    // Yeni başlıyor, beyazdan asıl renge geçiş
                    
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
