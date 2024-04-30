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
        
        public bool TimeIsActive
        {
            get => _timeIsActive;
        }

        public void ActivateTimer()
        {
            CurrentTime = PlayerPrefs.GetInt("LastTime");
            _timeIsActive = true;
        }
        public void DeactivateTimer() => _timeIsActive = false;

        private void Update()
        {
            if (_timeIsActive)
            {
                _currentTime += Time.deltaTime;
                timerText.text = _currentTime.ToString("F0");
            }
        }
        
        public void ResetTime()
        {
            _currentTime = 0;
            timerText.text = _currentTime.ToString("F0");
            _timeIsActive = true;
        }
    }
}
