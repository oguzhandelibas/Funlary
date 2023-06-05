using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    public class Step : MonoBehaviour, IStep
    {
        [SerializeField] private BoxCollider boxCollider;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private GameObject wallObject;
        private StepManager stepManager;

        public StepManager GetStepManager()
        {
            return stepManager;
        }

        public int Index { get; set; }

        public void InitializeStep(StepManager _stepManager, Vector3 _localPos, Vector3 _localScale, int index)
        {
            stepManager = _stepManager;
            Index = index;
            transform.localPosition = _localPos;
            transform.localScale = _localScale;
        }

        public void SetActiveness(bool gameObjectActiveness, bool wallObjectActiveness)
        {
            gameObject.SetActive(gameObjectActiveness);
            wallObject.SetActive(wallObjectActiveness);
        }
        

        public void SetColor(Material colorMaterial)
        {
            meshRenderer.sharedMaterial = colorMaterial;

        }
    }
}
