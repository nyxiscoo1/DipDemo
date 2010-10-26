using System;
using System.Windows.Forms;

namespace DipDemo.Cataloguer.Infrastructure.Presentation.EventBinding.WinForms
{
    public class DataGridViewSelectionChangedEventBinder : IEventBinder
    {
        public void Bind(IPresenter presenter)
        {
            BindingHelper.FindAndConnectMethodsWithOneParameterFor<DataGridView>(
                presenter, ConnectDgvSelectionChanged, Convensions.ListBoxItemSelectedSuffix, Convensions.DataGridViewPrefix);
        }

        private void ConnectDgvSelectionChanged(DataGridView dgv, Delegate action)
        {
            dgv.SelectionChanged += (sender, args) =>
            {
                var item1 = ((BindingSource)((DataGridView)sender).DataSource).Current;

                action.DynamicInvoke(item1);
            };
        }
    }
}
