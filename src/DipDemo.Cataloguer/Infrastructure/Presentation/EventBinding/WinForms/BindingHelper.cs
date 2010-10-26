using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DipDemo.Cataloguer.Infrastructure.Presentation.EventBinding.WinForms
{
    public static class BindingHelper
    {
        public static void FindAndConnectMethodsWithOneParameterFor<T>(
            IPresenter presenter, Action<T, Delegate> connect, string suffix, string elementPrefix)
            where T : Control
        {
            var presenterType = presenter.GetType();
            var elements = GetViewControls(presenter.View).OfType<T>();
            var methodsAndElements = from method in GetActionMethods(presenterType)
                                     where method.Name.EndsWith(suffix)
                                     where method.GetParameters().Length == 1
                                     let elementName = method.Name.Substring(
                                         Convensions.EventHandlerPrefix.Length, 
                                         method.Name.Length - Convensions.EventHandlerPrefix.Length - suffix.Length)
                                     let matchingElement = elements.FirstOrDefault(x => x.Name == elementPrefix + elementName)
                                     where matchingElement != null
                                     select new { method, matchingElement };

            foreach (var methodAndEvent in methodsAndElements)
            {
                var parameterType = methodAndEvent.method.GetParameters()[0].ParameterType;
                var action = Delegate.CreateDelegate(typeof(Action<>).MakeGenericType(parameterType),
                                                     presenter, methodAndEvent.method);

                connect(methodAndEvent.matchingElement, action);
            }
        }

        public static IEnumerable<Control> GetViewControls(IView view)
        {
            var winFormView = view as Control;
            if (winFormView == null)
                throw new ArgumentException("view is not a control", "view");

            var controls = (winFormView).Controls.Cast<Control>();

            var foundControls = new List<Control>();
            foundControls.AddRange(controls);

            foreach (var control in controls)
            {
                foundControls.AddRange(GetControls(control));
            }

            return foundControls;
        }

        public static IEnumerable<Control> GetControls(Control control)
        {
            var controls = control.Controls.Cast<Control>();

            var list = new List<Control>();
            list.AddRange(controls);

            foreach (var ctrl in controls)
            {
                list.Add(ctrl);
                list.AddRange(GetControls(ctrl));
            }

            return list;
        }

        public static IEnumerable<MethodInfo> GetParameterlessActionMethods(Type type)
        {
            return from method in GetActionMethods(type)
                   where method.GetParameters().Length == 0
                   select method;
        }

        public static IEnumerable<MethodInfo> GetActionMethods(Type type)
        {
            return from method in type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                   where method.Name.StartsWith(Convensions.EventHandlerPrefix)
                   select method;
        }
    }
}