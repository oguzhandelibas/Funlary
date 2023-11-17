using System.Collections;
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
        // TO DO: KARAKTER FINISH NOKTASINA KO�ACAK, D�N�P DANS EDECEK
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out OpponentPhysicsController opponentPhysics))
            {
                Opponent opponent = opponentPhysics.opponent;
                opponent.CanMove = false;
                StartCoroutine(RunFinishPoint(opponent));
            }
        }

        private IEnumerator RunFinishPoint(Opponent opponent)
        {
            Transform opponentTransform = opponent.opponentPhysicsController.transform;
            Vector3 targetPosition = new Vector3(transform.position.x, opponentTransform.position.y, transform.position.z);
            while (Vector3.Distance(opponentTransform.position, targetPosition) > 0.25)
            {
                opponentTransform.position =
                    Vector3.MoveTowards(opponentTransform.transform.position, targetPosition, 0.10f);
                yield return null;
            }
            TurnToCamera(opponent);
        }
        private void TurnToCamera(Opponent opponent)
        {
            Transform target = Camera.main.transform;
            Vector3 lookAtPosition = new Vector3(target.position.x, opponent.character.position.y, target.position.z);
            opponent.character.LookAt(lookAtPosition);
            LetsDance(opponent);
        }
        private void LetsDance(Opponent opponent)
        {
            opponent.DropAllStacks(false, true);
            opponent.animationController.PlayAnim(AnimTypes.DANCE);
            GameUIManager.Instance.Show<LevelCompletedUI>();
        }
    }
}
