using System.Linq;
using System.Windows.Forms;

namespace DipDemo.Cataloguer.Infrastructure.Presentation.EventBinding.WinForms
{
    public class ButtonEnablinessBinder : IEventBinder
    {
        public void Bind(IPresenter presenter)
        {
            var presenterType = presenter.GetType();
            var controls = BindingHelper.GetViewControls(presenter.View).OfType<Button>();

            var facts = presenterType.GetProperties()
                .Where(x =>
                       x.PropertyType == typeof(Fact)
                       && x.Name.StartsWith(Convensions.EnablinessPropertyPrefix));

            foreach (var fact in facts)
            {
                string factName = fact.Name;
                var btn = controls.FirstOrDefault(x => factName.EndsWith(x.Name.Substring(Convensions.ButtonPrefix.Length)));
                if (btn == null)
                    continue;

                Fact factInst = (Fact)fact.GetValue(presenter, null);
                new EnableControlCommand(btn, factInst);
            }
        }
    }
}