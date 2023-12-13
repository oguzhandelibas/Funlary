using Funlary.UIModule.Game;
using UnityEngine;
using Funlary.UIModule.Menu;
namespace Funlary.UIModule.Core
{
    public abstract class View : MonoBehaviour
    {
        /// <summary>
        /// Initializes the View
        /// </summary>
        public virtual void Initialize()
        {
        }

        #region UI BUTTON

        public void _ClosePanel()
        {
            GameUIManager.Instance.GoBack();
        }

        #endregion
        
        
        /// <summary>
        /// Makes the View visible
        /// </summary>
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the view
        /// </summary>
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
