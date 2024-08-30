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
    /// Image.xaml 的交互逻辑
    /// </summary>
    public partial class Image : Page
    {
        public Image()
        {
            InitializeComponent();
            BitmapImage Image = new BitmapImage();
            Image.BeginInit();
            Image.UriSource = new Uri(MainWindow.arguments["imagePath"].Replace("\"", ""));
            Image.EndInit();
            ImgContent.Source = Image;
        }
    }
}
