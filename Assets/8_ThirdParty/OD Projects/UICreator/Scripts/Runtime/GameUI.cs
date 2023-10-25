using DG.Tweening;
using Funlary.SoundModule.Signals;
using Funlary.Unit5.ColourBridge.BridgeModule;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Funlary.UIModule.Core
{
    public class GameUI : View
    {

        [SerializeField] private TextMeshProUGUI levelCountText;
        [SerializeField] private TextMeshProUGUI levelColorText;
        [SerializeField] private Image playLevelColorSoundImage;
        [SerializeField] private TextMeshProUGUI coinCountText;
        [SerializeField] private RectTransform settingsHolder;

        private bool settingsToggle;
        private Vector2 settingsSizeDelta;
        private ColorType currentColorType;

        private void Start()
        {
            settingsSizeDelta = settingsHolder.sizeDelta;
        }

        public void ToggleSettingsButtonClick()
        {
            var sizeDelta = settingsSizeDelta;

            if (settingsToggle)
                sizeDelta = new Vector2(sizeDelta.x, sizeDelta.y);
            else
                sizeDelta = new Vector2(sizeDelta.x, sizeDelta.y / 3f);

            settingsToggle = !settingsToggle;
            settingsHolder.DOSizeDelta(sizeDelta, .25f);
        }

        public void SetLevelCountText(string levelText)
        {
            levelCountText.text = levelText;
        }

        public void SetLevelColorText(ColorType colorType, Color color)
        {
            color = new Color(color.r+0.2f, color.g + 0.2f, color.b + 0.2f, color.a);
            levelColorText.text = colorType.ToString();
            levelColorText.color = color;
            playLevelColorSoundImage.color = color;
            SetAndPlayColorSound(colorType);
        }

        public void SetCoinText(string coinText)
        {
            coinCountText.text = coinText;
        }

        private void SetAndPlayColorSound(ColorType colorType)
        {
            currentColorType = colorType;
            _PlayColorSound();
        }

        #region BUTTONS

        public void _PlayColorSound()
        {
            AudioSignals.Instance.onPlaySound(currentColorType);
        }

        #endregion
    }
}