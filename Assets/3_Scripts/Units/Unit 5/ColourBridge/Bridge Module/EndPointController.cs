using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Funlary.Unit5.OpponentModule;
using Funlary.Unit5.OpponentModule.Controller;
using UnityEngine;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    public class EndPointController : MonoBehaviour
    {
        [SerializeField] private BoxCollider firstWall, secondWall;
        [SerializeField] private BoxCollider triggerWall;
        private Opponent opponent;

        private void Start()
        {
            triggerWall.enabled = true;
            firstWall.enabled = false;
            secondWall.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.GetComponent<OpponentPhysicsController>())
            {
                firstWall.enabled = true;
                secondWall.enabled = false;
            }
        }

        private void OnTriggerStay(Collider other)
        {

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.GetComponent<OpponentPhysicsController>())
            {
                SetSecondWall();
            }
        }

        private async Task SetSecondWall()
        {
            await Task.Delay(250);
            secondWall.enabled = true;
        }
    }
}
