using UnityEngine.Events;
using Funlary.SoundModule.Enums;
using Funlary.Unit5.ColourBridge.BridgeModule;

namespace Funlary.SoundModule.Signals
{
    public class AudioSignals : AbstractSingleton<AudioSignals>
    {
        public UnityAction<ColorType> onPlaySound = delegate { };
    }
}
