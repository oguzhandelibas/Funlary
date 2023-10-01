using System.Collections;
using System.Collections.Generic;
using Funlary.UIModule.Core;
using Funlary.UIModule.Game;
using Funlary.Unit5.OpponentModule;
using Funlary.Unit5.OpponentModule.Animation;
using Funlary.Unit5.OpponentModule.Controller;
using UnityEngine;

namespace Funlary
{
    public class FinishController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out OpponentPhysicsController opponentPhysics))
            {
                Opponent opponent = opponentPhysics.opponent;
                Transform target = Camera.main.transform;
                Vector3 lookAtPosition = new Vector3(target.position.x, opponent.character.position.y, target.position.z);
                
                opponent.transform.position = new Vector3(transform.localPosition.x, opponent.transform.position.y, transform.localPosition.z);
                opponent.character.LookAt(lookAtPosition);
                opponent.DropAllStacks(false, false);
                opponent.animationController.PlayAnim(AnimTypes.DANCE);

                GameUIManager.Instance.Show<LevelCompletedUI>();
            }
        }
    }
}
