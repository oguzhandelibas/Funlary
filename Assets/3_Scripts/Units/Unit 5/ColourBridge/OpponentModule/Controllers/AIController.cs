using System.Collections;
using System.Collections.Generic;
using Funlary.Unit5.OpponentModule.Animation;
using UnityEngine;
using UnityEngine.AI;

namespace Funlary.Unit5.OpponentModule.Controller
{
    public class AIController : IControl
    {
        private Opponent _opponent;
        private AnimationController _animationController;
        private NavMeshAgent _navMeshAgent;

        private Vector3 crawlSpace;
        private Vector3 destination;
        private bool hasDestination;

        public AIController(Opponent opponent, AnimationController animationController, NavMeshAgent navMeshAgent)
        {
            _opponent = opponent;
            _animationController = animationController;
            _navMeshAgent = navMeshAgent;
        }

        public Vector3 MoveDirection()
        {
            if (!hasDestination)
            {
                if (!_opponent.currentStackManager) return Vector3.zero;

                crawlSpace = _opponent.GetStackAreaSize;
                Vector3 destinationTemp = new Vector3(Random.Range(-crawlSpace.x / 2, crawlSpace.x / 2), 0,
                    Random.Range(-crawlSpace.z / 2, crawlSpace.z / 2));
                while (Vector3.Distance(destination,destinationTemp) < 1f)
                {
                    destinationTemp = new Vector3(Random.Range(-crawlSpace.x / 2, crawlSpace.x / 2), 0,
                        Random.Range(-crawlSpace.z / 2, crawlSpace.z / 2));
                }

                destination = destinationTemp;
                _navMeshAgent.SetDestination(destination);
                hasDestination = true;
            }
            else if(_navMeshAgent.remainingDistance < 0.1f)
            {
                hasDestination = false;
            }

            if (_opponent.HasStack)
                _animationController.PlayAnim(AnimTypes.RUN, 1);
            else
                _animationController.PlayAnim(AnimTypes.RUN, 0);

            return Vector3.back;
        }

        public Vector3 Stop()
        {
            return Vector3.zero;
        }
    }
}
