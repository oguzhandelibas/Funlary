using System.Collections;
using System.Collections.Generic;
using Funlary.Unit5.ColourBridge.BridgeModule;
using UnityEngine;

namespace Funlary.Unit5.OpponentModule
{
    public class OpponentManager : MonoBehaviour
    {
        [SerializeField] private Opponent[] opponents;
        private List<ColorType> _colorTypes = new List<ColorType>();

        public void SetColorTypes(List<ColorType> colorTypes)
        {
            _colorTypes = colorTypes;
            InitializeOpponents();
        }

        private void InitializeOpponents()
        {
            int count = _colorTypes.Count;
            List<int> colorTypeIndex = new List<int>(count);
            for (var i = 0; i < count; i++) colorTypeIndex.Add(i);

            foreach (var item in opponents)
            {
                int j = Random.Range(0, colorTypeIndex.Count);
                item.SetColor(_colorTypes[colorTypeIndex[j]]);
                colorTypeIndex.RemoveAt(j);
            }
        }
    }
}
