using System;
using System.Linq;
using System.Windows.Forms;

namespace DipDemo.Cataloguer.Infrastructure.Presentation.EventBinding.WinForms
{
    public class ViewClosingEventBinder : IEventBinder
    {
        public void Bind(IPresenter presenter)
        {
            if(!(presenter.View is Form))
                return;
            
            Type presenterType = presenter.GetType();
            var method = BindingHelper.GetActionMethods(presenterType)
                .SingleOrDefault(x => x.GetParameters().Length == 1
                                      && x.GetParameters()[0].ParameterType == typeof (IViewClosingEventData));

            if(method == null)
                return;

            var view = presenter.View as Form;
            var action = Delegate.CreateDelegate(typeof(Action<>).MakeGenericType(typeof (IViewClosingEventData)),
                                                     presenter, method);

            view.FormClosing += (o, e) => action.DynamicInvoke(new WinFormClosingEventData(e));
        }
    }
}
