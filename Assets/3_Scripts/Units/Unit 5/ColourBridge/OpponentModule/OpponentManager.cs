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
        [SerializeField] private ColorData colorData;
        private List<ColorType> _colorTypes = new List<ColorType>();

        public void SetColorTypes(List<ColorType> colorTypes)
        {
            _colorTypes = colorTypes;
            InitializeOpponents();
        }

        public ColorType GetRandomColorType(ColorType currentColorType)
        {
            List<ColorType> colorTypes = BridgeManager.Instance.GetColorTypes();
            ColorType colorType = GetRandomColorType();

            while (currentColorType == colorType)
            {
                colorType = GetRandomColorType();
            }

            return colorType;
        }

        private ColorType GetRandomColorType()
        {
            return colorData.GetRandomColorType(_colorTypes);
        }

        private void InitializeOpponents()
        {
            foreach (var item in opponents)
            {
                item.SetColor(GetRandomColorType());
            }
        }
    }
}
