using System;
using System.Windows.Forms;

namespace DipDemo.Cataloguer.Infrastructure.Presentation.EventBinding.WinForms
{
    public class ListBoxItemSelectEventBinder : IEventBinder
    {
        public void Bind(IPresenter presenter)
        {
            BindingHelper.FindAndConnectMethodsWithOneParameterFor<ListBox>(
                presenter, ConnctListBoxItemSelect, Convensions.ListBoxItemSelectedSuffix, Convensions.ListBoxPrefix);
        }

        private void ConnctListBoxItemSelect(ListBox lb, Delegate action)
        {
            lb.SelectedIndexChanged += (sender, args) =>
                                           {
                                               var item1 = ((ListBox)sender).SelectedItem;
                                               if (item1 == null)
                                                   return;
                                               action.DynamicInvoke(item1);
                                           };
        }
    }
}