using System;
using System.Windows.Forms;

namespace DipDemo.Cataloguer.Presentation.ViewCatalogue
{
    public partial class CatalogueView : UserControl, ICatalogueView
    {
        public CatalogueView()
        {
            InitializeComponent();
        }

        public void ShowView()
        {
            throw new NotImplementedException();
        }

        public void ShowModalView()
        {
            throw new NotImplementedException();
        }

        public void CloseView()
        {
            Closed(this, EventArgs.Empty);
        }

        public event EventHandler Closed = delegate { };

        public void SetModel(Core.Catalogue catalogue)
        {
            catalogueBindingSource.DataSource = catalogue;
            bookBindingSource.DataSource = catalogue.Books;
        }

        public void UpdateModel()
        {
            catalogueBindingSource.ResetBindings(false);
            bookBindingSource.ResetBindings(false);
        }
    }
}
