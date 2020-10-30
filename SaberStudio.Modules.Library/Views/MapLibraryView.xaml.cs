using SaberStudio.Modules.Library.ViewModels;
using SaberStudio.Services.BeatSaber.Parser.Models;
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

namespace SaberStudio.Modules.Library.Views
{
    /// <summary>
    /// Interaction logic for MapLibraryView.xaml
    /// </summary>
    public partial class MapLibraryView : UserControl
    {
        public MapLibraryView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event that routes MouseWheelEventArgs on the ListView control to the parent ScrollViewer
        /// </summary>
        private void MapList_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
                {
                    RoutedEvent = MouseWheelEvent,
                    Source = sender
                };
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }

        private void MapList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as MapLibraryViewModel;
            if (e.AddedItems.Count > 0)
            {
                viewModel.SelectedCommand.Execute(e.AddedItems[0] as BeatMap);
            }
        }
    }
}
