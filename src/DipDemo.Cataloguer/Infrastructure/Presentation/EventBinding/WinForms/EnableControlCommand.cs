using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;

namespace DipDemo.Cataloguer.Infrastructure.Presentation.EventBinding.WinForms
{
    public class EnableControlCommand
    {
        private readonly Control control;
        private readonly Fact canExecute;
        private SynchronizationContext ctx;

        public EnableControlCommand(Control control, Fact canExecute)
        {
            this.control = control;
            this.canExecute = canExecute;
            ctx = AsyncOperationManager.SynchronizationContext;

            if (canExecute != null)
            {
                this.canExecute.PropertyChanged +=
                    (sender, args) =>
                    ctx.Post(x => control.Enabled = canExecute.Value, null);
            }
        }
    }
}