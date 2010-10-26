using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DipDemo.Cataloguer.Infrastructure.Presentation.EventBinding.WinForms
{
    public class MenuClickEventBinder : IEventBinder
    {
        public void Bind(IPresenter presenter)
        {
            var presenterType = presenter.GetType();
            var menu = BindingHelper.GetViewControls(presenter.View).OfType<MenuStrip>().FirstOrDefault();
            if (menu == null)
                return;

            var items = GetMenuItems(menu);

            var presenterMethods = BindingHelper.GetParameterlessActionMethods(presenterType);

            foreach (var method in presenterMethods)
            {
                string methodName = method.Name;
                var item = items.FirstOrDefault(
                    x => x.Name == Convensions.MenuItemPrefix + methodName.Substring(Convensions.EventHandlerPrefix.Length));

                if (item == null)
                    continue;

                var action = (Action)Delegate.CreateDelegate(typeof(Action), presenter, method.Name);

                var handler = (EventHandler)((sender, args) => action());
                item.Click += handler;
            }
        }

        // ограничение на элементы который имеют только один уровень вложенности
        // для того, чтобы сделать 2 нужно
        private List<ToolStripMenuItem> GetMenuItems(MenuStrip menu)
        {
            var controls = menu.Items.OfType<ToolStripMenuItem>();
            List<ToolStripMenuItem> items = new List<ToolStripMenuItem>();
            foreach (var control in controls)
            {
                items.AddRange(GetDropDownItems(control));
            }
            return items;
        }

        private List<ToolStripMenuItem> GetDropDownItems(ToolStripMenuItem control)
        {
            var items = new List<ToolStripMenuItem>();

            var controls = control.DropDownItems.OfType<ToolStripMenuItem>();
            foreach (var item in controls)
            {
                items.AddRange(GetDropDownItems(item));
            }

            items.AddRange(controls);
            return items;
        }
    }
}
