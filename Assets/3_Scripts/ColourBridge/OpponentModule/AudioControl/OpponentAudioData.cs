using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Funlary.Unit5.OpponentModule
{
    [CreateAssetMenu(fileName = "OpponentAudioData", menuName = "OpponentModule/OpponentAudioData", order = 1)]
    public class OpponentAudioData : ScriptableObject
    {
        [SerializedDictionary("Audio Type", "Sound")]
        public SerializedDictionary<OpponentAudioType, AudioClip> AudioClips;
    }
}
