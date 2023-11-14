using Funlary.Unit5.OpponentModule.Animation;
using UnityEngine;

namespace Funlary.Unit5.OpponentModule.Controller
{
    public class AIController : IControl
    {
        #region FIELDS

        private Opponent _opponent;
        private AnimationController _animationController;

        #endregion

        #region VARIABLES

        private bool _firstBridgeMovement;

        private Vector3 _crawlSpace;
        private Vector3 _destination;

        private bool _hasDestination;
        private bool _bridgeTime;
        private bool _turnBackArena;
        private int _targetStackCount;
        private bool _targetStackCountCalculated;
        private bool _bridgeConstructionDone;

        #endregion

        #region AI CONTROL

        public void SetTargetBridge()
        {

        }

        public AIController(Opponent opponent, AnimationController animationController)
        {
            _opponent = opponent;
            _animationController = animationController;
            _firstBridgeMovement = true;
        }

        public Vector3 MoveDirection()
        {
            if (!_hasDestination) _destination = CheckDestinition();

            if (_bridgeTime || Vector3.Distance(_opponent.character.parent.position, _destination) < 0.75f)
            {
                _destination = CheckDestinition();
            }

            if (_opponent.HasStack)
                _animationController.PlayAnim(AnimTypes.RUN, 1);
            else
                _animationController.PlayAnim(AnimTypes.RUN, 0);

            return _destination;
        }

        public Vector3 Stop()
        {
            return Vector3.zero;
        }


        private Vector3 CheckDestinition()
        {
            _hasDestination = true;
            if (!_bridgeTime)
            {
                if (!_opponent.currentStackManager) return Vector3.zero;

                _crawlSpace = _opponent.GetStackAreaSize;
                Vector3 destinationTemp = new Vector3(Random.Range(-_crawlSpace.x / 2, _crawlSpace.x / 2), 0,
                    Random.Range(-_crawlSpace.z / 2, _crawlSpace.z / 2));

                while (Vector3.Distance(_destination, destinationTemp) < 3f)
                {
                    destinationTemp = new Vector3(Random.Range(-_crawlSpace.x / 2, _crawlSpace.x / 2), 0,
                        Random.Range(-_crawlSpace.z / 2, _crawlSpace.z / 2));
                }
                return destinationTemp;
            }
            else
            {
                Debug.Log("1");
                Vector3 targetBridgePosition = Vector3.zero;
                if (_turnBackArena)
                {
                    Debug.Log("-1");
                    targetBridgePosition = _opponent.targetBridge.startPoint.position;
                    if (Vector3.Distance(_opponent.character.parent.position,
                            _opponent.targetBridge.startPoint.position) < 1.0f)
                    {
                        Debug.Log("-2");

                        _bridgeConstructionDone = false;
                        _bridgeTime = false;
                        _turnBackArena = false;
                        _hasDestination = false;
                        _firstBridgeMovement = false;
                    }
                }
                else
                {
                    if (_firstBridgeMovement)
                    {
                        Debug.Log("2");
                        targetBridgePosition = _opponent.targetBridge.startPoint.position;
                        if (Vector3.Distance(_opponent.character.parent.position,
                                _opponent.targetBridge.startPoint.position) < 0.5f)
                        {
                            Debug.Log("3");
                            _firstBridgeMovement = false;
                        }
                    }
                    else
                    {
                        Debug.Log("4");
                        _firstBridgeMovement = false;
                        targetBridgePosition = _opponent.targetBridge.endPoint.position;
                    }
                }
                

                return targetBridgePosition;
            }

        }

        // From OpponentStackController
        public void StackAdded()
        {
            if (_bridgeConstructionDone) _bridgeConstructionDone = false;

                // YETERL� SAYIDA STACK'E SAH�P M�?
            if (!_targetStackCountCalculated) CalculateTargetStackCount();
            Debug.Log(_targetStackCount);
            if (_opponent.StackCount >= _targetStackCount)
            {
                // K�PR� VAKT�
                _bridgeTime = true;
                Debug.Log("Bridge Time");
            }
            else
            {
                // TOPLAMAYA DEVAM
                _bridgeTime = false;
            }

        }

        public void GoNextArena()
        {
            _hasDestination = false;
            _bridgeTime = false;
        }

        public void StackRemoved()
        {
            if (_opponent.StackCount <= 0)
            {
                if (_bridgeConstructionDone)
                {
                    GoNextArena();
                }
                else
                {
                    Debug.Log("Turn Time!");
                    // TOPLAMAYA D�N��
                    _turnBackArena = true;
                    _targetStackCountCalculated = false;
                    _firstBridgeMovement = true;
                }
                
            }
        }

        private void CalculateTargetStackCount()
        {
            _targetStackCount = Random.Range(4, 8);
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
