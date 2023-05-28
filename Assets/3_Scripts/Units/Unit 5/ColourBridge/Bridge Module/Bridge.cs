using System.Collections.Generic;
using UnityEngine;
using Funlary.Unit5.ColourBridge.MeshGenerationModule;
using Funlary.Unit5.ColourBridge.MeshGenerationModule.Enum;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    public class Bridge : MonoBehaviour
    {
        [Header("Indicator Transforms")]
        [SerializeField] private Transform stairParent; 
        [SerializeField] private Transform startPoint; 
        [SerializeField] private Transform endPoint;

        [Header("Prefabs")]
        [SerializeField] private GameObject stepPrefab;
        
        [Header("Privete Lists")]
        private List<Step> stair = new List<Step>();
        
        [Header("Privete Variables")]
        private float BridgeWidth = 3.0f;
        private float BridgeLength = 20.0f;
        private float BridgeHeight = 5.0f;
        private float StepHeight;

        private void Start()
        {
            BridgeLength = endPoint.position.z - startPoint.position.z;
            BridgeHeight = endPoint.position.y - startPoint.position.y;
            StepHeight = BridgeHeight / BridgeLength;
            
            MeshGeneration.Instance.CreateMesh(MeshType.PLANE, startPoint, endPoint, BridgeWidth, BridgeLength, BridgeHeight, transform);

            InitializeBridge();
        }

        private void InitializeBridge()
        {
            Vector3 stepPosition = new Vector3(0,0,0.5f);
            Vector3 stepScale = new Vector3(3, StepHeight, 1);
            
            for (int i = 0; i < BridgeLength; i++)
            {
                Step step = Instantiate(stepPrefab, stairParent).GetComponent<Step>();
                stair.Add(step);
                step.InitializeStep(stepPosition, stepScale);
                
                stepPosition += new Vector3(0, StepHeight, 1);
            }
        }
    }
}
