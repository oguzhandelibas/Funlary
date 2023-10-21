using UnityEngine;
using System.Collections.Generic;
using Funlary.SoundModule.Data.ScriptableObjects;
using Funlary.SoundModule.Signals;
using Funlary.SoundModule.Enums;

namespace Funlary.SoundModule
{
    public class AudioManager : MonoBehaviour
    {
        #region Self Variables

        #region Serializable Variables

        [SerializeField] private AudioSource Source;
        #endregion

        #region Private Variables
        private CD_Sound _cdSound;
        #endregion

        #endregion

        private CD_Sound GetSoundData(SoundType soundType)
        {
            return Resources.Load<CD_Sound>("Sound/CD_Sound_" + soundType);
        }

        #region Events Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            AudioSignals.Instance.onPlaySound += OnPlaySound;
        }

        private void UnsubscribeEvents()
        {
            AudioSignals.Instance.onPlaySound -= OnPlaySound;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void OnPlaySound(SoundType arg0)
        {
            Source.clip = GetSoundData(arg0).SoundData;
            Source.Play();
        }
    }
}
