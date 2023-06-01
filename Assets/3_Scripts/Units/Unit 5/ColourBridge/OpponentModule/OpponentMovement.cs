using System;
using System.Collections;
using UnityEngine;
using Funlary.Unit5.OpponentModule.Animation;
using Funlary.Unit5.OpponentModule.Controller;

namespace Funlary.Unit5.OpponentModule
{
    public class OpponentMovement : MonoBehaviour
    {
        [SerializeField] private AnimationController animationController;
        [SerializeField] private Rigidbody rb;
        public float moveSpeed = 2.0f;

        [HideInInspector] public IControl _IControl;

        private void Start()
        {
            animationController.PlayAnim(AnimTypes.IDLE);
        }

        private void Update()
        {
            Vector3 direction = _IControl.MoveDirection();
            Vector3 move = new Vector3(
                direction.x * moveSpeed * 1000 * Time.deltaTime,
                0,
                direction.z * moveSpeed * 1000  * Time.deltaTime);
            
            if (direction.magnitude > 0)
            {
                rb.velocity = move;
                animationController.PlayAnim(AnimTypes.RUN);
            }
            else
            {
                animationController.PlayAnim(AnimTypes.IDLE);
                rb.velocity = _IControl.Stop();
            }
        }
    }
}
