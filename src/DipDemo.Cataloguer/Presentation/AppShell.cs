using System;
using System.Windows.Forms;
using DipDemo.Cataloguer.Core;
using DipDemo.Cataloguer.Events;
using DipDemo.Cataloguer.Infrastructure.AppController;
using DipDemo.Cataloguer.Infrastructure.EventAggregator;
using DipDemo.Cataloguer.Infrastructure.IoC;
using DipDemo.Cataloguer.Presentation.ViewCatalogue;
using DipDemo.Cataloguer.Requests;

namespace DipDemo.Cataloguer.Presentation
{
    public partial class AppShell : Form, IEventHandler<CatalogueOpenedEvent>
    {
        private readonly IApplicationController applicationController;
        private Catalogue currentCataloguer;

        public AppShell(IDependencyResolver dependencyResolver, IApplicationController applicationController)
        {
            this.applicationController = applicationController;
            InitializeComponent();

            dependencyResolver.Inject<ICatalogueView>(catalogueView1);
        }

        public void ProcessEvent(CatalogueOpenedEvent data)
        {
            currentCataloguer = data.Catalogue;
            SetOpenedState();
        }

        private void SetOpenedState()
        {
            catalogueView1.Enabled = true;
            mnuSave.Enabled = true;
            mnuSaveAs.Enabled = true;
        }

        private void SetClosedState()
        {
            catalogueView1.Enabled = false;
            mnuSave.Enabled = false;
            mnuSaveAs.Enabled = false;
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            applicationController.ProcessRequest(new NewCatalogueRequest(currentCataloguer));
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            if (CloseCurrentCatalogue())
                return;

            SetClosedState();

            applicationController.ProcessRequest(new OpenCatalogueRequest());
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            applicationController.ProcessRequest(new SaveCatalogueRequest(currentCataloguer));
        }

        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            applicationController.ProcessRequest(new SaveCatalogueAsRequest(currentCataloguer));
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            if (!CloseCurrentCatalogue())
                Close();
        }

        private void AppShell_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = CloseCurrentCatalogue();
        }

        private bool CloseCurrentCatalogue()
        {
            var reply = applicationController.RequestReply<CloseCatalogueRequest, CloseCatalogueResponse>(
                new CloseCatalogueRequest(currentCataloguer));

            return reply.IsCancelled;
        }
    }
}
