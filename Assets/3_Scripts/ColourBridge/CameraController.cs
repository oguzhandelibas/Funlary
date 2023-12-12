using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace Funlary
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
        private CinemachineTransposer _cinemachineTransposer;
        private void Start()
        {
            _cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        }

        public void SetFollowOffset(Vector3 followOffset)
        {
            DOTween.To(() => _cinemachineTransposer.m_FollowOffset,
                    x => _cinemachineTransposer.m_FollowOffset = x,
                    followOffset, 2)
                .SetEase(Ease.Linear);
        }
    }
}
