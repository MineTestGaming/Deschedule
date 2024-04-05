using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Deschedule.ExternalDll;
using Deschedule.Main.Models;
using Newtonsoft.Json.Linq;

namespace Deschedule.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Dictionary<string, string> arguments = new Dictionary<string, string>();
        public List<DisplaySchedule> displaySchedules = [];
        string[] args = Environment.GetCommandLineArgs();
        public MainWindow()
        {
            InitializeComponent();
            if (args.Length != 0)
            {

                try
                {

                    for (int i = 1 ; i < args.Length; i+= 2)
                    {
                        arguments.Add(args[i].Replace("--", String.Empty), args[i+1]);
                    }
                }
                catch (System.IndexOutOfRangeException) 
                {
                    ArgumentNotExpected();
                }
            } // 初始化参数
            else
            {
                ArgumentNotExpected();
            }
            if (arguments.ContainsKey("videoPath"))
            {
                Background.Navigate(new Video()) ;
            }
            if (arguments.ContainsKey("imagePath"))
            {
                Background.Navigate(new Image()) ;
            }
            if (arguments.ContainsKey("jsonSettings"))
            {
                Models.ScheduleMgr.ImportSettings(arguments["jsonSettings"]);
            }
            if (arguments.ContainsKey("webAddress"))
            {
                Background.Navigate(new WebView()) ;
            }

#if DEBUG
            Models.ScheduleMgr.GenerateDummyData();
#endif
            // Listview 美化
            
            // 读取课程
            displaySchedules = Models.ScheduleMgr.ConvertSchedule();
            ScheduleDisplay.ItemsSource = displaySchedules;








        }

        private void ArgumentNotExpected()
        {
            MessageBox.Show("Argument Not Defined, See Internal Document" + "/n" + args[0] + args[1]); ;
            App.Current.Shutdown();                
        }

        public void SetWallpaper()
        {
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            var desktop = Win32Api.FindWindow("Progman", null); // 设置桌面窗口变量
            IntPtr result = IntPtr.Zero;
            Win32Api.SendMsgTimeout(desktop, 0x52c,UIntPtr.Zero,IntPtr.Zero,0,2,result); // 给桌面发送0x52c信息以使其分解

            Win32Api.EnumWindows((hWnd, lParam) => // 枚举窗口
            {
                if (Win32Api.FindWindowEx(hWnd, IntPtr.Zero, "SHELLDLL_DefView", null) != IntPtr.Zero) // 找到需要隐藏的WorkerW窗口
                {
                    IntPtr tmpWnd = Win32Api.FindWindowEx(IntPtr.Zero, hWnd, "WorkerW", null); // 找到父级的WorkerW窗口
                    Win32Api.ShowWindow(tmpWnd, 0); // 使其隐藏
                }
                return true;

            }, IntPtr.Zero);
            Win32Api.SetParent(new WindowInteropHelper(this).Handle, desktop); // 使窗口后置于壁纸层
            this.Top = 0;
            this.Left = 0;
            this.Height = Win32Api.GetSystemMetrics(1); // 调整窗口为屏幕高度
            this.Width = Win32Api.GetSystemMetrics(0); // 同理，屏幕高度
            SetWpLayer.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!arguments.ContainsKey("windowed"))
            {
                SetWallpaper();
            }
            else if (arguments["windowed"] == "false")
            {
                SetWallpaper();
            }
            else
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.ResizeMode = ResizeMode.CanResize;
            }

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void SetWpLayer_Click(object sender, RoutedEventArgs e)
        {
            SetWallpaper();
        }
    }
}