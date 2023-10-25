using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Funlary.MeshGenerationModule.Enum;
using UnityEngine;

namespace Funlary
{
    public class GroundBounds : MonoBehaviour
    {
        [SerializeField] private Transform arenaTransform;
        [SerializeField] private bool HasEnterDoor;
        [SerializeField] private bool HasExitDoor;
        [SerializeField] private PoleController poleController;

        public async Task DeleteArenaBound()
        {
            List<Transform> childrenToDestroy = new List<Transform>();

            foreach (Transform child in transform)
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
            DeleteArenaBound();
            await Task.Delay(250);
            CreateEnterWalls();
            await Task.Delay(100);
            CreateExitWalls();
            await Task.Delay(100);
            CreateLeftRightWalls();
        }
        private void CreateEnterWalls()
        {
            if (HasEnterDoor)
            {
                PoleController EnterRight_PoleController = Instantiate(poleController, transform);
                PoleController EnterLeft_PoleController = Instantiate(poleController, transform);

                EnterRight_PoleController.name = "EnterRight_PoleController";
                EnterLeft_PoleController.name = "EnterLeft_PoleController";

                EnterRight_PoleController.transform.localPosition = Vector3.zero;
                EnterLeft_PoleController.transform.localPosition = Vector3.zero;

                EnterRight_PoleController.startPoint.localPosition = new Vector3(arenaTransform.localScale.x / 2, 1, -arenaTransform.localScale.x / 2);
                EnterRight_PoleController.endPoint.localPosition = new Vector3(1.5f, 1, -arenaTransform.localScale.x / 2);
                EnterRight_PoleController.startPoint.GetComponent<MeshRenderer>().enabled = false;
                EnterRight_PoleController.endPoint.GetComponent<MeshRenderer>().enabled = false;

                EnterLeft_PoleController.startPoint.localPosition = new Vector3(-1.5f, 1, -arenaTransform.localScale.x / 2);
                EnterLeft_PoleController.endPoint.localPosition = new Vector3(-arenaTransform.localScale.x / 2, 1,-arenaTransform.localScale.x / 2);
                EnterLeft_PoleController.startPoint.GetComponent<MeshRenderer>().enabled = false;
                EnterLeft_PoleController.endPoint.GetComponent<MeshRenderer>().enabled = false;

                float rightWallLength = EnterRight_PoleController.endPoint.position.x -
                                        EnterRight_PoleController.startPoint.position.x;
                float leftWallLength = EnterLeft_PoleController.endPoint.position.x -
                                      EnterLeft_PoleController.startPoint.position.x;

                EnterRight_PoleController.CreateMeshAndRope(MeshType.WALL, rightWallLength);
                EnterLeft_PoleController.CreateMeshAndRope(MeshType.WALL, leftWallLength);
            }
            else
            {
                PoleController Enter_PoleController = Instantiate(poleController, transform);

                Enter_PoleController.name = "Enter_PoleController";

                Enter_PoleController.transform.localPosition = Vector3.zero;

                Enter_PoleController.startPoint.localPosition = new Vector3(arenaTransform.localScale.x / 2, 1, -arenaTransform.localScale.x / 2);
                Enter_PoleController.endPoint.localPosition = new Vector3(-arenaTransform.localScale.x / 2, 1, -arenaTransform.localScale.x / 2);

                Enter_PoleController.startPoint.GetComponent<MeshRenderer>().enabled = false;
                Enter_PoleController.endPoint.GetComponent<MeshRenderer>().enabled = false;

                float enterWallLength = Enter_PoleController.endPoint.position.x -
                                        Enter_PoleController.startPoint.position.x;

                Enter_PoleController.CreateMeshAndRope(MeshType.WALL, enterWallLength);

            }
        }
        private void CreateExitWalls()
        {
            if (HasExitDoor)
            {
                PoleController ExitRight_PoleController = Instantiate(poleController, transform);
                PoleController ExitLeft_PoleController = Instantiate(poleController, transform);

                ExitRight_PoleController.name = "ExitRight_PoleController";
                ExitLeft_PoleController.name = "ExitLeft_PoleController";

                ExitRight_PoleController.transform.localPosition = Vector3.zero;
                ExitLeft_PoleController.transform.localPosition = Vector3.zero;

                ExitRight_PoleController.startPoint.localPosition = new Vector3(arenaTransform.localScale.x / 2, 1, arenaTransform.localScale.x / 2);
                ExitRight_PoleController.endPoint.localPosition = new Vector3(1.5f, 1, arenaTransform.localScale.x / 2);
                ExitRight_PoleController.startPoint.GetComponent<MeshRenderer>().enabled = false;
                ExitRight_PoleController.endPoint.GetComponent<MeshRenderer>().enabled = false;

                ExitLeft_PoleController.startPoint.localPosition = new Vector3(-1.5f, 1, arenaTransform.localScale.x / 2);
                ExitLeft_PoleController.endPoint.localPosition = new Vector3(-arenaTransform.localScale.x / 2, 1, arenaTransform.localScale.x / 2);
                ExitLeft_PoleController.startPoint.GetComponent<MeshRenderer>().enabled = false;
                ExitLeft_PoleController.endPoint.GetComponent<MeshRenderer>().enabled = false;

                ExitRight_PoleController.CreateMeshAndRope(MeshType.WALL, ExitRight_PoleController.endPoint.position.x - ExitRight_PoleController.startPoint.position.x);
                ExitLeft_PoleController.CreateMeshAndRope(MeshType.WALL, ExitLeft_PoleController.endPoint.position.x - ExitLeft_PoleController.startPoint.position.x);
            }
            else
            {
                PoleController Exit_PoleController = Instantiate(poleController, transform);

                Exit_PoleController.name = "Exit_PoleController";
                Exit_PoleController.transform.localPosition = Vector3.zero;

                Exit_PoleController.startPoint.localPosition = new Vector3(arenaTransform.localScale.x / 2, 1, arenaTransform.localScale.z / 2);
                Exit_PoleController.endPoint.localPosition = new Vector3(-arenaTransform.localScale.x / 2, 1, arenaTransform.localScale.z / 2);

                Exit_PoleController.startPoint.GetComponent<MeshRenderer>().enabled = false;
                Exit_PoleController.endPoint.GetComponent<MeshRenderer>().enabled = false;

                Exit_PoleController.CreateMeshAndRope(MeshType.WALL, Exit_PoleController.endPoint.position.x - Exit_PoleController.startPoint.position.x);
            }
        }

        private void CreateLeftRightWalls()
        {
            PoleController Right_PoleController = Instantiate(poleController, transform);
            PoleController Left_PoleController = Instantiate(poleController, transform);

            Right_PoleController.name = "Right_PoleController";
            Left_PoleController.name = "Left_PoleController";

            Right_PoleController.transform.localPosition = Vector3.zero;
            Left_PoleController.transform.localPosition = Vector3.zero;

            Right_PoleController.startPoint.localPosition = new Vector3(arenaTransform.localScale.x / 2, 1, -arenaTransform.localScale.z / 2);
            Right_PoleController.endPoint.localPosition = new Vector3(arenaTransform.localScale.x / 2, 1, arenaTransform.localScale.z / 2);
            Left_PoleController.startPoint.localPosition = new Vector3(-arenaTransform.localScale.x / 2, 1, -arenaTransform.localScale.z / 2);
            Left_PoleController.endPoint.localPosition = new Vector3(-arenaTransform.localScale.x / 2, 1, arenaTransform.localScale.z / 2);
            

            Right_PoleController.CreateMeshAndRope(MeshType.BRIDGE, Right_PoleController.endPoint.position.z - Right_PoleController.startPoint.position.z);
            Left_PoleController.CreateMeshAndRope(MeshType.BRIDGE, Left_PoleController.endPoint.position.z - Left_PoleController.startPoint.position.z);
        }
    }
}