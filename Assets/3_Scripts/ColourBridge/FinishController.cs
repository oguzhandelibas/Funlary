using System;
using System.Collections;
using System.Threading.Tasks;
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
        [SerializeField] private GameObject[] inLineconfettiPrefabs;
        [SerializeField] private GameObject[] lastConfettiPrefabs;
        private void Start()
        {
            ConfettiActiveness(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out OpponentPhysicsController opponentPhysics))
            {
                Opponent opponent = opponentPhysics.opponent;
                opponent.CanMove = false;
                StartCoroutine(RunFinishPoint(opponent));
                ConfettiActiveness(true);
            }
        }

        private IEnumerator RunFinishPoint(Opponent opponent)
        {
            opponent.SetUIPanelActiveness(false);
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

        private async Task ConfettiActiveness(bool value) {
            for (int i = 0; i < inLineconfettiPrefabs.Length; i+=2)
            {
                
                inLineconfettiPrefabs[i].SetActive(value);
                if(i!=inLineconfettiPrefabs.Length-1) inLineconfettiPrefabs[i+1].SetActive(value);
                await Task.Delay(100);
            }

            foreach (GameObject item in lastConfettiPrefabs) item.SetActive(value);
        }
        
        private void LetsDance(Opponent opponent)
        {
            opponent.DropAllStacks(false, true);
            opponent.animationController.PlayAnim(AnimTypes.DANCE);
            GameUIManager.Instance.Show<LevelCompletedUI>();
        }
    }
}
