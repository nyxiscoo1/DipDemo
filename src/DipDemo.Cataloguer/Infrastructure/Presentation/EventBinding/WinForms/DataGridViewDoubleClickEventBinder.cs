using System;
using System.Windows.Forms;

namespace DipDemo.Cataloguer.Infrastructure.Presentation.EventBinding.WinForms
{
    public class DataGridViewDoubleClickEventBinder : IEventBinder
    {
        public void Bind(IPresenter presenter)
        {
            BindingHelper.FindAndConnectMethodsWithOneParameterFor<DataGridView>(
                presenter, ConnectDgvDoubleClick, Convensions.ListBoxDoubleClickSuffix, Convensions.DataGridViewPrefix);
        }

        private void ConnectDgvDoubleClick(DataGridView dgv, Delegate action)
        {
            dgv.CellMouseDoubleClick += (sender, args) =>
                                            {
                                                if (args.RowIndex == -1)
                                                    return;

                                                var item1 = ((BindingSource)((DataGridView)sender).DataSource).Current;
                                                if (item1 == null)
                                                    return;

                                                action.DynamicInvoke(item1);
                                            };
        }
    }
}