using System.Collections.Generic;
using UnityEngine;

namespace Funlary.SoundModule.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CD_Sound", menuName = "ScriptableObjects/SoundModule/Data/CD_Sound", order = 0)]
    public class CD_Sound : ScriptableObject
    {
        public AudioClip[] SoundDatas;
    }
}
