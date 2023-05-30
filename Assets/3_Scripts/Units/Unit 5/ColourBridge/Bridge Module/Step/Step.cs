using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    public class Step : MonoBehaviour, IStep
    {
        [SerializeField] private BoxCollider boxCollider;
        [SerializeField] private MeshRenderer meshRenderer;
        public void InitializeStep(Vector3 _localPos, Vector3 _localScale)
        {
            transform.localPosition = _localPos;
            transform.localScale = _localScale;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Activate()
        {
            Show();
            TriggerActiveness(true);   
        }

        public void Deactivate()
        {
            TriggerActiveness(false); 
        }
        
        public void TriggerActiveness(bool activate)
        {
            boxCollider.isTrigger = activate;
        }

        public void SetColor(Color color)
        {
            meshRenderer.sharedMaterial.color = color;
        }
    }
}
