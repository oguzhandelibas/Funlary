using System;
using UnityEngine;

namespace Funlary.Unit5.OpponentModule.Animation
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        AnimTypes animNowSelect = AnimTypes.IDLE;

        public void PlayAnim(AnimTypes animName, float blend = 0)
        {
            animator.SetFloat("Blend", blend);
            if (animNowSelect == animName)
                return;

            if (animName == AnimTypes.FALLING || animName == AnimTypes.DANCE)
            {
                animator.SetTrigger(animName.ToString());
                animNowSelect = animName;
                return;
            }

            foreach (AnimTypes item in (AnimTypes[])Enum.GetValues(typeof(AnimTypes)))
                animator.SetBool(item.ToString(), item == animName);

            animNowSelect = animName;
        }
    }
}
