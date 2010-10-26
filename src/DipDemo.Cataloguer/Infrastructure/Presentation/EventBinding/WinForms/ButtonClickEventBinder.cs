using System;
using System.Linq;
using System.Windows.Forms;

namespace DipDemo.Cataloguer.Infrastructure.Presentation.EventBinding.WinForms
{
    public class ButtonClickEventBinder : IEventBinder
    {
        public void Bind(IPresenter presenter)
        {
            var presenterType = presenter.GetType();
            var controls = BindingHelper.GetViewControls(presenter.View).OfType<Button>();

            var presenterMethods = BindingHelper.GetParameterlessActionMethods(presenterType);

            foreach (var method in presenterMethods)
            {
                string methodName = method.Name;
                var btn = controls.FirstOrDefault(
                    x => x.Name == Convensions.ButtonPrefix + methodName.Substring(Convensions.EventHandlerPrefix.Length));

                if (btn == null)
                    continue;

                var action = (Action)Delegate.CreateDelegate(typeof(Action), presenter, method.Name);

                var handler = (EventHandler)((sender, args) => action());
                btn.Click += handler;
            }
        }
    }
}