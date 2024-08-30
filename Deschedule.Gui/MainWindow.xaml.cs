using Deschedule.Main.Models;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.IO;
using Windows.UI.Popups;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Deschedule.Gui
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    ///

    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            if (!Directory.Exists(Environment.CurrentDirectory + "\\ScheduleSets\\"))
            {
                Directory.CreateDirectory("ScheduleSets");
            }
#if DEBUG
            Dummy.Visibility = Visibility.Visible;
            Debug.Visibility = Visibility.Visible;
#endif
            this.AppWindow.Resize(new Windows.Graphics.SizeInt32(753, 586));
            var presenter = this.AppWindow.Presenter as OverlappedPresenter;
            presenter.IsResizable = false;
            presenter.IsMaximizable = false;
            MainFrame.Navigate(typeof(SchedulesSettings));
        }

        public static string FilePath;

        private void SaveAndLaunch_Click(object sender, RoutedEventArgs e)
        {
            ScheduleMgr.SaveData(FilePath);
            MessageDialog messageDialog = new MessageDialog("Todo");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ScheduleMgr.SaveData(FilePath);
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Exit();
        }

        private void Navigation_SelectionChanged(SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
        {
            switch (Navigation.SelectedItem.Tag.ToString())
            {
                case "SchedulesSettings":
                    MainFrame.Navigate(typeof(SchedulesSettings));
                    break;

                case "BackgroundSettings":
                    MainFrame.Navigate(typeof(BackgroundSettings));
                    break;

                case "BootSettings":
                    MainFrame.Navigate(typeof(BootSettings));
                    break;

                default:
                    ElementSoundPlayer.State = ElementSoundPlayerState.On;
                    ElementSoundPlayer.Play(ElementSoundKind.Focus);
                    break;
            }
        }

        private void Dummy_Click(object sender, RoutedEventArgs e)
        {
            ScheduleMgr.GenerateDummyData();
        }

        private async void Debug_Click(object sender, RoutedEventArgs e)
        {
            string DebugInfo = "Debug Information Generated" + "|" + FilePath;
            this.Title = "Deschedule 设置" + DebugInfo;
            ContentDialog messageDialog = new ContentDialog();
            messageDialog.XamlRoot = App.m_window.Content.XamlRoot;
            messageDialog.Content = DebugInfo;
            messageDialog.CloseButtonText = "好";
            await messageDialog.ShowAsync();
        }
    }
}