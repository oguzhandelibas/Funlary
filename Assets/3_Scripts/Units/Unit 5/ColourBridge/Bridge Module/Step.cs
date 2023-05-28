using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    public class Step : MonoBehaviour
    {
        [SerializeField] private BoxCollider boxCollider;

        private void Start()
        {
            ColliderActiveness(true);
        }

        public void ColliderActiveness(bool activate)
        {
            boxCollider.isTrigger = !activate;
        }
        
        public void InitializeStep(Vector3 _localPos, Vector3 _localScale)
        {
            transform.localPosition = _localPos;
            transform.localScale = _localScale;
        }
    }
}
