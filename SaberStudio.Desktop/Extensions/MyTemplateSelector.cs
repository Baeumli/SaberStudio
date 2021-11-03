using System;
using System.Windows;
using System.Windows.Controls;
using SaberStudio.Desktop.Models;

namespace SaberStudio.Desktop.Extensions
{
    public class MyTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (!(container is FrameworkElement elem))
                return null;

            if (item is null)
                throw new ApplicationException();

            if (!(item is Setting))
                throw new ApplicationException();

            var setting = (Setting) item;

            return setting.DataType switch
            {
                DataType.Path => elem.FindResource("PathDataTemplate") as DataTemplate,
                DataType.Text => elem.FindResource("StringDataTemplate") as DataTemplate,
                DataType.Bool => elem.FindResource("BoolDataTemplate") as DataTemplate,

                _ => throw new ApplicationException()
            };
        }
    }
}
