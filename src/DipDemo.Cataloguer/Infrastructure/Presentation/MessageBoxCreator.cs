using System.Windows.Forms;

namespace DipDemo.Cataloguer.Infrastructure.Presentation
{
    public class MessageBoxCreator : IMessageBoxCreator
    {
        public static string AskUserDefaultCaption = "Внимание";
        public static string ShowInfoDefaultCaprion = "Информация";
        public static string ShowErrorDefaultCaption = "Ошибка";
        public static string AskUserWithCancellationDefaultCaption = "Внимание";

        #region IMessageBoxCreator Members

        public bool AskUser(string message)
        {
            return AskUser(message, AskUserDefaultCaption);
        }

        public bool AskUser(string message, string caption)
        {
            return MessageBox.Show(
                       message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public void ShowInfo(string message)
        {
            ShowInfo(message, ShowInfoDefaultCaprion);
        }

        public void ShowInfo(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowError(string message)
        {
            ShowError(message, ShowErrorDefaultCaption);
        }

        public void ShowError(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool? AskUserWithCancellation(string message)
        {
            return AskUserWithCancellation(message, AskUserWithCancellationDefaultCaption);
        }

        public bool? AskUserWithCancellation(string message, string caption)
        {
            bool? result = null;

            var dialogResult =
                MessageBox.Show(message, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
                result = true;
            else if (dialogResult == DialogResult.No)
                result = false;

            return result;
        }

        #endregion
    }
}