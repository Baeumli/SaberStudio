using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace SaberStudio.UI.Extensions
{
    // https://stackoverflow.com/a/27742886
    public static class TextBlockUrlExtension
    {
        
        public static string GetFormattedText(DependencyObject obj)
        {
            return (string)obj.GetValue(FormattedTextProperty);
        }

        public static void SetFormattedText(DependencyObject obj, string value)
        {
            obj.SetValue(FormattedTextProperty, value);
        }

        public static readonly DependencyProperty FormattedTextProperty =
            DependencyProperty.RegisterAttached("FormattedText", typeof(string), typeof(TextBlockUrlExtension),
                new PropertyMetadata(string.Empty, (sender, e) =>
                {
                    if (!(sender is TextBlock textBlock)) return;

                    var text = e.NewValue as string;

                    textBlock.Inlines.Clear();
                    var regex = new Regex(@"(https?://[^\s]+)", RegexOptions.IgnoreCase);
                    var str = regex.Split(text ?? string.Empty);

                    for (var i = 0; i < str.Length; i++)
                    {
                        if (i % 2 == 0)
                        {
                            textBlock.Inlines.Add(new Run { Text = str[i] });
                        }
                        else
                        {
                            var link = new Hyperlink { NavigateUri = new Uri(str[i]), Foreground = Brushes.Blue };
                            link.RequestNavigate += Link_RequestNavigate;
                            link.Inlines.Add(new Run { Text = str[i] });
                            textBlock.Inlines.Add(link);
                        }
                    }
                }));

        private static void Link_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", e.Uri.ToString());
        }
    }
}
