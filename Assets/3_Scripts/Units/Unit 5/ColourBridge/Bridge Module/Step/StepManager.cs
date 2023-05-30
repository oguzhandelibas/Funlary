using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    public class StepManager : MonoBehaviour
    {
        private List<IStep> IStep = new List<IStep>();
        
        public int ID { get { return ID;} set { ID = value; } }
        public int Index { private get; set; } = -1;
        public Color StepColor { set => StepColor = value; get => StepColor; }
       


        public bool CheckId(int x)
        {//eğer eşse color ataması gerekli yerden yapılsın
            bool value = (x == ID);
            
            if (!value)
            {
                ID = x;
                Index = -1;
                ActivateNextStep();    
            }
            
            return value;
        }

        public void AddStep(IStep step, Vector3 stepPosition, Vector3 stepScale)
        {
            IStep.Add(step);
            step.InitializeStep(stepPosition, stepScale);
            step.Deactivate();
        }
        
        private void ActivateNextStep()
        {
            if (Index >= IStep.Count) return;
            if(Index > 0) IStep[Index].Hide();
            
            Index++;
            
            IStep[Index].Show();
            IStep[Index].Activate();
            IStep[Index].SetColor(StepColor);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            // trigger eden karakterin id'si kontrol edilsin, aynı ise bir şey yok
            // değil ise index sıfırlansın ve objeler yeniden renklendirilmeye başlansın.
        }
    }
}
