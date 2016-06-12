using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Threading;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cl.ColorOfLeftButton == cl.getCorrectColor())
            {
                cl.CurrentMark++;
                cl.timerOff();
                cl.next();
            }
            else
            {
                cl.timerOff();
                cl.CallGameLostContentDiagAsync();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (cl.ColorOfMiddleButton == cl.getCorrectColor())
            {
                cl.CurrentMark++;
                cl.timerOff();
                cl.next();
            }
            else
            {
                cl.timerOff();
                cl.CallGameLostContentDiagAsync();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (cl.ColorOfRightButton == cl.getCorrectColor())
            {
                cl.CurrentMark++;
                cl.timerOff();
                cl.next();
            }
            else
            {
                cl.timerOff();
                cl.CallGameLostContentDiagAsync();
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


    public class GameLogicViewModel : INotifyPropertyChanged
    {
        Windows.Storage.ApplicationDataContainer localSettings =
                                           Windows.Storage.ApplicationData.Current.LocalSettings;

        public List<Color> ColorList;
        public List<Color> currentColorList;
        private List<string> currentTempOrderedColorList;
        private ObservableCollection<string> currentOrderedColorList;
        DependencyObject progressBar;
        public ObservableCollection<string> CurrentOrderedColorList
        {
            get { return currentOrderedColorList; }
            set { currentOrderedColorList = value; }
        }
        private string colorOfLeftButton;

        public string ColorOfLeftButton
        {
            get { return colorOfLeftButton; }
            set
            {
                colorOfLeftButton = value;
                PropertyChangedEventHandler handler = this.PropertyChanged;
                if (handler != null)
                {
                    handler.Invoke(this, new PropertyChangedEventArgs(nameof(ColorOfLeftButton)));
                }
            }
        }
        private string colorOfMiddleButton;

        public string ColorOfMiddleButton
        {
            get { return colorOfMiddleButton; }
            set
            {
                colorOfMiddleButton = value;
                PropertyChangedEventHandler handler = this.PropertyChanged;
                if (handler != null)
                {
                    handler.Invoke(this, new PropertyChangedEventArgs(nameof(ColorOfMiddleButton)));
                }
            }
        }
        private string colorOfRightButton;

        public string ColorOfRightButton
        {
            get { return colorOfRightButton; }
            set
            {
                colorOfRightButton = value;
                PropertyChangedEventHandler handler = this.PropertyChanged;
                if (handler != null)
                {
                    handler.Invoke(this, new PropertyChangedEventArgs(nameof(ColorOfRightButton)));
                }
            }
        }

        public void setValueToButton()
        {
            ColorOfLeftButton = currentTempOrderedColorList[0];
            ColorOfMiddleButton = currentTempOrderedColorList[1];
            ColorOfRightButton = currentTempOrderedColorList[2];
        }


        public Random r;
        public ThreadPoolTimer _TimerOfLost;
        public ThreadPoolTimer _TimerOfProgressValue;
        private Windows.UI.Xaml.Visibility visiblityOfReplayButton;
        public Visibility VisiblityOfReplayButton
        {
            get
            {
                return visiblityOfReplayButton;
            }

            set
            {
                visiblityOfReplayButton = value;
                PropertyChangedEventHandler handler = this.PropertyChanged;
                if (handler != null)
                {
                    handler.Invoke(this, new PropertyChangedEventArgs(nameof(VisiblityOfReplayButton)));
                }
            }
        }
        private int currentMark;

        public int CurrentMark
        {
            get { return currentMark; }
            set
            {
                currentMark = value;
                PropertyChangedEventHandler handler = this.PropertyChanged;
                if (handler != null)
                {
                    handler.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentMark)));
                }
            }
        }


        private string backgroundColor;

        public string BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                backgroundColor = value;
                PropertyChangedEventHandler handler = this.PropertyChanged;
                if (handler != null)
                {
                    handler.Invoke(this, new PropertyChangedEventArgs(nameof(BackgroundColor)));
                }
            }
        }

        private string stringColor;

        public string StringColor
        {
            get { return stringColor; }
            set
            {
                stringColor = value;
                PropertyChangedEventHandler handler = this.PropertyChanged;
                if (handler != null)
                {
                    handler.Invoke(this, new PropertyChangedEventArgs(nameof(StringColor)));
                }
            }
        }

        private string currentColorOfStringMeaning;

        public string CurrentColorOfStringMeaning
        {
            get
            {
                return currentColorOfStringMeaning;
            }

            set
            {
                currentColorOfStringMeaning = value;
                PropertyChangedEventHandler handler = this.PropertyChanged;
                if (handler != null)
                {
                    handler.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentColorOfStringMeaning)));
                }
            }
        }

        private double currentMaxValueOfProgressBar;

        public double CurrentMaxValueOfProgressBar
        {
            get { return currentMaxValueOfProgressBar; }
            set
            {
                currentMaxValueOfProgressBar = value;
                PropertyChangedEventHandler handler = this.PropertyChanged;
                if (handler != null)
                {
                    handler.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentMaxValueOfProgressBar)));
                }
            }
        }

        private double currentValueOfProgressBar;

        public double CurrentValueOfProgressBar
        {
            get { return currentValueOfProgressBar; }
            set { currentValueOfProgressBar = value;
                PropertyChangedEventHandler handler = this.PropertyChanged;
                if (handler != null)
                {
                    handler.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentValueOfProgressBar)));
                }
            }
        }

        public int toBeSelectindex { get; set; }

        private string theStringToBeSelect;

        public string TheStringToBeSelect
        {
            get { return theStringToBeSelect; }
            set
            {
                theStringToBeSelect = value;
                PropertyChangedEventHandler handler = this.PropertyChanged;
                if (handler != null)
                {
                    handler.Invoke(this, new PropertyChangedEventArgs(nameof(TheStringToBeSelect)));
                }
            }
        }



        public string[] toBeSelectMessages;

        public event PropertyChangedEventHandler PropertyChanged;



        public GameLogicViewModel(DependencyObject progressBar)
        {
            this.progressBar = progressBar;
            currentMark = 0;
            currentMaxValueOfProgressBar = Tools.MAXTIME;
            currentValueOfProgressBar = currentMaxValueOfProgressBar;
            visiblityOfReplayButton = Visibility.Collapsed;
            ColorList = new List<Color> {Colors.Black,
                Colors.Blue,Colors.Brown,Colors.Gold,Colors.Gray,
                Colors.Green,Colors.Orange,Colors.Pink,Colors.Purple,
                Colors.Red,Colors.White,Colors.Yellow };
            //ColorList = new List<string> {"Black",
            //    "Blue","Brown","Gold","Gray",
            //    "Green","Orange","Pink","Purple",
            //    "Red","White","Yellow" };


            toBeSelectMessages = new string[] { "Background", "Color", "Meaning" };

            r = new Random(DateTime.Now.Day * DateTime.Now.Hour * DateTime.Now.Minute * DateTime.Now.Second);



            currentColorList = getThreeColors();
            currentTempOrderedColorList = new List<string>();
            BackgroundColor = currentColorList[0].ToString();
            StringColor = currentColorList[1].ToString();
            CurrentColorOfStringMeaning = currentColorList[2].ToString();

            currentColorList.copyTo(currentTempOrderedColorList);
            currentTempOrderedColorList.Sort();
            setColorToBeSelectindex();
            setValueToButton();
            CurrentOrderedColorList = new ObservableCollection<string>(currentTempOrderedColorList);
            timerOn();
        }
        public List<Color> getThreeColors()
        {
            int i = r.Next(ColorList.Count);
            int j = r.Next(ColorList.Count);
            int k = r.Next(ColorList.Count);
            while (i == j)
            {
                j = r.Next(ColorList.Count);
            }
            while (i == k || j == k)
            {
                k = r.Next(ColorList.Count);
            }
            return new List<Color> { ColorList[i], ColorList[j], ColorList[k] };
        }
        public void setColorToBeSelectindex()
        {
            toBeSelectindex = r.Next(3);
            TheStringToBeSelect = toBeSelectMessages[toBeSelectindex];
        }
        public string getCorrectColor()
        {
            if (toBeSelectindex == 0)
            {
                return backgroundColor;
            }
            else if (toBeSelectindex == 1)
            {
                return stringColor;
            }
            else
                return currentColorOfStringMeaning;
        }

        public void timerOn()
        {
            _TimerOfProgressValue = ThreadPoolTimer.CreatePeriodicTimer(
                async (source) =>
                {

                    await progressBar.Dispatcher.RunAsync(
                              CoreDispatcherPriority.Normal,
                               () =>
                               {
                                   if (CurrentValueOfProgressBar>=0)
                                   {
                                       CurrentValueOfProgressBar -= Tools.INTERVALTIME;
                                   } 
                               }
                  );
                }, TimeSpan.FromMilliseconds(Tools.INTERVALTIME));



            _TimerOfLost = ThreadPoolTimer.CreateTimer(
                async (source) =>
                {
                    
                    await progressBar.Dispatcher.RunAsync(
                              CoreDispatcherPriority.Normal,
                               async () =>
                              {
                                  await CallGameLostContentDiagAsync();
                              }
                  );

                },
                TimeSpan.FromMilliseconds(currentMaxValueOfProgressBar));

        }
        public void timerOff()
        {
            if (_TimerOfLost != null)
            {
                _TimerOfLost.Cancel();
                _TimerOfLost = null;
            }
            if (_TimerOfProgressValue != null)
            {
                _TimerOfProgressValue.Cancel();
                _TimerOfProgressValue = null;
            }

        }

        public void next()
        {
            currentColorList = getThreeColors();
            currentTempOrderedColorList = new List<string>();
            BackgroundColor = currentColorList[0].ToString();
            StringColor = currentColorList[1].ToString();
            CurrentColorOfStringMeaning = currentColorList[2].ToString();

            currentColorList.copyTo(currentTempOrderedColorList);
            currentTempOrderedColorList.Sort();
            setColorToBeSelectindex();
            setValueToButton();
            timerOn();
            CurrentMaxValueOfProgressBar -= Tools.INTERVALTIME;
            CurrentValueOfProgressBar = CurrentMaxValueOfProgressBar;
        }
        public async Task CallGameLostContentDiagAsync()
        {
            timerOff();
            if (CurrentMark > Convert.ToInt32(localSettings.Values["MaxScore"].ToString()))
            {
                localSettings.Values["MaxScore"] = CurrentMark;
            }
            ContentDialog GameLostContentDiagAsync = new ContentDialog()
            {
                Title = "Score",
                Content = "Score："+CurrentMark.ToString()+"\nMax："+localSettings.Values["MaxScore"],
                PrimaryButtonText = "Replay"
            };
            GameLostContentDiagAsync.PrimaryButtonClick += GameLostContentDiag_PrimaryButtonClick;

             await GameLostContentDiagAsync.ShowAsync();
        }

        private void GameLostContentDiag_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            CurrentMark = 0;
            CurrentMaxValueOfProgressBar = Tools.MAXTIME;
            CurrentValueOfProgressBar = CurrentMaxValueOfProgressBar;
            //  VisiblityOfReplayButton = Visibility.Collapsed;
            r = new Random(DateTime.Now.Day * DateTime.Now.Hour * DateTime.Now.Minute * DateTime.Now.Second);
            currentColorList = getThreeColors();
            currentTempOrderedColorList = new List<string>();
            BackgroundColor = currentColorList[0].ToString();
            StringColor = currentColorList[1].ToString();
            CurrentColorOfStringMeaning = currentColorList[2].ToString();

            currentColorList.copyTo(currentTempOrderedColorList);
            currentTempOrderedColorList.Sort();
            setColorToBeSelectindex();
            setValueToButton();
            timerOn();
        }
    }
}

