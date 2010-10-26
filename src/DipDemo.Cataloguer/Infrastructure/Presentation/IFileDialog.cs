namespace DipDemo.Cataloguer.Infrastructure.Presentation
{
    public interface IFileDialog
    {
        bool ShowDialog();
        string Title { get; set; }
        string Filter { get; set; }
        string FileName { get; }
    }

    public interface ISaveFileDialog : IFileDialog
    {
    }

    public interface IOpenFileDialog : IFileDialog
    {
    }

    public abstract class AbstractFileDialog<T> : IFileDialog
        where T : System.Windows.Forms.FileDialog, new()
    {
        private T dialog = new T();

        #region IFileDialog Members

        public bool ShowDialog()
        {
            return dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        public string Title
        {
            get { return dialog.Title; }
            set { dialog.Title = value; }
        }

        public string Filter
        {
            get { return dialog.Filter; }
            set { dialog.Filter = value; }
        }

        public string FileName
        {
            get { return dialog.FileName; }
        }

        #endregion
    }

    public class SaveFileDialog : AbstractFileDialog<System.Windows.Forms.SaveFileDialog>, ISaveFileDialog
    {
    }

    public class OpenFileDialog : AbstractFileDialog<System.Windows.Forms.OpenFileDialog>, IOpenFileDialog
    {
    }
}