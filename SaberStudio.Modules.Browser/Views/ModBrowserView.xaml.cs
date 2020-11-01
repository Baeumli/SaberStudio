using SaberStudio.Modules.Browser.ViewModels;
using SaberStudio.Services.BeatMods.Models.Parser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SaberStudio.Modules.Browser.Views
{
    /// <summary>
    /// Interaction logic for ModBrowserView.xaml
    /// </summary>
    public partial class ModBrowserView : UserControl
    {
        public ModBrowserView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event that routes MouseWheelEventArgs on the ListView control to the parent ScrollViewer
        /// </summary>
        private void ListView_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Handled) return;
            e.Handled = true;
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
            {
                RoutedEvent = MouseWheelEvent,
                Source = sender
            };
            var parent = ((Control)sender).Parent as UIElement;
            parent.RaiseEvent(eventArg);
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as ModBrowserViewModel;
            if (e.AddedItems.Count > 0)
            {
                viewModel.SelectedCommand.Execute(e.AddedItems[0] as Mod);
            }
        }
    }
}
