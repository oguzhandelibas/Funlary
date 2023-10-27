using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Funlary.Unit5.OpponentModule.Controller
{
    public class AIController : IControl
    {
        private Opponent _opponent;
        private NavMeshAgent _navMeshAgent;

        private Vector3 crawlSpace;
        private Vector3 destination;
        private bool hasDestination;

        public AIController(Opponent opponent, NavMeshAgent navMeshAgent)
        {
            _opponent = opponent;
            _navMeshAgent = navMeshAgent;
        }

        public Vector3 MoveDirection()
        {
            if (!hasDestination)
            {
                crawlSpace = _opponent.currentStackManager.stackAreaSize;
                destination = new Vector3(Random.Range(-crawlSpace.x / 2, crawlSpace.x / 2), 0,
                    Random.Range(-crawlSpace.z / 2, crawlSpace.z / 2));
                _navMeshAgent.SetDestination(destination);
                hasDestination = true;
            }
            else if(_navMeshAgent.remainingDistance < 0.1f)
            {
                hasDestination = false;
            }

            return Vector3.back;
        }

        public Vector3 Stop()
        {
            return Vector3.zero;
        }
    }
}
