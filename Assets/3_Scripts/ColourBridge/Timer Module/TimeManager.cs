using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Funlary
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private float _currentTime;
        private bool _timeIsActive;

        public float CurrentTime
        {
            get => _currentTime;
            set => _currentTime = value;
        }
        public void ActivateTimer()
        {
            _timeIsActive = true;
        }

        private void Update()
        {
            if (_timeIsActive)
            {
                _currentTime += Time.deltaTime;
                timerText.text = _currentTime.ToString("000");
            }
        }


        public void ResetTime()
        {
            _timeIsActive = false;
            _currentTime = 0;
        }
    }
}
