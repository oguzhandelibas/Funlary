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
        [SerializeField] private AnimationController animationController;
        [SerializeField] private Rigidbody rb;
        public float moveSpeed = 2.0f;

        [HideInInspector] public IControl _IControl;


        private bool fall = false;
        private void Start()
        {
            animationController.PlayAnim(AnimTypes.IDLE);
        }

        private void FixedUpdate()
        {
            Vector3 direction = _IControl.MoveDirection();
            Vector3 move = new Vector3(
                direction.x * moveSpeed * 250 * Time.deltaTime,
                0,
                direction.z * moveSpeed * 250  * Time.deltaTime);
            
            if (direction.magnitude > 0)
            {
                rb.velocity = move;
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
                rb.velocity = _IControl.Stop();
            }
        }
    }
}
