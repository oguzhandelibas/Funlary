using System.Collections.Generic;
using UnityEngine;
using Funlary.MeshGenerationModule;
using Funlary.MeshGenerationModule.Enum;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    public class Bridge : MonoBehaviour
    {
        [SerializeField] private StepManager stepManager;
        
        [Header("Indicator Transforms")]
        [SerializeField] private Transform stairParent; 
        [SerializeField] private Transform startPoint; 
        [SerializeField] private Transform endPoint;

        [Header("Prefabs")]
        [SerializeField] private GameObject stepPrefab;

        [Header("Privete Variables")]
        private float BridgeWidth = 3.0f;
        private float BridgeLength = 20.0f;
        private float BridgeHeight = 5.0f;
        private float StepHeight;

        [SerializeField] private PoleController leftPole;
        [SerializeField] private PoleController rightPole;

        private void Start()
        {
            BridgeLength = endPoint.position.z - startPoint.position.z;
            BridgeHeight = endPoint.position.y - startPoint.position.y;
            StepHeight = BridgeHeight / BridgeLength;
            
            MeshGeneration.Instance.CreateMesh(
                MeshType.PLANE, MeshRotationType.UP, 
                BridgeWidth, BridgeLength, BridgeHeight, 
                startPoint, endPoint,transform
                );

            InitializeBridge();

            leftPole.CreateRope(new Vector3(0, BridgeHeight + 1, BridgeLength));
            rightPole.CreateRope(new Vector3(0, BridgeHeight + 1, BridgeLength));
        }

        private void InitializeBridge()
        {
            Vector3 stepPosition = new Vector3(0,0,0.5f);
            Vector3 stepScale = new Vector3(3, StepHeight, 1);

            for (int i = 0; i < BridgeLength; i++)
            {
                IStep IStep = Instantiate(stepPrefab, stairParent).GetComponent<IStep>();
                stepManager.AddStep(IStep, stepPosition, stepScale);
                
                stepPosition += new Vector3(0, StepHeight, 1);
            }
        }
    }
}
