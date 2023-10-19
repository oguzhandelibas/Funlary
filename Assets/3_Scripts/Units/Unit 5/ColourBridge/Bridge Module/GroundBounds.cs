using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

                Transform RightFirst = EnterRight_PoleController.transform.GetChild(0);
                Transform RightSecond = EnterRight_PoleController.transform.GetChild(1);

                Transform LeftFirst = EnterLeft_PoleController.transform.GetChild(0);
                Transform LeftSecond = EnterLeft_PoleController.transform.GetChild(1);

                EnterRight_PoleController.transform.GetChild(0).localPosition = new Vector3(arenaTransform.localScale.x / 2, 1, -arenaTransform.localScale.x / 2);
                EnterRight_PoleController.transform.GetChild(1).localPosition = new Vector3(1.5f, 1, -arenaTransform.localScale.x / 2);
                EnterLeft_PoleController.transform.GetChild(0).localPosition = new Vector3(-1.5f, 1, -arenaTransform.localScale.x / 2);
                EnterLeft_PoleController.transform.GetChild(1).localPosition = new Vector3(-arenaTransform.localScale.x / 2, 1,-arenaTransform.localScale.x / 2);

                EnterRight_PoleController.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                EnterRight_PoleController.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
                EnterLeft_PoleController.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                EnterLeft_PoleController.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;

                EnterRight_PoleController.CreateRope(RightSecond.position.x - RightFirst.position.x);
                EnterLeft_PoleController.CreateRope(LeftSecond.position.x - LeftFirst.position.x);
            }
            else
            {
                PoleController Enter_PoleController = Instantiate(poleController, transform);

                Enter_PoleController.name = "Enter_PoleController";

                Enter_PoleController.transform.localPosition = Vector3.zero;

                Transform First = Enter_PoleController.transform.GetChild(0);
                Transform Second = Enter_PoleController.transform.GetChild(1);

                Enter_PoleController.transform.GetChild(0).localPosition = new Vector3(arenaTransform.localScale.x / 2, 1, -arenaTransform.localScale.x / 2);
                Enter_PoleController.transform.GetChild(1).localPosition = new Vector3(-arenaTransform.localScale.x / 2, 1, -arenaTransform.localScale.x / 2);

                Enter_PoleController.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                Enter_PoleController.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;

                Enter_PoleController.CreateRope(Second.position.x - First.position.x);

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

                Transform RightFirst = ExitRight_PoleController.transform.GetChild(0);
                Transform RightSecond = ExitRight_PoleController.transform.GetChild(1);

                Transform LeftFirst = ExitLeft_PoleController.transform.GetChild(0);
                Transform LeftSecond = ExitLeft_PoleController.transform.GetChild(1);

                ExitRight_PoleController.transform.GetChild(0).localPosition = new Vector3(arenaTransform.localScale.x / 2, 1, arenaTransform.localScale.x / 2);
                ExitRight_PoleController.transform.GetChild(1).localPosition = new Vector3(1.5f, 1, arenaTransform.localScale.x / 2);
                ExitRight_PoleController.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                ExitRight_PoleController.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;

                ExitLeft_PoleController.transform.GetChild(0).localPosition = new Vector3(- 1.5f, 1, arenaTransform.localScale.x / 2);
                ExitLeft_PoleController.transform.GetChild(1).localPosition = new Vector3(-arenaTransform.localScale.x / 2, 1, arenaTransform.localScale.x / 2);
                ExitLeft_PoleController.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                ExitLeft_PoleController.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;

                ExitRight_PoleController.CreateRope(RightSecond.position.x - RightFirst.position.x);
                ExitLeft_PoleController.CreateRope(LeftSecond.position.x - LeftFirst.position.x);
            }
            else
            {
                PoleController Exit_PoleController = Instantiate(poleController, transform);

                Exit_PoleController.name = "Exit_PoleController";

                Exit_PoleController.transform.localPosition = Vector3.zero;

                Transform First = Exit_PoleController.transform.GetChild(0);
                Transform Second = Exit_PoleController.transform.GetChild(1);

                Exit_PoleController.transform.GetChild(0).localPosition = new Vector3(arenaTransform.localScale.x / 2, 1, arenaTransform.localScale.z / 2);
                Exit_PoleController.transform.GetChild(1).localPosition = new Vector3(-arenaTransform.localScale.x / 2, 1, arenaTransform.localScale.z / 2);

                Exit_PoleController.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                Exit_PoleController.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;

                Exit_PoleController.CreateRope(Second.position.x - First.position.x);
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

            Transform RightFirst = Right_PoleController.transform.GetChild(0);
            Transform RightSecond = Right_PoleController.transform.GetChild(1);

            Transform LeftFirst = Left_PoleController.transform.GetChild(0);
            Transform LeftSecond = Left_PoleController.transform.GetChild(1);

            Right_PoleController.transform.GetChild(0).localPosition = new Vector3(arenaTransform.localScale.x / 2, 1, -arenaTransform.localScale.z / 2);
            Right_PoleController.transform.GetChild(1).localPosition = new Vector3(arenaTransform.localScale.x / 2, 1, arenaTransform.localScale.z / 2);
            Left_PoleController.transform.GetChild(0).localPosition = new Vector3(-arenaTransform.localScale.x / 2, 1, -arenaTransform.localScale.z / 2);
            Left_PoleController.transform.GetChild(1).localPosition = new Vector3(-arenaTransform.localScale.x / 2, 1, arenaTransform.localScale.z / 2);
            

            Right_PoleController.CreateRope(RightSecond.position.z - RightFirst.position.z);
            Left_PoleController.CreateRope(LeftSecond.position.z - LeftFirst.position.z);
        }
    }
}
