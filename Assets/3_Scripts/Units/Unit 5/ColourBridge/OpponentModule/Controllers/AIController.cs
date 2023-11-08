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
            // YETERLÝ SAYIDA STACK'E SAHÝP MÝ?
            if (!_targetStackCountCalculated) CalculateTargetStackCount();
            Debug.Log(_targetStackCount);
            if (_opponent.StackCount >= _targetStackCount)
            {
                // KÖPRÜ VAKTÝ
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
                // TOPLAMAYA DÖNÜÞ
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
        // SEÇÝLÝ KÖRPÜYE SAHÝP MÝ?    // SEÇÝLÝ KÖPRÜ SÝLME    // KÖPRÜ SEÇÝMÝ. 

        // KÖPRÜYE YÖNEL,   // KÖPRÜYE STEP EKLE  // KÖPRÜ TAMAMLANDI MI   // TAMAMLANDIYSA SIRADAKÝ ALANA GEÇ   // TAMAMLANMADIYSA GERÝ DÖN VE TOPLAMAYA DEVAM ET

        #endregion
    }
}
