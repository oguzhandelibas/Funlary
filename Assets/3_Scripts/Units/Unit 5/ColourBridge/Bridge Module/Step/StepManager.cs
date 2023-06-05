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
        [SerializeField] private GameObject BridgeWall;
        public int ID { get { return ID;} set { ID = value; } }
        public int ActiveStepCount { private get; set; } = -1;
        public Color StepColor { set => StepColor = value; get => StepColor; }
        
        
        public void CreateStep(IStep step, Vector3 stepPosition, Vector3 stepScale, int index)
        {
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

        private void ActivateNextStep(int stepIndex)
        {
            if(stepIndex >= StepList.Count) return;
            StepList[stepIndex].SetActiveness(true, false);
            StepList[stepIndex].SetColor(StepColor);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent(out OpponentPhysicController opponentPhysicController) && opponentPhysicController.opponent.HasStack)
            {
                BridgeWall.SetActive(!opponentPhysicController.opponent.HasStack);
                
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
