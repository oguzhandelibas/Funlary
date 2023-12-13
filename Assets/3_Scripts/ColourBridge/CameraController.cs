using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace Funlary
{
    public class CameraController : AbstractSingleton<CameraController>
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

        public void SetFollowObject(Transform followTransform)
        {
            VirtualCameraActiveness(true);
            cinemachineVirtualCamera.Follow = followTransform;
            cinemachineVirtualCamera.LookAt = followTransform;
        }

        public void VirtualCameraActiveness(bool value)
        {
            cinemachineVirtualCamera.enabled = value;
        }
    }
}
