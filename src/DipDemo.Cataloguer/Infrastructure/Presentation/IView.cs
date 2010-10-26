using System;

namespace DipDemo.Cataloguer.Infrastructure.Presentation
{
    public interface IView
    {
        void ShowView();
        void ShowModalView();
        void CloseView();
        event EventHandler Closed;
    }
}