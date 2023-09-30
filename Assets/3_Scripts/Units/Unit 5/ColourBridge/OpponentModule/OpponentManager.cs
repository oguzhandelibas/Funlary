using System.Collections;
using System.Collections.Generic;
using Funlary.UIModule.Core;
using Funlary.UIModule.Game;
using Funlary.Unit5.ColourBridge.BridgeModule;
using UnityEngine;

namespace Funlary.Unit5.OpponentModule
{
    public class OpponentManager : AbstractSingleton<OpponentManager>
    {
        [SerializeField] private Opponent[] opponents;
        private List<ColorType> _colorTypes = new List<ColorType>();

        public ColorType GetRandomColorType(List<int> colorTypeIndex = null)
        {
            if (colorTypeIndex == null)
            {
                int count = _colorTypes.Count;
                colorTypeIndex = new List<int>(count);
                for (var i = 0; i < count; i++) colorTypeIndex.Add(i);
            }

            int j = Random.Range(0, colorTypeIndex.Count);
            ColorType colorType = _colorTypes[colorTypeIndex[j]];
            colorTypeIndex.RemoveAt(j);
            return colorType;
        }
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
                item.SetColor(GetRandomColorType(colorTypeIndex));
            }
            ColorType colorType = opponents[0].ColorType;
            GameUIManager.Instance.gameUI.SetLevelColorText(colorType.ToString(), ColorManager.Instance.GetColor(colorType));
        }
    }
}
