using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Funlary.MeshGenerationModule.Enum;
using Funlary.Unit5.ColourBridge.BridgeModule;
using Funlary.Unit5.StackModule;
using UnityEngine;

namespace Funlary
{
    public class GroundBounds : MonoBehaviour
    {
        [Header("Enter Door Properties")]
        [SerializeField] private bool HasEnterDoor;
        public List<Bridge> enterDoorBridges;

        [Header("Exit Door Properties")]
        [SerializeField] private bool HasExitDoor;
        [SerializeField] private int exitDoorCount;

        [Header("Fields")]
        [SerializeField] private Transform arenaTransform;
        [SerializeField] private Transform bridgeParent;

        [SerializeField] private StackManager stackManager;
        [SerializeField] private Bridge bridgePrefab;
        [SerializeField] private PoleController poleControllerPrefab;

        [SerializeField] private GroundBounds nextGroundBounds;
        [SerializeField] private List<Bridge> bridges;


        public void SetEnterDoorBridges(List<Bridge> enterDoorbridgeList)
        {
            enterDoorBridges = enterDoorbridgeList;
        }

        public async Task DeleteArenaBound()
        {
            List<Transform> childrenToDestroy = new List<Transform>();

            foreach (Transform child in transform)
            {
                childrenToDestroy.Add(child);
            }

            foreach (Transform child in bridgeParent)
            {
                childrenToDestroy.Add(child);
            }

            foreach (Transform child in childrenToDestroy)
            {
                DestroyImmediate(child.gameObject);
                await Task.Delay(100);
            }
        }
        public async Task SetArenaBound()
        {
            if (!bridgeParent)
            {
                bridgeParent = new GameObject().transform;
                bridgeParent.name = "Bridges";
            }
            bridgeParent.parent = null;
            bridgeParent.localScale = new Vector3(1, 1, 1);
            bridgeParent.parent = transform.parent;
            bridgeParent.transform.localPosition = new Vector3(0,0,0);

            DeleteArenaBound();
            await Task.Delay(250);
            CreateEnterWalls();
            await Task.Delay(100);
            CreateLeftRightWalls();
            await Task.Delay(100);
            CreateExitWalls();
        }
        private void CreateEnterWalls()
        {
            if (HasEnterDoor)
            {
                for (int i = 0; i <= enterDoorBridges.Count; i++)
                {
                    PoleController poleController = Instantiate(poleControllerPrefab, transform);
                    poleController.name = "Enter_PoleController_" + i;
                    poleController.transform.localPosition = Vector3.zero;

                    if (i==0)
                    {
                        poleController.startPoint.localPosition = new Vector3(-arenaTransform.localScale.x / 2, 1, -arenaTransform.localScale.x / 2);
                    }
                    else
                    {
                        poleController.startPoint.localPosition = new Vector3(enterDoorBridges[i-1].transform.position.x + 1.5f, 1, -arenaTransform.localScale.x / 2);
                    }

                    if (i == enterDoorBridges.Count)
                    {
                        poleController.endPoint.localPosition = new Vector3(arenaTransform.localScale.x / 2, 1, -arenaTransform.localScale.x / 2);
                    }
                    else
                    {
                        poleController.endPoint.localPosition = new Vector3(enterDoorBridges[i].transform.position.x - 1.5f, 1, -arenaTransform.localScale.x / 2);
                    }
                    
                    poleController.startPoint.GetComponent<MeshRenderer>().enabled = false;
                    poleController.endPoint.GetComponent<MeshRenderer>().enabled = false;

                    poleController.CreateMeshAndRope(MeshType.HORIZONTAL_WALL, poleController.endPoint.position.x -
                                                                               poleController.startPoint.position.x);
                }
            }
            else
            {
                PoleController Enter_PoleController = Instantiate(poleControllerPrefab, transform);

                Enter_PoleController.name = "Enter_PoleController";

                Enter_PoleController.transform.localPosition = Vector3.zero;

                Enter_PoleController.startPoint.localPosition = new Vector3(-arenaTransform.localScale.x / 2, 1, -arenaTransform.localScale.x / 2);
                Enter_PoleController.endPoint.localPosition = new Vector3(arenaTransform.localScale.x / 2, 1, -arenaTransform.localScale.x / 2);

                Enter_PoleController.startPoint.GetComponent<MeshRenderer>().enabled = false;
                Enter_PoleController.endPoint.GetComponent<MeshRenderer>().enabled = false;

                float enterWallLength = Enter_PoleController.endPoint.position.x -
                                        Enter_PoleController.startPoint.position.x;

                Enter_PoleController.CreateMeshAndRope(MeshType.HORIZONTAL_WALL, enterWallLength);

            }
        }

        private void CreateBridges()
        {
            bridges.Clear();
            float increaseAmount = 10;
            float horizontalPos;
            int leftPiece = exitDoorCount / 2;
            int rightPiece = leftPiece;

            for (int i = -leftPiece; i <= rightPiece; i++)
            {
                if (exitDoorCount % 2 == 0)
                {
                    if (i == 0) continue;
                    else
                    {
                        float multiplier = -1;
                        if (i < 0) multiplier = 1;
                        horizontalPos = (i * increaseAmount) + ((multiplier) * (increaseAmount / 2));
                    }
                }
                else
                {
                    horizontalPos = (i * increaseAmount);
                }


                Bridge bridge = Instantiate(bridgePrefab, bridgeParent);
                bridge.transform.position += Vector3.right * horizontalPos + Vector3.forward * (arenaTransform.localScale.z / 2);
                bridge.SetStackManager(stackManager);
                bridges.Add(bridge);
            }

            nextGroundBounds?.SetEnterDoorBridges(bridges);
        }

        private void CreateExitWalls()
        {
            if (HasExitDoor)
            {
                CreateBridges();

                for (int i = 0; i <= exitDoorCount; i++)
                {
                    PoleController poleController =  Instantiate(poleControllerPrefab, transform);
                    poleController.name = "Exit_PoleController_" + i;
                    poleController.transform.localPosition = Vector3.zero;;


                    if (i == 0)
                    {
                        poleController.startPoint.localPosition = new Vector3(-arenaTransform.localScale.x / 2, 1, arenaTransform.localScale.x / 2);
                    }
                    else
                    {
                        poleController.startPoint.localPosition = new Vector3(bridges[i-1].transform.position.x + 1.5f, 1, arenaTransform.localScale.x / 2);
                    }

                    if (i == exitDoorCount)
                    {
                        poleController.endPoint.localPosition = new Vector3(arenaTransform.localScale.x / 2, 1, arenaTransform.localScale.x / 2);
                    }
                    else
                    {
                        poleController.endPoint.localPosition = new Vector3(bridges[i].transform.position.x - 1.5f, 1, arenaTransform.localScale.x / 2);
                    }
                    
                    poleController.startPoint.GetComponent<MeshRenderer>().enabled = false;
                    poleController.endPoint.GetComponent<MeshRenderer>().enabled = false;

                    poleController.CreateMeshAndRope(MeshType.HORIZONTAL_WALL, poleController.endPoint.position.x - poleController.startPoint.position.x);
                }
            }
            else
            {
                PoleController Exit_PoleController = Instantiate(poleControllerPrefab, transform);

                Exit_PoleController.name = "Exit_PoleController";
                Exit_PoleController.transform.localPosition = Vector3.zero;

                Exit_PoleController.startPoint.localPosition = new Vector3(-arenaTransform.localScale.x / 2, 1, arenaTransform.localScale.z / 2);
                Exit_PoleController.endPoint.localPosition = new Vector3(arenaTransform.localScale.x / 2, 1, arenaTransform.localScale.z / 2);

                Exit_PoleController.startPoint.GetComponent<MeshRenderer>().enabled = false;
                Exit_PoleController.endPoint.GetComponent<MeshRenderer>().enabled = false;

                Exit_PoleController.CreateMeshAndRope(MeshType.HORIZONTAL_WALL, Exit_PoleController.endPoint.position.x - Exit_PoleController.startPoint.position.x);
            }
        }

        private void CreateLeftRightWalls()
        {
            PoleController Right_PoleController = Instantiate(poleControllerPrefab, transform);
            PoleController Left_PoleController = Instantiate(poleControllerPrefab, transform);

            Right_PoleController.name = "Right_PoleController";
            Left_PoleController.name = "Left_PoleController";

            Right_PoleController.transform.localPosition = Vector3.zero;
            Left_PoleController.transform.localPosition = Vector3.zero;

            Right_PoleController.startPoint.localPosition = new Vector3(arenaTransform.localScale.x / 2, 1, -arenaTransform.localScale.z / 2);
            Right_PoleController.endPoint.localPosition = new Vector3(arenaTransform.localScale.x / 2, 1, arenaTransform.localScale.z / 2);
            Left_PoleController.startPoint.localPosition = new Vector3(-arenaTransform.localScale.x / 2, 1, -arenaTransform.localScale.z / 2);
            Left_PoleController.endPoint.localPosition = new Vector3(-arenaTransform.localScale.x / 2, 1, arenaTransform.localScale.z / 2);
            

            Right_PoleController.CreateMeshAndRope(MeshType.VERTICAL_WALL, Right_PoleController.endPoint.position.z - Right_PoleController.startPoint.position.z);
            Left_PoleController.CreateMeshAndRope(MeshType.VERTICAL_WALL, Left_PoleController.endPoint.position.z - Left_PoleController.startPoint.position.z);
        }
    }
}