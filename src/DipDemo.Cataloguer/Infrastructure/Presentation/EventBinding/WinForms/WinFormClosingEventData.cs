using System.Windows.Forms;

namespace DipDemo.Cataloguer.Infrastructure.Presentation.EventBinding.WinForms
{
    public class WinFormClosingEventData : IViewClosingEventData
    {
        private FormClosingEventArgs ea;

        public WinFormClosingEventData(FormClosingEventArgs e)
        {
            ea = e;
        }

        public bool Cancel
        {
            get { return ea.Cancel; }
            set { ea.Cancel = value; }
        }
    }
}