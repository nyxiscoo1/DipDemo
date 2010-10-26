using System.Windows.Forms;
using DipDemo.Cataloguer.Infrastructure.Presentation;

namespace DipDemo.Cataloguer.Presentation
{
    public class ViewForm : Form, IView
    {
        #region Implementation of IView

        public virtual void ShowView()
        {
            Show();
        }

        public virtual void ShowModalView()
        {
            ShowDialog();
        }

        public virtual void CloseView()
        {
            Close();
        }

        #endregion
    }
}