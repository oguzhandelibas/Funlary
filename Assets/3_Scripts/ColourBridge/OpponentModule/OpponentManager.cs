using System.Collections.Generic;
using Funlary.UIModule.Core;
using Funlary.UIModule.Game;
using Funlary.Unit5.ColourBridge.BridgeModule;
using UnityEngine;

namespace Funlary.Unit5.OpponentModule
{
    public class OpponentManager : MonoBehaviour
    {
        [SerializeField] private BridgeManager bridgeManager;
        [SerializeField] private ArenaManager arenaManager;
        [SerializeField] private Opponent[] opponents;
        [SerializeField] private ColorData colorData;
        private List<ColorType> _colorTypes = new List<ColorType>();

        public ColorType GetRandomColorType(ColorType currentColorType)
        {
            List<ColorType> colorTypes = bridgeManager.GetColorTypes();
            ColorType colorType = GetRandomColorType();

            while (currentColorType == colorType)
            {
                colorType = GetRandomColorType();
            }

            return colorType;
        }

        public void SetColorTypes(List<ColorType> colorTypes)
        {
            _colorTypes = colorTypes;
            InitializeOpponents();
        }

        private ColorType GetRandomColorType() => colorData.GetRandomColorType(_colorTypes);
        

        private void InitializeOpponents()
        {
            for (int i = 0; i < opponents.Length; i++)
            {
                opponents[i].SetColor(_colorTypes[i]);
                opponents[i].InitializeOpponent(arenaManager.arenas[0].stackManager);
            }
        }
    }
}
