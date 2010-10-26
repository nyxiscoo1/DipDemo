using System;
using System.Windows.Forms;

namespace DipDemo.Cataloguer.Infrastructure.Presentation.EventBinding.WinForms
{
    public class ListBoxDoubleClickEventBinder : IEventBinder
    {
        public void Bind(IPresenter presenter)
        {
            BindingHelper.FindAndConnectMethodsWithOneParameterFor<ListBox>(
                presenter, ConnectListBoxDoubleClick, Convensions.ListBoxDoubleClickSuffix, Convensions.ListBoxPrefix);
        }

        private void ConnectListBoxDoubleClick(ListBox lb, Delegate action)
        {
            lb.MouseDoubleClick += (sender, args) =>
                                       {
                                           var item1 = ((ListBox)sender).SelectedItem;
                                           if (item1 == null)
                                               return;
                                           action.DynamicInvoke(item1);
                                       };
        }
    }
}