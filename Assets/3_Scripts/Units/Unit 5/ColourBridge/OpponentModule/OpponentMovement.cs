using System;
using System.Collections;
using UnityEngine;
using Funlary.Unit5.OpponentModule.Animation;
using Funlary.Unit5.OpponentModule.Controller;

namespace Funlary.Unit5.OpponentModule
{
    public class OpponentMovement : MonoBehaviour
    {
        [SerializeField] private Opponent opponent;
        [SerializeField] private OpponentMovementData movementData;
        public AnimationController animationController;
        public Rigidbody rb;
        [SerializeField] private Transform character;

        public float MovementSpeed
        {
            get => movementData.MovementSpeed;
        }
        public bool DoubleSpeed { get => movementData.DoubleSpeed; set => movementData.DoubleSpeed = value; }


        [HideInInspector] public IControl _IControl;
        private bool forceAdded = false;

        private bool fall = false;

        private void Start()
        {
            animationController.PlayAnim(AnimTypes.IDLE);
        }

        private void FixedUpdate()
        {
            if (!opponent.CanMove) return;

            RaycastHit hit;
            if (!Physics.Raycast(character.position + Vector3.up, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
            {
                animationController.PlayAnim(AnimTypes.FALLING);
                opponent.CanMove = false;
                opponent.DropAllStacks(false, true);
                return;
            }

            if(opponent.opponentType == Opponent.OpponentType.PLAYER) PlayerMovement();
            else AIMovement();
        }

        private void PlayerMovement()
        {
            float gravity = rb.velocity.y;

            Vector3 direction = _IControl.MoveDirection();
            Vector3 movement = new Vector3(direction.x, gravity, direction.z);
            movement = movement.normalized * MovementSpeed * Time.deltaTime;
            movement = movementData.DoubleSpeed ? movement *= 2 : movement;
            
            if (direction.magnitude > 0)
            {
                
                rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
                float blend = opponent.HasStack ? 1 : 0;
                animationController.PlayAnim(AnimTypes.RUN, blend);
            }
            else
            {
                AnimTypes animTypes;
                if (opponent.HasStack)
                    animTypes = AnimTypes.HOLDING_IDLE;
                else
                    animTypes = AnimTypes.IDLE;

                animationController.PlayAnim(animTypes);
                rb.velocity = new Vector3(0, gravity, 0);
            }
        }

        private void AIMovement()
        {
            _IControl.MoveDirection();
        }
    }
}
