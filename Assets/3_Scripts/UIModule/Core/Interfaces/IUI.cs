using Funlary.UIModule.Core.Enums;

namespace Funlary.UIModule.Core.Interfaces
{
    public interface IUI
    {
        //void SetPanelType(UIPanelType uıPanelType);
        T GetView<T>() where T : View;
        void Show<T>(bool keepInHistory = true) where T : View;
        void Show(View view, bool keepInHistory = true);
        void GoBack();
    }
}