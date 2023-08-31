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
        public AnimationController animationController;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform character;
        public float moveSpeed = 2.0f;

        [HideInInspector] public IControl _IControl;
        private bool forceAdded = false;

        private bool fall = false;

        private void Start()
        {
            animationController.PlayAnim(AnimTypes.IDLE);
        }

        private void FixedUpdate()
        {
            float gravity = rb.velocity.y;
            Vector3 direction = _IControl.MoveDirection();
            Vector3 movement = new Vector3(direction.x, gravity, direction.z);
            movement = movement.normalized * moveSpeed * Time.deltaTime;
            
            if (direction.magnitude > 0)
            {
                rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
                animationController.PlayAnim(AnimTypes.RUN);
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
    }
}
