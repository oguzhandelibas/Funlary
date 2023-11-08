using Funlary.Unit5.OpponentModule.Animation;
using UnityEngine;
using UnityEngine.AI;

namespace Funlary.Unit5.OpponentModule.Controller
{
    public class AIController : IControl
    {
        #region FIELDS

        private Opponent _opponent;
        private AnimationController _animationController;
        private NavMeshAgent _navMeshAgent;

        #endregion

        #region VARIABLES

        private Vector3 _crawlSpace;
        private Vector3 _destination;
        private bool _hasDestination;
        private bool _bridgeTime;
        private int _targetStackCount;
        private bool _targetStackCountCalculated;

        #endregion

        #region AI CONTROL

        public AIController(Opponent opponent, AnimationController animationController, NavMeshAgent navMeshAgent)
        {
            _opponent = opponent;
            _animationController = animationController;
            _navMeshAgent = navMeshAgent;
        }

        public Vector3 MoveDirection()
        {
            if (!_hasDestination)
            {
                _destination = CheckDestinition();

                _navMeshAgent.SetDestination(_destination);
                _hasDestination = true;
            }
            else if (_navMeshAgent.remainingDistance < 0.1f)
            {
                _hasDestination = false;
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



        private Vector3 CheckDestinition()
        {
            if (!_bridgeTime)
            {
                if (!_opponent.currentStackManager) return Vector3.zero;

                _crawlSpace = _opponent.GetStackAreaSize;
                Vector3 destinationTemp = new Vector3(Random.Range(-_crawlSpace.x / 2, _crawlSpace.x / 2), 0,
                    Random.Range(-_crawlSpace.z / 2, _crawlSpace.z / 2));
                while (Vector3.Distance(_destination, destinationTemp) < 1f)
                {
                    destinationTemp = new Vector3(Random.Range(-_crawlSpace.x / 2, _crawlSpace.x / 2), 0,
                        Random.Range(-_crawlSpace.z / 2, _crawlSpace.z / 2));
                }

                return destinationTemp;
            }
            else
            {
                return _opponent.targetBridge.endPoint.position;
            }

        }

        // From OpponentStackController
        public void StackAdded()
        {
            // YETERL� SAYIDA STACK'E SAH�P M�?
            if (!_targetStackCountCalculated) CalculateTargetStackCount();
            Debug.Log(_targetStackCount);
            if (_opponent.StackCount >= _targetStackCount)
            {
                // K�PR� VAKT�
                _bridgeTime = true;
            }
            else
            {
                // TOPLAMAYA DEVAM
                _bridgeTime = false;
            }

        }

        public void StackRemoved()
        {
            if (_opponent.StackCount <= 0)
            {
                // TOPLAMAYA D�N��
                _bridgeTime = false;
                _targetStackCountCalculated = false;
            }
        }

        private void CalculateTargetStackCount()
        {
            _targetStackCount = Random.Range(5, 15);
            _targetStackCountCalculated = true;
        }
        private void SetNewBridge()
        {

        }
        // SE��L� K�RP�YE SAH�P M�?    // SE��L� K�PR� S�LME    // K�PR� SE��M�. 

        // K�PR�YE Y�NEL,   // K�PR�YE STEP EKLE  // K�PR� TAMAMLANDI MI   // TAMAMLANDIYSA SIRADAK� ALANA GE�   // TAMAMLANMADIYSA GER� D�N VE TOPLAMAYA DEVAM ET

        #endregion
    }
}
