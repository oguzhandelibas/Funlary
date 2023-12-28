using UnityEngine;

namespace Funlary.Unit5.OpponentModule
{
    public class OpponentAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource opponentAudioSource;
        [SerializeField] private OpponentAudioData opponentAudioData;

        public void PlaySound(OpponentAudioType opponentAudioType)
        {
            if(!GameManager.Instance.soundIsActive) return;
            opponentAudioSource.clip = opponentAudioData.AudioClips[opponentAudioType];
            opponentAudioSource.Play();
        }
    }
}
