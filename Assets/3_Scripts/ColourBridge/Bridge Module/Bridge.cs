using System;
using System.Collections.Generic;
using UnityEngine;
using Funlary.MeshGenerationModule;
using Funlary.MeshGenerationModule.Enum;
using Funlary.Unit5.StackModule;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    public class Bridge : MonoBehaviour
    {
        #region FIELDS

        private StackManager _stackManager;
        [SerializeField] private StepManager stepManager;
        [SerializeField] private ColorData colorData;

        [Header("Indicator Transforms")] 
        [SerializeField] private Transform nextGround;
        [SerializeField] private Transform stairParent;
        public Transform startPoint;
        public Transform endPoint;

        [Header("Prefabs")]
        [SerializeField] private GameObject stepPrefab;
        public PoleController rightPole;
        public PoleController leftPole;

        [Header("Materials Will Change Renderers")]
        [SerializeField] private MeshRenderer[] willChangeMeshRenderers;
        [SerializeField] private LineRenderer[] willChangeLineRenderers;

        #endregion

        #region VARIABLES
        [Header("Private Variables")]
        private float BridgeWidth = 3.0f;
        private float BridgeLength = 20.0f;
        private float BridgeHeight = 5.0f;
        private float StepHeight;
        #endregion

        #region UNITY FUNCTIONS
        private void Start()
        {
            Arena currentArena = GetComponentInParent<Arena>();
            Arena nextArena = currentArena.ArenaManager.GetNextArena(currentArena.index);
            
            endPoint.position = new Vector3(endPoint.position.x, nextArena.transform.position.y + 0.5f,
                nextArena.transform.position.z - nextArena.transform.GetChild(0).localScale.z / 2);
            
            BridgeLength = endPoint.localPosition.z - startPoint.localPosition.z;
            BridgeHeight = endPoint.localPosition.y - startPoint.localPosition.y;
            
            StepHeight = BridgeHeight / BridgeLength;

            MeshGeneration.Instance.CreateMesh(
                MeshType.BRIDGE_ROPE, MeshRotationType.UP,
                BridgeWidth, BridgeLength, BridgeHeight, startPoint.position,
                startPoint, endPoint, transform
                );

            InitializeBridge();

            rightPole.SetEndPolePosition(new Vector3(0, BridgeHeight, BridgeLength));
            leftPole.SetEndPolePosition(new Vector3(0, BridgeHeight, BridgeLength));
        }
        
        #endregion

        #region BRIDGE
        public Material GetBridgeColorMaterial(ColorType colorType)
        {
            return colorData.ColorType[colorType];
        }

        private void InitializeBridge()
        {
            stepManager.bridge = this;
            Vector3 stepPosition = new Vector3(0, 0, 0.5f);
            Vector3 stepScale = new Vector3(3, StepHeight, 1);

            List<IStep> stepList = new List<IStep>();
            for (int i = 0; i < BridgeLength; i++)
            {
                IStep IStep = Instantiate(stepPrefab, stairParent).GetComponent<IStep>();
                stepList.Add(IStep);

            }
            stepList.Add(stepList[stepList.Count - 1]);
            for (int i = 0; i < BridgeLength; i++)
            {
                stepManager.CreateStep(stepList[i], stepList[i + 1], stepPosition, stepScale, i);
                stepPosition += new Vector3(0, StepHeight, 1);
            }
        }

        public void ChangeBridgeColor(Material newMaterial)
        {
            foreach (var item in willChangeMeshRenderers) item.material = newMaterial;
            foreach (var item in willChangeLineRenderers) item.material = newMaterial;
        }

        public void SetStackManager(StackManager stackManager)
        {
            _stackManager = stackManager;
        }

        #endregion
    }
}