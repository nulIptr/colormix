using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace demo3th
{
    using ColorKeyValuePair = KeyValuePair<string, string>;
    public class GameLogicViewModel : INotifyPropertyChanged
    {
        Windows.Storage.ApplicationDataContainer localSettings =
                                           Windows.Storage.ApplicationData.Current.LocalSettings;

        public List<ColorKeyValuePair> ColorList;
        public List<ColorKeyValuePair> currentColorList;
        private List<ColorKeyValuePair> currentTempOrderedColorList;
        DependencyObject progressBar;
        public Dictionary<string, string> ColorDict;

        private ColorKeyValuePair colorOfLeftButton;

        public ColorKeyValuePair ColorOfLeftButton
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
        private ColorKeyValuePair colorOfMiddleButton;

        public ColorKeyValuePair ColorOfMiddleButton
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
        private ColorKeyValuePair colorOfRightButton;

        public ColorKeyValuePair ColorOfRightButton
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


        private ColorKeyValuePair backgroundColor;

        public ColorKeyValuePair BackgroundColor
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

        private ColorKeyValuePair stringColor;

        public ColorKeyValuePair StringColor
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

        private ColorKeyValuePair currentColorOfStringMeaning;

        public ColorKeyValuePair CurrentColorOfStringMeaning
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
            set
            {
                currentValueOfProgressBar = value;
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
            initGameData();
            visiblityOfReplayButton = Visibility.Collapsed;
            ColorList = new List<KeyValuePair<string, string>>(ColorDictionary.ColorDict);
            toBeSelectMessages = new string[] { "Background", "Color", "Meaning" };

            r = new Random(DateTime.Now.Day * DateTime.Now.Hour * DateTime.Now.Minute * DateTime.Now.Second);
            setDataToLayout();
            timerOn();
        }
        public List<ColorKeyValuePair> getThreeColors()
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
            return new List<ColorKeyValuePair> { ColorList[i], ColorList[j], ColorList[k] };
        }
        public void setColorToBeSelectindex()
        {
            toBeSelectindex = r.Next(3);
            TheStringToBeSelect = toBeSelectMessages[toBeSelectindex];
        }
        public ColorKeyValuePair getCorrectColor()
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
                                   if (CurrentValueOfProgressBar >= 0)
                                   {
                                       CurrentValueOfProgressBar -= Tools.REFERSHINTERVAL;
                                   }
                               }
                  );
                }, TimeSpan.FromMilliseconds(Tools.REFERSHINTERVAL));



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
            setDataToLayout();
            timerOn();
            CurrentMaxValueOfProgressBar -= Tools.Decrease();
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
                Content = "Score：" + CurrentMark.ToString() + "\nMax：" + localSettings.Values["MaxScore"],
                PrimaryButtonText = "Replay"
            };
            GameLostContentDiagAsync.PrimaryButtonClick += GameLostContentDiag_PrimaryButtonClick;

            await GameLostContentDiagAsync.ShowAsync();
        }

        private void GameLostContentDiag_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            initGameData();
            //  VisiblityOfReplayButton = Visibility.Collapsed;
            r = new Random(DateTime.Now.Day * DateTime.Now.Hour * DateTime.Now.Minute * DateTime.Now.Second);
            setDataToLayout();
            timerOn();
        }
        public void initGameData()
        {
            CurrentMark = 0;
            CurrentMaxValueOfProgressBar = Tools.MAXTIME;
            CurrentValueOfProgressBar = CurrentMaxValueOfProgressBar;
        }
        public void setDataToLayout()
        {
            
            currentColorList = getThreeColors();
            currentTempOrderedColorList = new List<ColorKeyValuePair>(currentColorList);
            BackgroundColor = currentColorList[0];
            StringColor = currentColorList[1];
            CurrentColorOfStringMeaning = currentColorList[2];

            currentTempOrderedColorList.Sort((x, y) => (x.Key.CompareTo(y.Key)));
            setColorToBeSelectindex();
            setValueToButton();
        }
    }

}
