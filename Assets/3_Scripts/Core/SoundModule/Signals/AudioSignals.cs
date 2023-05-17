using UnityEngine.Events;
using Funlary.AudioModule.Enums;

namespace Funlary.AudioModule.Signals
{
    public class AudioSignals : AbstractSingleton<AudioSignals>
    {
        public UnityAction<SoundType, float> onPlaySound = delegate { };
    }
}
