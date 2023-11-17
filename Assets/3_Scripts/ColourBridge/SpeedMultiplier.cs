using System.Collections;
using System.Collections.Generic;
using Funlary.Unit5.OpponentModule;
using Funlary.Unit5.OpponentModule.Controller;
using UnityEngine;

namespace Funlary
{
    public class SpeedMultiplier : MonoBehaviour
    {
        [SerializeField] private Collider frontCollider;
        [SerializeField] private Collider backCollider;
        public bool frontActive = true;

        private void Start()
        {
            frontCollider.enabled = true;
            backCollider.enabled = false;
        }

        private void SetColliderActiveness(OpponentMovement opponentMovement)
        {
            if (frontActive)
            {
                backCollider.enabled = true;
                frontCollider.enabled = false;
                opponentMovement.DoubleSpeed = true;
            }
            else
            {
                frontCollider.enabled = true;
                backCollider.enabled = false;
                opponentMovement.DoubleSpeed = false;
            }
            frontActive = !frontActive;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out OpponentPhysicsController physicsController))
            {
                SetColliderActiveness(physicsController.opponent.opponentMovement);
            }
        }
    }
}
