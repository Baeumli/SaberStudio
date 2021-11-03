using SaberStudio.Modules.Browser.ViewModels;
using SaberStudio.Services.BeatSaver.Parser.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SaberStudio.Modules.Browser.Views
{
    /// <summary>
    /// Interaction logic for MapBrowserView.xaml
    /// </summary>
    public partial class MapBrowserView : UserControl
    {
        public MapBrowserView()
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

        private void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                var viewModel = DataContext as MapBrowserViewModel;
                viewModel?.SelectedCommand.Execute();
            }
            e.Handled = true;
        }
    }
}
