using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Deschedule.Main
{
    /// <summary>
    /// Video.xaml 的交互逻辑
    /// </summary>
    public partial class Video : Page
    {
        public Video()
        {
            InitializeComponent();
            string Path = MainWindow.arguments["videoPath"];
            VideoPlayback.Source = new Uri(Path);
            if (MainWindow.arguments.ContainsKey("volume"))
            {
                VideoPlayback.Volume = int.Parse(MainWindow.arguments["volume"]);
            }
            VideoPlayback.Play();
            
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void VideoPlayback_MediaEnded(object sender, RoutedEventArgs e)
        {
            VideoPlayback.Position = TimeSpan.MinValue;
        }
    }
}
