using DG.Tweening;
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

        public void SetLevelColorText(string colorText, Color color)
        {
            levelColorText.text = colorText;
            levelColorText.color = color;
            playLevelColorSoundImage.color = color;
        }

        public void SetCoinText(string coinText)
        {
            coinCountText.text = coinText;
        }
    }
}