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

        [SerializeField] private StepManager stepManager;
        [SerializeField] private StackManager stackManager;
        [SerializeField] private ColorData colorData;

        [Header("Indicator Transforms")]
        [SerializeField] private Transform stairParent;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;

        [Header("Prefabs")]
        [SerializeField] private GameObject stepPrefab;
        [SerializeField] private PoleController leftPole;
        [SerializeField] private PoleController rightPole;

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
            BridgeLength = endPoint.localPosition.z - startPoint.localPosition.z;
            BridgeHeight = endPoint.localPosition.y - startPoint.localPosition.y;
            StepHeight = BridgeHeight / BridgeLength;

            MeshGeneration.Instance.CreateMesh(
                MeshType.BRIDGE, MeshRotationType.UP,
                BridgeWidth, BridgeLength, BridgeHeight, startPoint.position,
                startPoint, endPoint, transform
                );

            InitializeBridge();

            leftPole.SetEndPolePosition(new Vector3(0, BridgeHeight + 1, BridgeLength));
            rightPole.SetEndPolePosition(new Vector3(0, BridgeHeight + 1, BridgeLength));
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

        public void SetStackManagerColorTypes(List<ColorType> colorTypes)
        {
            stackManager.SetStackManagerColorTypes(colorTypes);
        }

        #endregion
    }
}