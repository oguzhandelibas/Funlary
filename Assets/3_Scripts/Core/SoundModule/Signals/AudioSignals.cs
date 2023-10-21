using UnityEngine.Events;
using Funlary.SoundModule.Enums;

namespace Funlary.SoundModule.Signals
{
    public class AudioSignals : AbstractSingleton<AudioSignals>
    {
        public UnityAction<SoundType> onPlaySound = delegate { };
    }
}
