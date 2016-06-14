using System;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace demo3th
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class corelogic : Page
    {
        public GameLogicViewModel cl;
        public corelogic()
        {
            Windows.Storage.ApplicationDataContainer localSettings =
                                           Windows.Storage.ApplicationData.Current.LocalSettings;
            this.InitializeComponent();
            cl = new GameLogicViewModel(progressBar1);
            DataContext = cl;

            if (localSettings.Values["MaxScore"] == null)
            {
                localSettings.Values["MaxScore"] = 0;
            }

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if ( cl.ColorOfLeftButton.EqualTo(cl.getCorrectColor()))
            {
                cl.CurrentMark++;
                cl.timerOff();
                cl.next();
            }
            else
            {
                cl.timerOff();
                await cl.CallGameLostContentDiagAsync();
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (cl.ColorOfMiddleButton.EqualTo(cl.getCorrectColor()))
            {
                cl.CurrentMark++;
                cl.timerOff();
                cl.next();
            }
            else
            {
                cl.timerOff();
                await cl.CallGameLostContentDiagAsync();
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (cl.ColorOfRightButton.EqualTo(cl.getCorrectColor()))
            {
                cl.CurrentMark++;
                cl.timerOff();
                cl.next();
            }
            else
            {
                cl.timerOff();
                await cl.CallGameLostContentDiagAsync();
            }
        }

        private void Replay_Click(object sender, RoutedEventArgs e)
        {
            cl.CurrentMark = 0;
            //   mpvm.VisiblityOfReplayButton = Visibility.Collapsed;
            cl.r = new Random(DateTime.Now.Day * DateTime.Now.Hour * DateTime.Now.Minute * DateTime.Now.Second);
            cl.next();
        }
    }


}

