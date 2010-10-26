namespace DipDemo.Cataloguer.Presentation.EditBook
{
    public partial class EditBookView : ViewForm, IEditBookView
    {
        public EditBookView()
        {
            InitializeComponent();
        }

        string IEditBookView.Name
        {
            get { return tbName.Text; }
            set { tbName.Text = value; }
        }

        string IEditBookView.Author
        {
            get { return tbAuthor.Text; }
            set { tbAuthor.Text = value; }
        }

        string IEditBookView.Description
        {
            get { return tbDescription.Text; }
            set { tbDescription.Text = value; }
        }
    }
}
