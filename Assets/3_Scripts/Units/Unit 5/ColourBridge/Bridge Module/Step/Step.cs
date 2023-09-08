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
        [SerializeField] private ColorData colorData;

        private StepManager stepManager;
        public bool Used { get; set; }
        public IStep NextStep { get; set; }

        private ColorType _stepColorType;
        public ColorType StepColorType
        {
            get => _stepColorType;
            set
            {
                _stepColorType = value;
                meshRenderer.sharedMaterial.color = colorData.ColorType[_stepColorType];
            }
        }

        private void Start()
        {
            Material stepMaterial = meshRenderer.material;
            meshRenderer.material = stepMaterial;
        }

        public StepManager GetStepManager() => stepManager;
        public Vector3 Position() => transform.position;

        public int Index { get; set; }
        
        public void InitializeStep(StepManager _stepManager, IStep _nextStep, Vector3 _localPos, Vector3 _localScale, int index)
        {
            stepManager = _stepManager;
            NextStep = _nextStep;
            Index = index;
            transform.localPosition = _localPos;
            transform.localScale = _localScale;
        }

        public void SetActiveness(bool gameObjectActiveness, bool wallObjectActiveness)
        {
            gameObject.SetActive(gameObjectActiveness); 
            wallObject.SetActive(wallObjectActiveness);
        }
    }
}
