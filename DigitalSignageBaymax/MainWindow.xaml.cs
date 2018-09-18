namespace DigitalSignageBaymax
{
    #region DLL

    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using GestureDefinition;
    using Microsoft.Kinect.Toolkit;
    using Microsoft.Kinect.Toolkit.Controls;
    using System.Reflection;
    using System.IO;
    using PluginInterface;
    using System.Diagnostics;
    using WindowsInput;
    using System.Runtime.InteropServices;
    using WpfScreenHelper;


    #endregion

    public partial class MainWindow
    {
        #region Mause

        [DllImport("user32")]
        public static extern int SetCursorPos(int x, int y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(UInt32 dwflags, int dx, int dy, int cButtons, IntPtr dwExtraInfo);
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x02;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x04;
        private const UInt32 MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const UInt32 MOUSEEVENTF_RIGHTUP = 0x10;

        #endregion

        #region Window Properties

        internal static string xmlUrl = @"../../../Config.xml";
        GestureDefinition gestureDefinition = new GestureDefinition();
        ReadConfig readConfig = new ReadConfig();
        MainContent mainContent = new MainContent();
        Caruzele caruzele = new Caruzele();
        MainGallery mainGallery = new MainGallery();
        Layout layout = new Layout();
        TweetContent tweetCaruseleContant = new TweetContent();
        MethodOfEventButton methodOfEventButton = new MethodOfEventButton();
        ButtonPath buttonPath = new ButtonPath();
        private KinectTileButton button = new KinectTileButton();
        public static List<Button> buttonsCopy = new List<Button>();
        public static List<WaterMark> WaterMarkList = new List<WaterMark>();
        public static List<Movie> movieCopy = new List<Movie>();
        List<Tutorial> tutosialList = new List<Tutorial>();
        TextBlock tweetsTextCOPY = new TextBlock();
        TextBlock timeTweetsCOPY = new TextBlock();
        private ExitEnum exitEnum = ExitEnum.NULL;
        private VolueZoom vlueZoom = VolueZoom.NULL;
        private readonly KinectSensorChooser sensorChooser;
        private string sourseRead = string.Empty;
        List<Assembly> pluginsDll = new List<Assembly>();
        private string tempolaryPathReader = string.Empty;
        private bool screenEvent, iInitializeImageCarusele, eventIsActive, switchSizeMainScreen = false;
        private Process process;
        private IPlugin typeLoadedFromPlugin;
        private int heighWindow = (int)Screen.PrimaryScreen.Bounds.Height;
        private int widthWindow = (int)Screen.PrimaryScreen.Bounds.Width;
        private double walueSpeeker = 0;
        private bool typeGridContent;
        private bool[] enableDisableGesture = new bool[12];
        private int counterWatherScreen, timeEventScreen = 0;
        #endregion

        #region MainWindow

        public MainWindow()
        {

            InitializeComponent();

            SetCursorPos(0, 0);

            GestureEnum GestureEnum = new GestureEnum();
            this.sensorChooser = new KinectSensorChooser();
            this.sensorChooserUi.KinectSensorChooser = this.sensorChooser;
            this.sensorChooser.Start();
            var regionSensorBinding = new Binding("Kinect") { Source = this.sensorChooser };
            BindingOperations.SetBinding(this.kinectRegion, KinectRegion.KinectSensorProperty, regionSensorBinding);

            this.Loaded += new RoutedEventHandler(gestureDefinition.MainWindow_Loaded);
            this.Closed += new EventHandler(gestureDefinition.MainWindow_Closed);
            gestureDefinition.OnGestrue += EventGesture;

            setParhInWindowsFromConfig();
        }

        #endregion

        #region Plugins

        private List<Assembly> GetAssemblies(string directory, string nameDLL)
        {
            var assemblies = new List<Assembly>();
            if (Directory.Exists(directory))
            {
                foreach (var file in Directory.GetFiles(directory, string.Format("{0}*.dll", nameDLL)))
                {
                    assemblies.Add(Assembly.LoadFrom(file));
                }
            }
            return assemblies;
        }

        private void InitializePlugins(PlugsEnumKey plugsEnum)
        {
            foreach (var assembly in pluginsDll)
            {
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (type.IsClass && type.IsPublic && type.GetInterface(typeof(IPlugin).FullName) != null)
                    {
                        switch (plugsEnum)
                        {
                            case PlugsEnumKey.PLAY:
                                typeLoadedFromPlugin = (IPlugin)Activator.CreateInstance(type);
                                typeLoadedFromPlugin.Initialize(ref GridSnakeScreen, ref GameOver, ref GridSnake);
                                typeLoadedFromPlugin.GameSnakePlay();
                                break;
                            case PlugsEnumKey.DOWN_KEY:
                                typeLoadedFromPlugin.DownGesturePassive();
                                break;
                            case PlugsEnumKey.LEFT_KEY:
                                typeLoadedFromPlugin.LeftGesturePassive();
                                break;
                            case PlugsEnumKey.RIGHT_KEY:
                                typeLoadedFromPlugin.RightGesturePassive();
                                break;
                            case PlugsEnumKey.UP_KEY:
                                typeLoadedFromPlugin.UpGesturePassive();
                                break;
                        }
                        break;
                    }
                }
                break;
            }
        }

        private void UpGesturePassive(object sender, RoutedEventArgs e)
        {
            InitializePlugins(PlugsEnumKey.UP_KEY);
        }

        private void DownGesturePassive(object sender, RoutedEventArgs e)
        {
            InitializePlugins(PlugsEnumKey.DOWN_KEY);
        }

        private void RightGesturePassive(object sender, RoutedEventArgs e)
        {
            InitializePlugins(PlugsEnumKey.RIGHT_KEY);
        }

        private void LeftGesturePassive(object sender, RoutedEventArgs e)
        {
            InitializePlugins(PlugsEnumKey.LEFT_KEY);
        }

        #endregion

        #region Set Config

        private List<Label> AddTextCentralCarusele()
        {
            List<Label> TextCentralCarusele = new List<Label>();

            for (int i = 1; i <= 15; i++)
            {
                Label TextCentral = LogicalTreeHelper.FindLogicalNode(CentralGrid, string.Format("TextCentral{0}", i)) as Label;
                TextCentralCarusele.Add(TextCentral);
            }

            return TextCentralCarusele;
        }

        private void SetEnableDisableGesture(int index, Gesture gesture)
        {
            if (gesture.enable == true)
                enableDisableGesture[index] = true;
            else
                enableDisableGesture[index] = false;
        }

        private void SetHorizontalAlignmentMetroContent()
        {
            switch (layout.HorizontalAlignment)
            {
                case "Center":
                    MetroContent.HorizontalAlignment = HorizontalAlignment.Center;
                    break;
                case "Left":
                    MetroContent.HorizontalAlignment = HorizontalAlignment.Left;
                    break;
                case "Right":
                    MetroContent.HorizontalAlignment = HorizontalAlignment.Right;
                    break;
            }
        }

        private void setParhInWindowsFromConfig()
        {

            MovieOne.Height = heighWindow;
            MovieOne.Width = widthWindow;

            layout = readConfig.GetLayoutConfig();
            SetHorizontalAlignmentMetroContent();
            MainGridWindowAplication.Visibility = Visibility.Hidden;
            timeEventScreen = layout.timeWatingScreenSaver * 150;
            tutosialList = readConfig.GetTutorialConfig();
            movieCopy = readConfig.GetMovieConfig();
            buttonsCopy = readConfig.GetButtonConfig();
            WaterMarkList = readConfig.GetWaterMarksConfig();
            mainGallery.InitTimerCarusele(layout.SpeedGallery);
            Volum.Value = 0;

            mainGallery.SetWhiteHeightCanvasGallery(Width, Height, Next, Prev, NextGallery, PrevGallery, GalleryShow, PresentationJPG);

            typeGridContent = mainContent.AddButtonInMainContent(LeftContent, footerScroll, scrollViewerAddons, scrollViewer, MainGridWindowAplication, MovieMain,
                MovieOne, MetroContent, Animation, MetroStyle, MainContent, Addons, FooterContent, Movie, OpenOutProgram, Gallery, InteractiveImages,
                OpenWeb, GalleryTypeTwo, GamePlug);

            caruzele.GetCopyLable(AddTextCentralCarusele());
            caruzele.SetCaruzeleContentPath();
            caruzele.InitTimerCarusele();

            try
            {
                foreach (WaterMark waterMark in WaterMarkList)
                {
                    switch (waterMark.id)
                    {
                        case "WaterMarkExitMark":
                            exitMark.Source = new BitmapImage(new Uri(waterMark.Source, UriKind.RelativeOrAbsolute));
                            break;
                        case "WaterMarkNextMark":
                            break;
                        case "WaterMarkPrevMark":
                            break;
                    }
                }

                foreach (Gesture gesture in readConfig.GetGestureConfig())
                {
                    switch (gesture.id)
                    {
                        case "VOLUME/ZOOM":
                            SetEnableDisableGesture(0, gesture);
                            break;
                        case "EXIT":
                            SetEnableDisableGesture(1, gesture);
                            break;
                        case "SYMULATE MOUSE CLIK":
                            SetEnableDisableGesture(2, gesture);
                            break;
                        case "DOWN":
                            SetEnableDisableGesture(3, gesture);
                            break;
                        case "LEFT":
                            SetEnableDisableGesture(4, gesture);
                            break;
                        case "UP":
                            SetEnableDisableGesture(5, gesture);
                            break;
                        case "MINIMALIZE":
                            SetEnableDisableGesture(6, gesture);
                            break;
                        case "MAXYMALIZE":
                            SetEnableDisableGesture(7, gesture);
                            break;
                        case "OPEN WEB":
                            SetEnableDisableGesture(8, gesture);
                            break;
                        case "NEXT":
                            SetEnableDisableGesture(9, gesture);
                            break;
                        case "PRIEV":
                            SetEnableDisableGesture(10, gesture);
                            break;
                        case "POSSIBLE":
                            SetEnableDisableGesture(11, gesture);
                            break;
                    }
                }

                foreach (Logo logo in readConfig.GetBackLogoConfig())
                {
                    switch (logo.id)
                    {
                        case "MainLogo":
                            LogoMain.Source = new BitmapImage(new Uri(logo.Source, UriKind.RelativeOrAbsolute));
                            break;
                        case "LogoLeft":
                            LogoLeft.Width = widthWindow / 7;
                            LogoLeft.Source = new BitmapImage(new Uri(logo.Source, UriKind.RelativeOrAbsolute));
                            break;
                        case "OffLogo":
                            LogoFooter.Source = new BitmapImage(new Uri(logo.Source, UriKind.RelativeOrAbsolute));
                            break;
                    }
                }

                foreach (Movie movie in movieCopy)
                {
                    switch (movie.id)
                    {
                        case "MovieMain":
                            if (!File.Exists(movie.Source) && !movie.Source.Equals(""))
                                MessageBox.Show(string.Format("the source fore {0} don't exist ", movie.id));
                            MediaElementMain.Source = new System.Uri(movie.Source, UriKind.RelativeOrAbsolute);
                            break;
                        case "MovieOne":
                            if (!File.Exists(movie.Source) && !movie.Source.Equals(""))
                                MessageBox.Show(string.Format("the source fore {0} don't exist ", movie.id));
                            if (movie.Source.Equals(""))
                            {
                                MetroScroll.Margin = new Thickness(-40, 20, 0, 0);
                                MetroContent.Margin = new Thickness(0, 0, 0, 0); 
                            }
                            MediaElementSlave.Source = new System.Uri(movie.Source, UriKind.RelativeOrAbsolute);
                            break;
                    }
                }

                foreach (BackGround backGround in readConfig.GetBackGroundConfig())
                {
                    switch (backGround.id)
                    {
                        case "BackGround":
                            BackGround.Source = new BitmapImage(new Uri(backGround.Source, UriKind.RelativeOrAbsolute));
                            break;
                        case "Gallery":
                            BackGroundGallery.Source = new BitmapImage(new Uri(backGround.Source, UriKind.RelativeOrAbsolute));
                            break;
                        case "InteractiveImages":
                            BackGroundInteractiveImages.Source = new BitmapImage(new Uri(backGround.Source, UriKind.RelativeOrAbsolute));
                            break;
                        case "Game":
                            BackGroundGame.Source = new BitmapImage(new Uri(backGround.Source, UriKind.RelativeOrAbsolute));
                            break;
                        case "OpenWWWPage":
                            BackGroundOpenWWWPage.Source = new BitmapImage(new Uri(backGround.Source, UriKind.RelativeOrAbsolute));
                            break;
                        case "Movie":
                            BackGroundMovie.Source = new BitmapImage(new Uri(backGround.Source, UriKind.RelativeOrAbsolute));
                            break;
                        case "BackGroundMain":
                            BackGroundLayoutRandomJpgShower.Source = new BitmapImage(new Uri(backGround.Source, UriKind.RelativeOrAbsolute));
                            GridOpenOutProgramBackGround.Source = new BitmapImage(new Uri(backGround.Source, UriKind.RelativeOrAbsolute));
                            break;
                        case "BackGroundRingAnimation":
                            Animation.Source = new BitmapImage(new Uri(backGround.Source, UriKind.RelativeOrAbsolute));
                            break;
                        case "ShutDown":
                            ShutDown.Source = new BitmapImage(new Uri(backGround.Source, UriKind.RelativeOrAbsolute));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("chack the source, ByMax cant found the file {0}", ex.Message));
            }
        }

        #endregion

        #region Event Gesture Detected

        private void EventGesture(GestureEnum.GestureName eventNumber)
        {
            SetMainUserColor();
            counterWatherScreen++;

            switch (eventNumber)
            {
                case GestureEnum.GestureName.VOLUME:
                    if (enableDisableGesture[0] == true)
                        EventVolumeDetected();
                    break;
                case GestureEnum.GestureName.STOP:
                    if (enableDisableGesture[1] == true)
                        ExitStop();
                    break;
                case GestureEnum.GestureName.PLAY:
                    if (enableDisableGesture[2] == true)
                        ClickMouseSimulatorT800();
                    break;
                case GestureEnum.GestureName.MINIMALIZE:
                    if (enableDisableGesture[6] == true)
                        MainScreen.WindowState = WindowState.Minimized;
                    break;
                case GestureEnum.GestureName.UP:
                    InputSimulatorKey(VirtualKeyCode.UP, true);
                    break;
                case GestureEnum.GestureName.DOWN:
                    if (enableDisableGesture[3] == true)
                        InputSimulatorKey(VirtualKeyCode.DOWN, true);
                    break;
                case GestureEnum.GestureName.LEFT:
                    if (enableDisableGesture[4] == true)
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.LEFT);
                    break;
                case GestureEnum.GestureName.RIGHT:
                    if (enableDisableGesture[5] == true)
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.RIGHT);
                    break;
                case GestureEnum.GestureName.AGAIN:
                    InputSimulator.SimulateKeyPress(VirtualKeyCode.ESCAPE);
                    break;
                case GestureEnum.GestureName.MAXYMALIZE:
                    if (enableDisableGesture[7] == true)
                        MainScreen.WindowState = WindowState.Maximized;
                    break;
                case GestureEnum.GestureName.DISABLESCREENSAVER:
                    counterWatherScreen = 0;
                    ScreenEventMethod();
                    break;
                case GestureEnum.GestureName.ENABLESCREENSAVER:
                    ScreenEventMethodOff();
                    break;
                case GestureEnum.GestureName.POSSIBLE:
                    if (enableDisableGesture[11] == true)
                        OpenPOSSIBLEweb();
                    break;
                case GestureEnum.GestureName.NEXT:
                    if (enableDisableGesture[9] == true)
                        NextDetected();
                    break;
                case GestureEnum.GestureName.PRIEV:
                    if (enableDisableGesture[10] == true)
                        PrieveDetected();
                    break;
                case GestureEnum.GestureName.WEB:
                    if (enableDisableGesture[8] == true)
                        ;
                    break;
            }
        }

        private void ScreenEventMethodOff()
        {
            if (screenEvent == true && counterWatherScreen >= timeEventScreen)
            {
                MainGridWindowAplication.Visibility = Visibility.Hidden;
                screenEvent = false;
                MovieMain.Volume = 0.0;
                counterWatherScreen = 0;
                ExitStop();
            }
        }

        private void ScreenEventMethod()
        {
            if (layout.ScreenEvent == false && screenEvent == false)
            {
                MovieMain.Volume = 0.8;
                screenEvent = true;
                MainGridWindowAplication.Visibility = Visibility.Visible;

                foreach (Button botton in buttonsCopy)
                {
                    if (botton.EventStartUp == true)
                    {
                        switch (botton.Event)
                        {
                            case "Movie":
                                MovieMethod(null, botton.id);
                                break;
                            case "ExeOpenWithParameter":
                                OpenOutProgramMethod(null, botton.id);
                                break;
                            case "Gallery":
                                GalleryMethod(null, botton.id);
                                break;
                            case "InteractiveImages":
                                InteractiveImagesMethod(null, botton.id);
                                break;
                            case "OpenWWWPage":
                                OpenWebMethod(null, botton.id);
                                break;
                            case "ParseImagesFromFile":
                                GalleryTypeTwoMethod(null, botton.id);
                                break;
                            case "GameSnakePlay":
                                GamePlugMethod(null, botton.id);
                                break;
                            case "Twiiter":
                                break;
                        }
                    }
                }
            }
            else 
            {
                MovieMain.Volume = 0.8;
                screenEvent = true;
                MainGridWindowAplication.Visibility = Visibility.Visible;
            }
        }

        private void OpenPOSSIBLEweb()
        {
            System.Diagnostics.Process.Start("http://www.possible.com");
            exitEnum = ExitEnum.OpenOutProgram;
        }

        private void PrieveDetected()
        {
            if (vlueZoom == VolueZoom.PresentationEvent)
                InputSimulatorKey(VirtualKeyCode.RIGHT, true);
            else
            {
                mainGallery.timer.Tick -= new EventHandler(mainGallery.GaruseleStarted);
                mainGallery.moveIndex(1);
                if (iInitializeImageCarusele == true)
                    mainGallery.InitializeImage(-1);
            }
        }

        private void NextDetected()
        {
            if (vlueZoom == VolueZoom.PresentationEvent)
                InputSimulatorKey(VirtualKeyCode.LEFT, true);
            else
            {
                mainGallery.timer.Tick -= new EventHandler(mainGallery.GaruseleStarted);
                mainGallery.moveIndex(-1);
                if (iInitializeImageCarusele == true)
                    mainGallery.InitializeImage(1);
            }
        }

        private void EventVolumeDetected()
        {
            switch (vlueZoom)
            {
                case VolueZoom.MapeEvent:
                    Mape.Width = (double)MainScreen.ActualWidth * (double)((gestureDefinition.volume) * 5);
                    break;
                case VolueZoom.PresentationEvent:
                    if (gestureDefinition.volume > 0.5)
                        InputSimulatorKey(VirtualKeyCode.VK_A, false);
                    else
                        InputSimulatorKey(VirtualKeyCode.VK_Z, false);
                    break;
                case VolueZoom.MovieEvent:
                    ti1eMovie.Volume = gestureDefinition.volume;
                    Volum.Value = (gestureDefinition.volume);
                    break;
                default:
                    MovieMain.Volume = gestureDefinition.volume;
                    Volum.Value = (gestureDefinition.volume);
                    break;
            }
        }

        private void SetMainUserColor()
        {
            if (gestureDefinition.GoodLocationForGestures == true)
            {
                if (gestureDefinition.slaveIsOn == true)
                {
                    TempolarySwitch.Content = "Blue";

                    if (gestureDefinition.gamerIsOn == true)
                        TempolarySwitch.Content = "Violet";
                }
                else
                    TempolarySwitch.Content = "Orange";
            }
            else
                TempolarySwitch.Content = "Red";
        }

        private void InputSimulatorKey(VirtualKeyCode volue, bool boolVolue)
        {
            InputSimulator.SimulateKeyPress(volue);
            InputSimulator.SimulateKeyDown(volue);
            switch (boolVolue)
            {
                case true:
                    KeyBoiardUp600Ms(volue);
                    break;
                case false:
                    KeyBoiardUp100Ms(volue);
                    break;
            }
        }

        private async void KeyBoiardUp600Ms(VirtualKeyCode volue)
        {
            System.Threading.Thread.Sleep(450);
            InputSimulator.SimulateKeyUp(volue);
        }

        private async void KeyBoiardUp100Ms(VirtualKeyCode volue)
        {
            System.Threading.Thread.Sleep(100);
            InputSimulator.SimulateKeyUp(volue);
        }

        #endregion

        #region Method MainWindow

        private void OpenInitPathEvent(ExitEnum exitEnum)
        {
            ShowTutorial(exitEnum);
            eventIsActive = true;
            walueSpeeker = Volum.Value;
            MovieMain.Volume = 0.0;
            ExitKinectButton.Visibility = Visibility.Visible;
            LeftMovie.Visibility = Visibility.Hidden;
            animationContent.Visibility = Visibility.Hidden;
            CentralGrid.Visibility = Visibility.Hidden;
            switch (typeGridContent)
            {
                case true:
                    RightMain.Visibility = Visibility.Hidden;
                    LeftContent.Visibility = Visibility.Hidden;
                    break;
                case false:
                    MetroStyle.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void Clip_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message);
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.sensorChooser.Stop();
        }

        private void ExitStop()
        {
            if (eventIsActive == true)
            {
                ExitKinectButton.Visibility = Visibility.Hidden;
                LeftMovie.Visibility = Visibility.Visible;
                animationContent.Visibility = Visibility.Visible;
                CentralGrid.Visibility = Visibility.Visible;

                switch (typeGridContent)
                {
                    case true:
                        RightMain.Visibility = Visibility.Visible;
                        LeftContent.Visibility = Visibility.Visible;
                        break;
                    case false:
                        MetroStyle.Visibility = Visibility.Visible;
                        break;
                }

                switch (exitEnum)
                {
                    case ExitEnum.Gallery:
                        GridGallery.Visibility = Visibility.Hidden;
                        mainGallery.timer.Tick -= new EventHandler(mainGallery.GaruseleStarted);
                        break;
                    case ExitEnum.OpenOutProgram:
                        GridOpenOutProgram.Visibility = Visibility.Hidden;
                        if (!process.HasExited)
                            process.Kill();
                        vlueZoom = VolueZoom.NULL;
                        SetCursorPos(0, 0);
                        break;
                    case ExitEnum.GalleryTypeTwo:
                        GridGalleryTwo.Visibility = Visibility.Hidden;
                        iInitializeImageCarusele = false;
                        break;
                    case ExitEnum.OpenWeb:
                        OpenWWWPageGrid.Visibility = Visibility.Hidden;
                        WebPagePossible.Navigate("about:blank");
                        break;
                    case ExitEnum.Movie:
                        vlueZoom = VolueZoom.NULL;
                        GridMovie.Visibility = Visibility.Hidden;
                        ti1eMovie.Stop();
                        break;
                    case ExitEnum.InteractiveImages:
                        vlueZoom = VolueZoom.NULL;
                        GridMape.Visibility = Visibility.Hidden;
                        break;
                    case ExitEnum.GamePlug:
                        GridSnake.Visibility = Visibility.Hidden;
                        break;
                    default:
                        break;
                }

                MovieMain.Volume = walueSpeeker;
                Volum.Value = walueSpeeker;
                ShowTutorial(ExitEnum.NULL);

                if (MainScreen.WindowState == WindowState.Minimized)
                {
                    MainScreen.WindowState = WindowState.Maximized;
                    MainScreen.WindowStyle = WindowStyle.None;
                }

                eventIsActive = !eventIsActive;

                InputSimulator.SimulateKeyPress(VirtualKeyCode.ESCAPE);

                exitEnum = ExitEnum.NULL;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    NextDetected();
                    break;
                case Key.Right:
                    PrieveDetected();
                    break;
                case Key.Escape:
                    ExitStop();
                    break;
                case Key.F12:
                    SwitchSizeMainScreen();
                    break;
                case Key.F11:
                    gestureDefinition.VoiceOn = gestureDefinition.slaveIsOn;
                    break;
                case Key.P:
                    MainGridWindowAplication.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void SwitchSizeMainScreen()
        {
            if (switchSizeMainScreen == false)
            {
                MainScreen.WindowState = WindowState.Normal;
                MainScreen.WindowStyle = WindowStyle.ThreeDBorderWindow;
            }
            else
            {
                MainScreen.WindowState = WindowState.Maximized;
                MainScreen.WindowStyle = WindowStyle.None;
            }

            switchSizeMainScreen = !switchSizeMainScreen;
        }

        private void ClickMouseSimulatorT800()
        {
            SetCursorPos(widthWindow / 2, heighWindow / 2);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, new System.IntPtr());
            KeyMouseUp(MOUSEEVENTF_LEFTUP);
        }

        private async void KeyMouseUp(UInt32 buttonMause)
        {
            System.Threading.Thread.Sleep(100);
            mouse_event(buttonMause, 0, 0, 0, new System.IntPtr());
        }

        private void carouselwindow_Loaded(object sender, RoutedEventArgs e)
        {
            mainGallery.Start();
        }

        private void setContentTutorial(string nameSource)
        {
            foreach (Tutorial tutorial in tutosialList)
            {
                if (nameSource.Equals(tutorial.id) && tutorial.Active == true)
                {
                    switch (tutorial.Type)
                    {
                        case true:
                            if (!File.Exists(tutorial.Source))
                                MessageBox.Show(string.Format("the source fore {0} don't exist ", tutorial.id));
                            else
                            {
                                Tutorial.Visibility = Visibility.Visible;
                                Tutorial.Source = new Uri(tutorial.Source, UriKind.RelativeOrAbsolute);
                                Tutorial.Volume = tutorial.Volue;
                                Tutorial.Play();
                            }
                            break;
                        case false:
                            try
                            {
                                GestureTutorial.Visibility = Visibility.Visible;
                                GestureTutorial.Height = TutorialContentImage.ActualHeight;
                                GestureTutorial.Source = new BitmapImage(new Uri(tutorial.Source, UriKind.RelativeOrAbsolute));
                            }
                            catch (Exception ex)
                            {
                                GestureTutorial.Visibility = Visibility.Hidden;
                                MessageBox.Show(string.Format("chack the source image, ByMax cant found the file {0}", ex.Message));
                            }
                            break;
                    }
                    break;
                }
            }
        }

        private void ShowTutorial(ExitEnum value)
        {
            switch (value)
            {
                case ExitEnum.Gallery:
                    setContentTutorial("Gallery");
                    break;
                case ExitEnum.GalleryTypeTwo:
                    setContentTutorial("Gallery");
                    break;
                case ExitEnum.GamePlug:
                    setContentTutorial("GamePlug");
                    break;
                case ExitEnum.InteractiveImages:
                    setContentTutorial("InteractiveImages");
                    break;
                case ExitEnum.Movie:
                    setContentTutorial("Movie");
                    break;
                case ExitEnum.OpenOutProgram:
                    setContentTutorial("OpenOutProgram");
                    break;
                case ExitEnum.OpenWeb:
                    setContentTutorial("OpenWeb");
                    break;
                case ExitEnum.NULL:
                    GestureTutorial.Visibility = Visibility.Hidden;
                    Tutorial.Visibility = Visibility.Hidden;
                    Tutorial.Stop();
                    break;
            }
        }

        #endregion

        #region Events

        private void ExitButton(object sender, RoutedEventArgs e)
        {
            ExitStop();
        }

        private void GamePlug(object sender, RoutedEventArgs e)
        {
            GamePlugMethod(e, null);
        }

        private void InteractiveImages(object sender, RoutedEventArgs e)
        {
            InteractiveImagesMethod(e, null);
        }

        private void Gallery(object sender, RoutedEventArgs e)
        {
            GalleryMethod(e, null);
        }

        private void OpenOutProgram(object sender, RoutedEventArgs e)
        {
            OpenOutProgramMethod(e, null);
        }

        private void GalleryTypeTwo(object sender, RoutedEventArgs e)
        {
            GalleryTypeTwoMethod(e, null);
        }

        private void OpenWeb(object sender, RoutedEventArgs e)
        {
            OpenWebMethod(e, null);
        }

        private void Movie(object sender, RoutedEventArgs e)
        {
            MovieMethod(e, null);
        }

        private void MovieMethod(RoutedEventArgs e, string buttonName)
        {
            if (buttonName != null)
                buttonPath = methodOfEventButton.getButtonPath(buttonName);
            else
                buttonPath = methodOfEventButton.getButtonPath((KinectTileButton)e.OriginalSource);
            GridMovie.Visibility = Visibility.Visible;
            ti1eMovie.Source = new Uri(buttonPath.source, UriKind.RelativeOrAbsolute);
            ti1eMovie.Play();
            vlueZoom = VolueZoom.MovieEvent;
            exitEnum = ExitEnum.Movie;
            OpenInitPathEvent(exitEnum);
        }

        private void OpenWebMethod(RoutedEventArgs e, string buttonName)
        {
            if (buttonName != null)
                buttonPath = methodOfEventButton.getButtonPath(buttonName);
            else
                buttonPath = methodOfEventButton.getButtonPath((KinectTileButton)e.OriginalSource);
            switch (buttonPath.eventType)
            {
                case "Twiiter":
                    sourseRead = string.Format(@"https://twitter.com/{0}", buttonPath.source);
                    break;
                default:
                    sourseRead = buttonPath.source;
                    break;
            }
            OpenWWWPageGrid.Visibility = Visibility.Visible;
            WebPagePossible.Navigate(new Uri(sourseRead));
            exitEnum = ExitEnum.OpenWeb;
            OpenInitPathEvent(exitEnum);
        }

        private void GalleryTypeTwoMethod(RoutedEventArgs e, string buttonName)
        {
            if (buttonName != null)
                buttonPath = methodOfEventButton.getButtonPath(buttonName);
            else
                buttonPath = methodOfEventButton.getButtonPath((KinectTileButton)e.OriginalSource);
            GridGalleryTwo.Visibility = Visibility.Visible;
            mainGallery.GetImageFiles(buttonPath.source, buttonPath.path1, true);
            iInitializeImageCarusele = true;
            exitEnum = ExitEnum.GalleryTypeTwo;
            OpenInitPathEvent(exitEnum);
        }

        private void OpenOutProgramMethod(RoutedEventArgs e, string buttonName)
        {
            SetCursorPos(widthWindow / 2, heighWindow / 2);
            if (buttonName != null)
                buttonPath = methodOfEventButton.getButtonPath(buttonName);
            else
                buttonPath = methodOfEventButton.getButtonPath((KinectTileButton)e.OriginalSource);
            vlueZoom = VolueZoom.PresentationEvent;
            GridOpenOutProgram.Visibility = Visibility.Visible;
            try
            {
                process = Process.Start(buttonPath.path1, string.Format("{0} {1}", buttonPath.path2, buttonPath.source));
                exitEnum = ExitEnum.OpenOutProgram;
                OpenInitPathEvent(exitEnum);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("wrong source {0}", ex));
                GridOpenOutProgram.Visibility = Visibility.Hidden;
                exitEnum = ExitEnum.NULL;
            }
        }

        private void GalleryMethod(RoutedEventArgs e, string buttonName)
        {
            if (buttonName != null)
                buttonPath = methodOfEventButton.getButtonPath(buttonName);
            else
                buttonPath = methodOfEventButton.getButtonPath((KinectTileButton)e.OriginalSource);
            GridGallery.Visibility = Visibility.Visible;
            mainGallery.GetImageFiles(buttonPath.source, buttonPath.path1, false);
            exitEnum = ExitEnum.Gallery;
            OpenInitPathEvent(exitEnum);
        }

        private void InteractiveImagesMethod(RoutedEventArgs e, string buttonName)
        {
            if (buttonName != null)
                buttonPath = methodOfEventButton.getButtonPath(buttonName);
            else
                buttonPath = methodOfEventButton.getButtonPath((KinectTileButton)e.OriginalSource);
            GridMape.Visibility = Visibility.Visible;
            Mape.Source = new BitmapImage(new Uri(buttonPath.source, UriKind.RelativeOrAbsolute));
            vlueZoom = VolueZoom.MapeEvent;
            exitEnum = ExitEnum.InteractiveImages;
            OpenInitPathEvent(exitEnum);
        }

        private void GamePlugMethod(RoutedEventArgs e, string buttonName)
        {
            GridSnakeScreen.Width = GridSnake.ActualWidth - 170;
            GridSnakeScreen.Height = GridSnake.ActualHeight - 170;
            if (buttonName != null)
                buttonPath = methodOfEventButton.getButtonPath(buttonName);
            else
                buttonPath = methodOfEventButton.getButtonPath((KinectTileButton)e.OriginalSource);
            pluginsDll = GetAssemblies(buttonPath.path2, buttonPath.path1);
            GameOver.Visibility = Visibility.Hidden;
            GridSnake.Visibility = Visibility.Visible;
            InitializePlugins(PlugsEnumKey.PLAY);
            eventIsActive = true;
            exitEnum = ExitEnum.GamePlug;
            OpenInitPathEvent(exitEnum);
        }

        #endregion

    }
}
