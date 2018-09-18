using System;
using System.Linq;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Xml.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Data;

namespace OpenFile
{
    public partial class MainWindow : Window
    {
        private string writeln;
        private string attribute = string.Empty;
        private string imagrFiltr = "Image (*.jpg)|*.jpg|Image (*.png)|*.png|All (*.*)|*.*";
        private string XmlFiltr = "xml (*.xml)|*.xml";
        private int buttonNumber, contentText = 0;
        OpenFileDialog openFileDialog = new OpenFileDialog();
        ReadConfig readConfig = new ReadConfig();

        public MainWindow()
        {
            InitializeComponent();
            addButtonContent(MainContent, false, "", "", "", "", EnnumListBox.Null, EnnumListContent.MContent, EnnumListSize.Metro11, EnnumListTweet.NULL, false);
            addButtonContent(LeftContent, false, "", "", "", "", EnnumListBox.Null, EnnumListContent.LContent, EnnumListSize.Metro11, EnnumListTweet.NULL, false);
        }

        private void setActiveTutorial(string value, ComboBox combo)
        {
            switch (value)
            {
                case "true":
                    combo.SelectedIndex = 1;
                    break;
                case "false":
                    combo.SelectedIndex = 0;
                    break;
            }
        }

        private void LoadContent(object content)
        {
            readConfig.xmlUrl = getSourceConfig();

            LayoutXml layoutXmlConfig = readConfig.GetLayoutConfig();

            AccessTokenTwiitXml AccessTokenTwiit = readConfig.GetAccessTokenTwiitXml();
            OAuthConsumerKey.Text = AccessTokenTwiit.OAuthConsumerKey;
            OAuthConsumerSecret.Text = AccessTokenTwiit.OAuthConsumerSecret;

            if (content != null)
            {
                switch (layoutXmlConfig.HeightButtonWidthButton)
                {
                    case "130":
                        HorizontalVerticalHW.SelectedIndex = 0;
                        break;
                    case "140":
                        HorizontalVerticalHW.SelectedIndex = 1;
                        break;
                    case "150":
                        HorizontalVerticalHW.SelectedIndex = 2;
                        break;
                    case "160":
                        HorizontalVerticalHW.SelectedIndex = 3;
                        break;
                    case "170":
                        HorizontalVerticalHW.SelectedIndex = 4;
                        break;
                    case "180":
                        HorizontalVerticalHW.SelectedIndex = 5;
                        break;
                    case "190":
                        HorizontalVerticalHW.SelectedIndex = 6;
                        break;
                    case "200":
                        HorizontalVerticalHW.SelectedIndex = 7;
                        break;
                    case "210":
                        HorizontalVerticalHW.SelectedIndex = 8;
                        break;
                    case "220":
                        HorizontalVerticalHW.SelectedIndex = 9;
                        break;
                    case "230":
                        HorizontalVerticalHW.SelectedIndex = 10;
                        break;
                    case "240":
                        HorizontalVerticalHW.SelectedIndex = 11;
                        break;
                    case "250":
                        HorizontalVerticalHW.SelectedIndex = 12;
                        break;
                    case "260":
                        HorizontalVerticalHW.SelectedIndex = 13;
                        break;
                    case "270":
                        HorizontalVerticalHW.SelectedIndex = 14;
                        break;
                    case "280":
                        HorizontalVerticalHW.SelectedIndex = 15;
                        break;
                    case "290":
                        HorizontalVerticalHW.SelectedIndex = 16;
                        break;
                    case "300":
                        HorizontalVerticalHW.SelectedIndex = 17;
                        break;
                }

                switch (layoutXmlConfig.Animation)
                {
                    case true:
                        AnimationBackGround.SelectedIndex = 0;
                        break;
                    case false:
                        AnimationBackGround.SelectedIndex = 1;
                        break;
                }
                BorderBrush.Text = layoutXmlConfig.BorderBrush;

                switch (layoutXmlConfig.Opacity)
                {
                    case "0,5":
                        Opacity.SelectedIndex = 0;
                        break;
                    case "0,6":
                        Opacity.SelectedIndex = 1;
                        break;
                    case "0,7":
                        Opacity.SelectedIndex = 2;
                        break;
                    case "0,8":
                        Opacity.SelectedIndex = 3;
                        break;
                    case "0,9":
                        Opacity.SelectedIndex = 4;
                        break;
                    default:
                        Opacity.SelectedIndex = 5;
                        break;
                }

                switch (layoutXmlConfig.timeWatingScreenSaver)
                {
                    case "15":
                        timeWatingScreenSaver.SelectedIndex = 0;
                        break;
                    case "30":
                        timeWatingScreenSaver.SelectedIndex = 1;
                        break;
                    case "45":
                        timeWatingScreenSaver.SelectedIndex = 2;
                        break;
                    default:
                        timeWatingScreenSaver.SelectedIndex = 3;
                        break;
                }

                switch (layoutXmlConfig.HorizontalVertical)
                {
                    case true:
                        HorizontalVertical.SelectedIndex = 0;
                        break;
                    case false:
                        HorizontalVertical.SelectedIndex = 1;
                        break;
                }

                foreach (LogoXml logo in readConfig.GetBackLogoConfig())
                {
                    switch (logo.id)
                    {
                        case "MainLogo":
                            MainLogo.Text = logo.Source;
                            RadioMainLogo.IsChecked = true;
                            break;
                        case "LogoLeft":
                            LogoLeft.Text = logo.Source;
                            RadioLogoLeft.IsChecked = true;
                            break;
                        case "OffLogo":
                            OffLogo.Text = logo.Source;
                            RadioOffLogo.IsChecked = true;
                            break;
                    }
                }

                foreach (TutorialXml tutorial in readConfig.GetTutorialConfig())
                {
                    switch (tutorial.id)
                    {
                        case "Gallery":
                            Tutorial1.Text = tutorial.Source;
                            setActiveTutorial(tutorial.Active, TutorialTrue1);
                            RadioTrueTutorial1.IsChecked = tutorial.Type;
                            break;
                        case "GamePlug":
                            Tutorial2.Text = tutorial.Source;
                            setActiveTutorial(tutorial.Active, TutorialTrue2);
                            RadioTrueTutorial2.IsChecked = tutorial.Type;
                            break;
                        case "InteractiveImages":
                            Tutorial3.Text = tutorial.Source;
                            setActiveTutorial(tutorial.Active, TutorialTrue3);
                            RadioTrueTutorial3.IsChecked = tutorial.Type;
                            break;
                        case "Movie":
                            Tutorial4.Text = tutorial.Source;
                            setActiveTutorial(tutorial.Active, TutorialTrue4);
                            RadioTrueTutorial4.IsChecked = tutorial.Type;
                            break;
                        case "OpenOutProgram":
                            Tutorial5.Text = tutorial.Source;
                            setActiveTutorial(tutorial.Active, TutorialTrue5);
                            RadioTrueTutorial5.IsChecked = tutorial.Type;
                            break;
                        case "OpenWeb":
                            Tutorial6.Text = tutorial.Source;
                            setActiveTutorial(tutorial.Active, TutorialTrue6);
                            RadioTrueTutorial6.IsChecked = tutorial.Type;
                            break;
                    }
                }

                foreach (MovieXml movie in readConfig.GetMovieConfig())
                {
                    switch (movie.id)
                    {
                        case "MovieMain":
                            MovieMain.Text = movie.Source;
                            break;
                        case "MovieOne":
                            MovieOne.Text = movie.Source;
                            break;
                    }
                }

                foreach (BackGroundXml backGround in readConfig.GetBackGroundConfig())
                {
                    switch (backGround.id)
                    {
                        case "BackGround":
                            BackGround.Text = backGround.Source;
                            break;
                        case "Gallery":
                            BackGroundGallery.Text = backGround.Source;
                            break;
                        case "InteractiveImages":
                            BackGroundInteractiveImages.Text = backGround.Source;
                            break;
                        case "OpenWWWPage":
                            BackGroundOpenWWWPage.Text = backGround.Source;
                            break;
                        case "Game":
                            BackGroundGame.Text = backGround.Source;
                            break;
                        case "Movie":
                            BackGroundMovie.Text = backGround.Source;
                            break;
                        case "BackGroundRingAnimation":
                            BackGroundRingAnimation.Text = backGround.Source;
                            break;
                        case "BackGroundMain":
                            BackGroundMain.Text = backGround.Source;
                            break;
                        case "ShutDown":
                            ShutDown.Text = backGround.Source;
                            break;
                    }
                }

                foreach (WaterMarkXml waterMark in readConfig.GetWaterMarksConfig())
                {
                    switch (waterMark.id)
                    {
                        case "WaterMarkMovie":
                            WaterMarkMovieSource.Text = waterMark.Source;
                            break;
                        case "WaterMarkOutSideProgram":
                            WaterMarkOutSideProgramSource.Text = waterMark.Source;
                            break;
                        case "WaterMarkGallery":
                            WaterMarkGallerySource.Text = waterMark.Source;
                            break;
                        case "WaterMarkInteractiveImages":
                            WaterMarkInteractiveImagesSource.Text = waterMark.Source;
                            break;
                        case "WaterMarkOpenWeb":
                            WaterMarkOpenWebSource.Text = waterMark.Source;
                            break;
                        case "WaterMarkGalleryParseFromFile":
                            WaterMarkGalleryParseFromFileSource.Text = waterMark.Source;
                            break;
                        case "WaterMarkExitMark":
                            WaterMarkExitMarkSource.Text = waterMark.Source;
                            break;
                        case "WaterMarkNextMark":
                            WaterMarkNextMarkSource.Text = waterMark.Source;
                            break;
                        case "WaterMarkPrevMark":
                            WaterMarkPrevMarkSource.Text = waterMark.Source;
                            break;
                    }
                }

                TextContentRight.Children.RemoveRange(1, TextContentRight.Children.Count);
                contentText = 0;

                DependencyObject dependencyObject = DigitalSignageConfig as DependencyObject;

                foreach (ContentTextXml text in readConfig.GetCaruseleTextConfig())
                {
                    addContentText(true, text.Text);
                    addBindingText();
                }

                LeftContent.Children.RemoveRange(26, LeftContent.Children.Count);
                MainContent.Children.RemoveRange(1, MainContent.Children.Count);
                FooterContent.Children.RemoveRange(1, FooterContent.Children.Count);
                buttonNumber = 0;

                EnnumListBox ennumEvent = new EnnumListBox();
                EnnumListSize sizeButton = new EnnumListSize();
                EnnumListTweet ennumListTweet = new EnnumListTweet();

                foreach (ButtonXml buttonXml in readConfig.GetButtonConfig())
                {
                    switch (buttonXml.Path2)
                    {
                        case "false":
                            ennumListTweet = EnnumListTweet.TWEET;
                            break;
                        case "true":
                            ennumListTweet = EnnumListTweet.HASTAG;
                            break;
                        default:
                            ennumListTweet = EnnumListTweet.NULL;
                            break;
                    }

                    switch (buttonXml.WindowSize)
                    {
                        case "1 : 1":
                            sizeButton = EnnumListSize.Metro11;
                            break;
                        case "1 : 2":
                            sizeButton = EnnumListSize.Metro12;
                            break;
                        case "2 : 2":
                            sizeButton = EnnumListSize.Metro22;
                            break;
                        case "2 : 1":
                            sizeButton = EnnumListSize.Metro21;
                            break;
                    }

                    switch (buttonXml.Event)
                    {
                        case "GamePlay":
                            ennumEvent = EnnumListBox.GameSnakePlay;
                            break;
                        case "Movie":
                            ennumEvent = EnnumListBox.Movie;
                            break;
                        case "ExeOpenWithParameter":
                            ennumEvent = EnnumListBox.ExeOpenWithParameter;
                            break;
                        case "Gallery":
                            ennumEvent = EnnumListBox.Gallery;
                            break;
                        case "InteractiveImages":
                            ennumEvent = EnnumListBox.InteractiveImages;
                            break;
                        case "OpenWWWPage":
                            ennumEvent = EnnumListBox.OpenWWWPage;
                            break;
                        case "GalleryTwo":
                            ennumEvent = EnnumListBox.ParseImagesFromFile;
                            break;
                        case "Twiiter":
                            ennumEvent = EnnumListBox.Twiiter;
                            break;
                    }

                    switch (buttonXml.IdContent)
                    {
                        case "Content Footer":
                            addButtonContent(FooterContent, true, buttonXml.Icon, buttonXml.Source, buttonXml.Path1, buttonXml.Path2, ennumEvent, EnnumListContent.FContent, sizeButton, ennumListTweet, buttonXml.EventStartUp);
                            break;
                        case "Content Left":
                            addButtonContent(LeftContent, true, buttonXml.Icon, buttonXml.Source, buttonXml.Path1, buttonXml.Path2, ennumEvent, EnnumListContent.LContent, sizeButton, ennumListTweet, buttonXml.EventStartUp);
                            break;
                        case "Content Main":
                            addButtonContent(MainContent, true, buttonXml.Icon, buttonXml.Source, buttonXml.Path1, buttonXml.Path2, ennumEvent, EnnumListContent.MContent, sizeButton, ennumListTweet, buttonXml.EventStartUp);
                            break;
                    }
                }
            }
            WriterlnRead.Text = "Success loading the config.xml";
        }

        private void SetContent(object content, string Input, string Name)
        {
            if (content != null)
            {
                DependencyObject dependencyObject = content as DependencyObject;

                TextBox inputValue = LogicalTreeHelper.FindLogicalNode(dependencyObject, Name) as TextBox;
                if (inputValue != null)
                    inputValue.Text = Input;
            }
        }

        private void SetImageButton(object content, string Input, string Name)
        {
            if (content != null)
            {
                DependencyObject dependencyObject = content as DependencyObject;

                Image inputValue = LogicalTreeHelper.FindLogicalNode(dependencyObject, Name) as Image;
                if (inputValue != null)
                    inputValue.Source = new BitmapImage(new Uri(Input, UriKind.RelativeOrAbsolute));
            }
        }

        private void addContentText(bool read, string value)
        {

            GroupBox groupBox = new GroupBox
            {
                Name = string.Format("groupBoxTextContent{0}", contentText)
            };

            Label redSpark = new Label
            {
                Content = "*",
                Foreground = Brushes.Red
            };

            Button delButton = new Button
            {
                Name = string.Format("DelButtonText{0}", contentText),
                Height = 40,
                Width = 180,
                BorderThickness = new Thickness(2, 2, 2, 2),
                BorderBrush = Brushes.Red
            };
            WrapPanel WrapButtonDel = new WrapPanel();
            Image ImabeDellButton = new Image
            {
                Source = new BitmapImage(new Uri(@"Images/Remove.png", UriKind.RelativeOrAbsolute)),
                HorizontalAlignment = HorizontalAlignment.Left
            };
            Label labelButtonDell = new Label
            {
                Content = "REMOVE CONTENT",
                HorizontalAlignment = HorizontalAlignment.Right
            };
            WrapButtonDel.Children.Add(ImabeDellButton);
            WrapButtonDel.Children.Add(labelButtonDell);
            delButton.Content = WrapButtonDel;

            delButton.Click += new RoutedEventHandler(DelButtonText);

            StackPanel stackPanel = new StackPanel
            {
                Width = 180
            };

            RadioButton radioButtonFalse = new RadioButton
            {
                GroupName = string.Format("radioButtonsContent{0}", contentText),
                Name = string.Format("radioButtonContentFalse{0}", contentText),
                Content = "Default",
                IsChecked = true
            };

            RadioButton radioButtonTrue = new RadioButton
            {
                GroupName = string.Format("radioButtonsContent{0}", contentText),
                Name = string.Format("radioButtonContentTrue{0}", contentText)
            };

            WrapPanel wrapPanel = new WrapPanel
            {
                Orientation = Orientation.Horizontal
            };

            Label Label = new Label
            {
                Content = string.Format("Text{0}", contentText)
            };

            TextBox textBox = new TextBox
            {
                Name = string.Format("ContentTextBox{0}", contentText),
                Width = 120
            };

            if (read != false)
            {
                textBox.Text = value;
                radioButtonTrue.IsChecked = true;
                radioButtonFalse.IsChecked = false;
            }

            wrapPanel.Children.Add(Label);
            wrapPanel.Children.Add(textBox);

            radioButtonTrue.Content = wrapPanel;

            stackPanel.Children.Add(delButton);
            stackPanel.Children.Add(radioButtonFalse);
            stackPanel.Children.Add(radioButtonTrue);

            groupBox.Content = stackPanel;

            TextContentRight.Children.Add(groupBox);

            contentText++;
        }

        private void addButtonContent(StackPanel content, bool read, string iconXml, string sourceXml, string path1Xml, string path2Xml, EnnumListBox ennum, EnnumListContent ennumContent, EnnumListSize sizeButton, EnnumListTweet ennumListTweet, bool EventStartUp)
        {
            GroupBox groupBox = new GroupBox
            {
                Margin = new Thickness(10, 0, 0, 0),
                Header = string.Format("content Data{0}", buttonNumber),
                Background = new SolidColorBrush(Colors.White)
            };

            Label redSpark = new Label
            {
                Content = "*",
                Foreground = Brushes.Red
            };

            Label redSpark1 = new Label
            {
                Content = "*",
                Foreground = Brushes.Red
            };

            Label redSpark2 = new Label
            {
                Content = "*",
                Foreground = Brushes.Red
            };

            StackPanel stackPanelFalse = new StackPanel();

            Button delButton = new Button
            {
                Name = string.Format("DelButton{0}", buttonNumber),
                Height = 40,
                Width = 180,
                Content = "REMOVE CONTENT",
                BorderThickness = new Thickness(2, 2, 2, 2),
                BorderBrush = Brushes.Red
            };
            WrapPanel WrapButtonDel = new WrapPanel();
            Image ImabeDellButton = new Image
            {
                Source = new BitmapImage(new Uri(@"Images/Remove.png", UriKind.RelativeOrAbsolute)),
                HorizontalAlignment = HorizontalAlignment.Left
            };
            Label labelButtonDell = new Label
            {
                Content = "REMOVE CONTENT",
                HorizontalAlignment = HorizontalAlignment.Right
            };
            WrapButtonDel.Children.Add(ImabeDellButton);
            WrapButtonDel.Children.Add(labelButtonDell);
            delButton.Content = WrapButtonDel;
            delButton.Click += new RoutedEventHandler(RemoveButton);

            RadioButton radioButtonEventFirstTrue = new RadioButton
            {
                GroupName = string.Format("radioButtonEventFirst"),
                Name = string.Format("radioButtonEventFirstTrue{0}", buttonNumber),
                Content = "STARTUP EVENT"
            };

            WrapPanel wrapPanel = new WrapPanel
            {
                Orientation = Orientation.Vertical
            };

            WrapPanel wrapPanelradioButtonEventFirstTrue = new WrapPanel
            {
                Orientation = Orientation.Vertical
            };

            StackPanel StackPanelRadioButton = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            Label Label = new Label
            {
                Width = 85,
                Content = "EVENT"
            };

            ComboBox trueFalse = new ComboBox
            {
                Name = string.Format("trueFalse{0}", buttonNumber),
                Width = 120,
                Visibility = Visibility.Hidden
            };

            ComboBoxItem ComboBoxItemtrueFalse1 = new ComboBoxItem
            {
                Name = string.Format("hastag{0}", buttonNumber),
                Content = "hastag"
            };

            ComboBoxItem ComboBoxItemtrueFalse2 = new ComboBoxItem
            {
                Name = string.Format("tweets{0}", buttonNumber),
                Content = "tweets"
            };

            ComboBoxItem ComboBoxItemtrueFalse3 = new ComboBoxItem
            {
                Name = string.Format("tweets{0}", buttonNumber),
                Content = "",
                IsSelected = true
            };

            trueFalse.Items.Add(ComboBoxItemtrueFalse1);
            trueFalse.Items.Add(ComboBoxItemtrueFalse2);
            trueFalse.Items.Add(ComboBoxItemtrueFalse3);

            ComboBox comboBox = new ComboBox
            {
                Name = string.Format("ContentMetro{0}", buttonNumber),
                Width = 120,
            };

            comboBox.DropDownClosed += new EventHandler(ChangePath);

            ComboBoxItem ComboBoxItem1 = new ComboBoxItem
            {
                Content = "Movie",
                Name = string.Format("Movie{0}", buttonNumber),
                IsSelected = true
            };

            ComboBoxItem ComboBoxItem2 = new ComboBoxItem
            {
                Name = string.Format("ExeOpenWithParameter{0}", buttonNumber),
                Content = "ExeOpenWithParameter"
            };

            ComboBoxItem ComboBoxItem3 = new ComboBoxItem
            {
                Name = string.Format("Gallery{0}", buttonNumber),
                Content = "Gallery"
            };

            ComboBoxItem ComboBoxItem4 = new ComboBoxItem
            {
                Name = string.Format("InteractiveImages{0}", buttonNumber),
                Content = "InteractiveImages"
            };

            ComboBoxItem ComboBoxItem5 = new ComboBoxItem
            {
                Name = string.Format("OpenWWWPage{0}", buttonNumber),
                Content = "OpenWWWPage"
            };

            ComboBoxItem ComboBoxItem6 = new ComboBoxItem
            {
                Name = string.Format("ParseImagesFromFile{0}", buttonNumber),
                Content = "GalleryTwo"
            };

            ComboBoxItem ComboBoxItem7 = new ComboBoxItem
            {
                Name = string.Format("GameSnakePlay{0}", buttonNumber),
                Content = "GamePlay"
            };

            ComboBoxItem ComboBoxItem10 = new ComboBoxItem
            {
                Name = string.Format("Twiiter{0}", buttonNumber),
                Content = "Twiiter"
            };

            Image iconButton = new Image
            {
                Name = string.Format("iconButtonSurce{0}", buttonNumber),
                Margin = new Thickness(210, 30, 0, 0),
                Width = 60,
                Height = 60,
                VerticalAlignment = VerticalAlignment.Top
            };

            comboBox.Items.Add(ComboBoxItem1);
            comboBox.Items.Add(ComboBoxItem2);
            comboBox.Items.Add(ComboBoxItem3);
            comboBox.Items.Add(ComboBoxItem4);
            comboBox.Items.Add(ComboBoxItem5);
            comboBox.Items.Add(ComboBoxItem6);
            comboBox.Items.Add(ComboBoxItem7);
            comboBox.Items.Add(ComboBoxItem10);

            StackPanelRadioButton.Children.Add(Label);
            StackPanelRadioButton.Children.Add(redSpark);
            StackPanelRadioButton.Children.Add(comboBox);

            wrapPanel.Children.Add(StackPanelRadioButton);
            wrapPanelradioButtonEventFirstTrue.Children.Add(radioButtonEventFirstTrue);

            StackPanel StackPanelIcon = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            Label LabelIcon = new Label
            {
                Name = string.Format("LabelIcon{0}", buttonNumber),
                Width = 85,
                Content = "ICON"
            };

            TextBox TextBoxIcon = new TextBox
            {
                Name = string.Format("IconContent{0}", buttonNumber),
                Width = 100,
            };

            var nameTest = TextBoxIcon.Name;
            Image waterMark = new Image
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Source = new BitmapImage(new Uri(@"Images/file.png", UriKind.RelativeOrAbsolute))
            };

            Image waterMarkTwo = new Image
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Source = new BitmapImage(new Uri(@"Images/file.png", UriKind.RelativeOrAbsolute))
            };

            Button ButtonSelectIcon = new Button
            {
                Height = 20,
                Width = 20,
                Name = string.Format("BI{0}", buttonNumber),
                FontSize = 1
            };

            ButtonSelectIcon.Content = waterMarkTwo;
            ButtonSelectIcon.Click += new RoutedEventHandler(OpenFileDialog);
            StackPanelIcon.Children.Add(LabelIcon);
            StackPanelIcon.Children.Add(redSpark1);
            StackPanelIcon.Children.Add(TextBoxIcon);
            StackPanelIcon.Children.Add(ButtonSelectIcon);

            StackPanel StackPanelSource = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            Label LabelSource = new Label
            {
                Width = 100,
                Content = "SOURCE"
            };

            TextBox TextBoxSource = new TextBox
            {
                Name = string.Format("SourceContent{0}", buttonNumber),
                Width = 100,
            };

            nameTest = TextBoxSource.Name;
            Button ButtonSelectSourse = new Button
            {
                Height = 20,
                Width = 20,
                Name = string.Format("BS{0}", buttonNumber),
                FontSize = 1
            };

            ButtonSelectSourse.Content = waterMark;
            ButtonSelectSourse.Click += new RoutedEventHandler(OpenFileDialog);
            StackPanelSource.Children.Add(LabelSource);
            StackPanelSource.Children.Add(TextBoxSource);
            StackPanelSource.Children.Add(ButtonSelectSourse);

            StackPanel StackPanelSize = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            Label LabelSize = new Label
            {
                Width = 100,
                Content = "WINDOW SIZE:"
            };

            ComboBox comboBoxSize = new ComboBox
            {
                Name = string.Format("WindowSize{0}", buttonNumber),
                Width = 120,
            };
            comboBoxSize.DropDownClosed += new EventHandler(ChangePath);

            ComboBoxItem ComboBoxItemSize1 = new ComboBoxItem
            {
                Content = "1 : 1",
                IsSelected = true
            };

            ComboBoxItem ComboBoxItemSize2 = new ComboBoxItem
            {
                Content = "1 : 2"
            };

            ComboBoxItem ComboBoxItemSize3 = new ComboBoxItem
            {
                Content = "2 : 2"
            };

            ComboBoxItem ComboBoxItemSize4 = new ComboBoxItem
            {
                Content = "2 : 1"
            };

            comboBoxSize.Items.Add(ComboBoxItemSize1);
            comboBoxSize.Items.Add(ComboBoxItemSize2);
            comboBoxSize.Items.Add(ComboBoxItemSize3);
            comboBoxSize.Items.Add(ComboBoxItemSize4);

            StackPanelSize.Children.Add(LabelSize);
            StackPanelSize.Children.Add(comboBoxSize);

            StackPanel StackPanelContent = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            Label LabelCoordinates = new Label
            {
                Width = 140,
                Content = "DESTINATION CONTENT:"
            };

            ComboBox comboBoxCoordinates = new ComboBox
            {
                Name = string.Format("Coordinates{0}", buttonNumber),
                Width = 120,
            };

            ComboBoxItem ComboBoxItemCoordinate1 = new ComboBoxItem
            {
                Content = "Content Left",
                IsSelected = true
            };

            ComboBoxItem ComboBoxItemCoordinate2 = new ComboBoxItem
            {
                Content = "Content Main"
            };

            ComboBoxItem ComboBoxItemCoordinate3 = new ComboBoxItem
            {
                Content = "Content Footer"
            };

            comboBoxCoordinates.Items.Add(ComboBoxItemCoordinate1);
            comboBoxCoordinates.Items.Add(ComboBoxItemCoordinate2);
            comboBoxCoordinates.Items.Add(ComboBoxItemCoordinate3);

            StackPanelContent.Children.Add(LabelCoordinates);
            StackPanelContent.Children.Add(redSpark2);
            StackPanelContent.Children.Add(comboBoxCoordinates);

            StackPanel StackPanelPath1 = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            Label LabelPath1 = new Label
            {
                Name = string.Format("Path1N{0}", buttonNumber),
                Width = 100,
            };

            TextBox TextBoxPath1 = new TextBox
            {
                Name = string.Format("TextBoxPathMain{0}", buttonNumber),
                Width = 120,
                Visibility = Visibility.Hidden
            };

            StackPanelPath1.Children.Add(LabelPath1);
            StackPanelPath1.Children.Add(TextBoxPath1);

            StackPanel StackPanelPath2 = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            Label LabelPath2 = new Label
            {
                Name = string.Format("Path2N{0}", buttonNumber),
                Width = 100,
            };

            TextBox TextBoxPath2 = new TextBox
            {
                Name = string.Format("TextBoxPathTwo{0}", buttonNumber),
                Width = 120,
                Visibility = Visibility.Hidden
            };
            StackPanelPath2.Children.Add(LabelPath2);
            StackPanelPath2.Children.Add(TextBoxPath2);
            StackPanelPath2.Children.Add(trueFalse);
            stackPanelFalse.Children.Add(delButton);
            stackPanelFalse.Children.Add(wrapPanelradioButtonEventFirstTrue);
            stackPanelFalse.Children.Add(wrapPanel);
            stackPanelFalse.Children.Add(StackPanelIcon);
            stackPanelFalse.Children.Add(StackPanelSource);
            stackPanelFalse.Children.Add(StackPanelContent);
            stackPanelFalse.Children.Add(StackPanelSize);
            stackPanelFalse.Children.Add(StackPanelPath1);
            stackPanelFalse.Children.Add(StackPanelPath2);

            Grid ButtonConfigDelegade = new Grid();

            ButtonConfigDelegade.Children.Add(iconButton);
            ButtonConfigDelegade.Children.Add(stackPanelFalse);

            groupBox.Content = ButtonConfigDelegade;

            content.Children.Add(groupBox);

            TextBoxIcon.Text = iconXml;
            TextBoxSource.Text = sourceXml;
            TextBoxPath1.Text = path1Xml;
            TextBoxPath2.Text = path2Xml;

            switch (ennumContent)
            {
                case EnnumListContent.FContent:
                    ComboBoxItemCoordinate3.IsSelected = true;
                    groupBox.Name = string.Format("groupBoxContentFooter{0}", buttonNumber);
                    break;
                case EnnumListContent.MContent:
                    ComboBoxItemCoordinate2.IsSelected = true;
                    groupBox.Name = string.Format("groupBoxContentMain{0}", buttonNumber);
                    break;
                case EnnumListContent.LContent:
                    ComboBoxItemCoordinate1.IsSelected = true;
                    groupBox.Name = string.Format("groupBoxContentLeft{0}", buttonNumber);
                    break;
            }

            switch (sizeButton)
            {
                case EnnumListSize.Metro11:
                    ComboBoxItemSize1.IsSelected = true;
                    break;
                case EnnumListSize.Metro12:
                    ComboBoxItemSize2.IsSelected = true;
                    break;
                case EnnumListSize.Metro22:
                    ComboBoxItemSize3.IsSelected = true;
                    break;
                case EnnumListSize.Metro21:
                    ComboBoxItemSize4.IsSelected = true;
                    break;
            }

            switch (ennumListTweet)
            {
                case EnnumListTweet.TWEET:
                    ComboBoxItemtrueFalse2.IsSelected = true;
                    break;
                case EnnumListTweet.HASTAG:
                    ComboBoxItemtrueFalse1.IsSelected = true;
                    break;
                case EnnumListTweet.NULL:
                    ComboBoxItemtrueFalse3.IsSelected = true;
                    break;
            }

            if (read != false)
            {
                switch (EventStartUp)
                {
                    case true:
                        radioButtonEventFirstTrue.IsChecked = true;
                        ScreenEvent.IsChecked = false;
                        break;
                }
                try
                {
                    iconButton.Source = new BitmapImage(new Uri(iconXml, UriKind.RelativeOrAbsolute));
                }
                catch (Exception)
                { }
                
                switch (ennum)
                {
                    case EnnumListBox.ExeOpenWithParameter:
                        LabelPath1.Content = "EXECUTABLE PROGRAM";
                        LabelPath2.Content = "PARAMETERS";
                        ComboBoxItem2.IsSelected = true;
                        TextBoxPath1.Visibility = Visibility.Visible;
                        TextBoxPath2.Visibility = Visibility.Visible;
                        break;
                    case EnnumListBox.Gallery:
                        LabelPath1.Content = ".";
                        ComboBoxItem3.IsSelected = true;
                        TextBoxPath1.Visibility = Visibility.Visible;
                        break;
                    case EnnumListBox.GameSnakePlay:
                        LabelPath1.Content = "PREFIX NAME";
                        LabelPath2.Content = "FILE SOURCE";
                        ComboBoxItem7.IsSelected = true;
                        TextBoxPath1.Visibility = Visibility.Visible;
                        TextBoxPath2.Visibility = Visibility.Visible;
                        break;
                    case EnnumListBox.InteractiveImages:
                        ComboBoxItem4.IsSelected = true;
                        break;
                    case EnnumListBox.Movie:
                        ComboBoxItem1.IsSelected = true;
                        break;
                    case EnnumListBox.OpenWWWPage:
                        ComboBoxItem5.IsSelected = true;
                        break;
                    case EnnumListBox.ParseImagesFromFile:
                        LabelPath1.Content = "FILE SOURCE";
                        ComboBoxItem6.IsSelected = true;
                        TextBoxPath1.Visibility = Visibility.Visible;
                        break;
                    case EnnumListBox.Twiiter:
                        LabelPath1.Content = "NUMBER TWEETS";
                        LabelPath2.Content = "";
                        ComboBoxItem10.IsSelected = true;
                        TextBoxPath1.Visibility = Visibility.Visible;
                        TextBoxPath2.Width = 1;
                        trueFalse.Visibility = Visibility.Visible;
                        break;
                }
            }
            buttonNumber++;
        }


        private void ReloadConfig(object sender, RoutedEventArgs e)
        {
            LoadContent(MainContent);
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.OriginalSource;

            switch (button.Name)
            {
                case "Left":
                    addButtonContent(LeftContent, false, "", "", "", "", EnnumListBox.Null, EnnumListContent.LContent, EnnumListSize.Metro11, EnnumListTweet.NULL, false);
                    break;
                case "Main":
                    addButtonContent(MainContent, false, "", "", "", "", EnnumListBox.Null, EnnumListContent.MContent, EnnumListSize.Metro11, EnnumListTweet.NULL, false);
                    break;
                case "Footer":
                    addButtonContent(FooterContent, false, "", "", "", "", EnnumListBox.Null, EnnumListContent.FContent, EnnumListSize.Metro11, EnnumListTweet.NULL, false);
                    break;
                case "TextButtonContent":
                    addContentText(false, "");
                    addBindingText();
                    break;
            }
        }

        private void ChangePath(object sender, EventArgs e)
        {
            DependencyObject dependencyObject = DigitalSignageConfig as DependencyObject;

            for (int i = 0; i < buttonNumber; i++)
            {
                ComboBox comboBox = LogicalTreeHelper.FindLogicalNode(dependencyObject, "ContentMetro" + i) as ComboBox;
                if (comboBox != null)
                {
                    Label Path1N = LogicalTreeHelper.FindLogicalNode(dependencyObject, string.Format("Path1N{0}", i)) as Label;
                    Label Path2N = LogicalTreeHelper.FindLogicalNode(dependencyObject, string.Format("Path2N{0}", i)) as Label;

                    Image imageIcon = LogicalTreeHelper.FindLogicalNode(dependencyObject, string.Format("iconButtonSurce{0}", i)) as Image;

                    TextBox TextBoxPathMain = LogicalTreeHelper.FindLogicalNode(dependencyObject, string.Format("TextBoxPathMain{0}", i)) as TextBox;
                    TextBox TextBoxPathTwo = LogicalTreeHelper.FindLogicalNode(dependencyObject, string.Format("TextBoxPathTwo{0}", i)) as TextBox;

                    ComboBox trueFalseCombo = LogicalTreeHelper.FindLogicalNode(dependencyObject, string.Format("trueFalse{0}", i)) as ComboBox;
                    ComboBox WindowSize = LogicalTreeHelper.FindLogicalNode(dependencyObject, string.Format("WindowSize{0}", i)) as ComboBox;

                    switch (((ComboBoxItem)WindowSize.SelectedItem).Content.ToString())
                    {
                        case "1 : 1":
                            imageIcon.Height = 60;
                            imageIcon.Width = 60;
                            break;
                        case "1 : 2":
                            imageIcon.Height = 30;
                            imageIcon.Width = 60;
                            break;
                        case "2 : 2":
                            imageIcon.Height = 90;
                            imageIcon.Width = 90;
                            break;
                        case "2 : 1":
                            imageIcon.Height = 60;
                            imageIcon.Width = 90;
                            break;
                    }

                    switch (((ComboBoxItem)comboBox.SelectedItem).Content.ToString())
                    {
                        case "Movie":
                            Path1N.Content = "";
                            Path2N.Content = "";
                            TextBoxPathMain.Visibility = Visibility.Hidden;
                            TextBoxPathTwo.Visibility = Visibility.Hidden;
                            trueFalseCombo.Visibility = Visibility.Hidden;
                            TextBoxPathTwo.Width = 120;
                            break;
                        case "ExeOpenWithParameter":
                            Path1N.Content = "EXECUTABLE PROGRAM";
                            Path2N.Content = "PARAMETERS";
                            TextBoxPathMain.Visibility = Visibility.Visible;
                            TextBoxPathTwo.Visibility = Visibility.Visible;
                            trueFalseCombo.Visibility = Visibility.Hidden;
                            TextBoxPathTwo.Width = 120;
                            break;
                        case "Gallery":
                            Path1N.Content = "FILE SOURCE";
                            Path2N.Content = "";
                            TextBoxPathMain.Visibility = Visibility.Visible;
                            TextBoxPathTwo.Visibility = Visibility.Hidden;
                            trueFalseCombo.Visibility = Visibility.Hidden;
                            TextBoxPathTwo.Width = 120;
                            break;
                        case "InteractiveImages":
                            Path1N.Content = "";
                            Path2N.Content = "";
                            TextBoxPathMain.Visibility = Visibility.Hidden;
                            TextBoxPathTwo.Visibility = Visibility.Hidden;
                            trueFalseCombo.Visibility = Visibility.Hidden;
                            TextBoxPathTwo.Width = 120;
                            break;
                        case "OpenWWWPage":
                            Path1N.Content = "";
                            Path2N.Content = "";
                            TextBoxPathMain.Visibility = Visibility.Hidden;
                            TextBoxPathTwo.Visibility = Visibility.Hidden;
                            trueFalseCombo.Visibility = Visibility.Hidden;
                            TextBoxPathTwo.Width = 120;
                            break;
                        case "GalleryTwo":
                            Path1N.Content = "FILE SOURCE";
                            Path2N.Content = "";
                            TextBoxPathMain.Visibility = Visibility.Visible;
                            TextBoxPathTwo.Visibility = Visibility.Hidden;
                            trueFalseCombo.Visibility = Visibility.Hidden;
                            TextBoxPathTwo.Width = 120;
                            break;
                        case "GamePlay":
                            Path1N.Content = "PREFIX NAME";
                            Path2N.Content = "FILE SOURCE";
                            TextBoxPathMain.Visibility = Visibility.Visible;
                            TextBoxPathTwo.Visibility = Visibility.Visible;
                            trueFalseCombo.Visibility = Visibility.Hidden;
                            TextBoxPathTwo.Width = 120;
                            break;
                        case "Maze":
                            Path1N.Content = "EXECUTABLE PROGRAM";
                            Path2N.Content = "PARAMETERS";
                            TextBoxPathMain.Visibility = Visibility.Visible;
                            TextBoxPathTwo.Visibility = Visibility.Visible;
                            trueFalseCombo.Visibility = Visibility.Hidden;
                            TextBoxPathTwo.Width = 120;
                            break;
                        case "Twiiter":
                            Path1N.Content = "NUMBER TWEETS";
                            Path2N.Content = "";
                            TextBoxPathMain.Visibility = Visibility.Visible;
                            TextBoxPathTwo.Visibility = Visibility.Hidden;
                            TextBoxPathTwo.Width = 1;
                            trueFalseCombo.Visibility = Visibility.Visible;
                            break;
                    }
                }
            }
        }

        private void addBindingText()
        {
            DependencyObject dependencyObject = DigitalSignageConfig as DependencyObject;

            TextBox inputValue = LogicalTreeHelper.FindLogicalNode(dependencyObject, string.Format("ContentTextBox{0}", (contentText - 1))) as TextBox;
            RadioButton inputValueRadioButton = LogicalTreeHelper.FindLogicalNode(dependencyObject, string.Format("radioButtonContentTrue{0}", (contentText - 1))) as RadioButton;
            Binding binding = new Binding();
            binding.Source = inputValueRadioButton;
            binding.Path = new PropertyPath("IsChecked");
            inputValue.SetBinding(TextBox.IsEnabledProperty, binding);
        }

        private List<ButtonXml> AddButtonInList()
        {
            TextBox inputValue = new TextBox();
            ComboBox comboBox = new ComboBox();
            List<ButtonXml> buttons = new List<ButtonXml>();
            RadioButton radioButton = new RadioButton();

            for (int next = 0; next < buttonNumber; next++)
            {
                ButtonXml dynamicButton = new ButtonXml();
                dynamicButton.id = next;
                inputValue = LogicalTreeHelper.FindLogicalNode(DigitalSignageConfig, string.Format("IconContent{0}", next)) as TextBox;
                if (inputValue != null && inputValue.Text != "")
                {
                    dynamicButton.Icon = inputValue.Text;
                    comboBox = LogicalTreeHelper.FindLogicalNode(DigitalSignageConfig, string.Format("ContentMetro{0}", next)) as ComboBox;
                    if (comboBox != null)
                        dynamicButton.Event = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();
                    comboBox = LogicalTreeHelper.FindLogicalNode(DigitalSignageConfig, string.Format("Coordinates{0}", next)) as ComboBox;
                    if (comboBox != null)
                        dynamicButton.IdContent = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();
                    comboBox = LogicalTreeHelper.FindLogicalNode(DigitalSignageConfig, string.Format("WindowSize{0}", next)) as ComboBox;
                    if (comboBox != null)
                        dynamicButton.WindowSize = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();
                    inputValue = LogicalTreeHelper.FindLogicalNode(DigitalSignageConfig, string.Format("SourceContent{0}", next)) as TextBox;
                    if (inputValue != null)
                        dynamicButton.Source = inputValue.Text;
                    inputValue = LogicalTreeHelper.FindLogicalNode(DigitalSignageConfig, string.Format("TextBoxPathMain{0}", next)) as TextBox;
                    if (inputValue != null)
                        dynamicButton.Path1 = inputValue.Text;
                    inputValue = LogicalTreeHelper.FindLogicalNode(DigitalSignageConfig, string.Format("TextBoxPathTwo{0}", next)) as TextBox;
                    if (inputValue != null)
                        dynamicButton.Path2 = inputValue.Text;
                    radioButton = LogicalTreeHelper.FindLogicalNode(DigitalSignageConfig, string.Format("radioButtonEventFirstTrue{0}", next)) as RadioButton;
                    if (radioButton != null && radioButton.IsChecked == true)
                        dynamicButton.EventStartUp = true;
                    else
                        dynamicButton.EventStartUp = false;
                    comboBox = LogicalTreeHelper.FindLogicalNode(DigitalSignageConfig, string.Format("trueFalse{0}", next)) as ComboBox;
                    if (!((ComboBoxItem)comboBox.SelectedItem).Content.ToString().Equals(""))
                    {
                        if ((((ComboBoxItem)comboBox.SelectedItem).Content.ToString().Equals("hastag")))
                            dynamicButton.Path2 = "true";
                        else
                            dynamicButton.Path2 = "false";
                    }
                    buttons.Add(dynamicButton);
                }
            }
            return buttons;
        }

        private List<ContentTextXml> AddContentTextList()
        {
            List<ContentTextXml> contentTextList = new List<ContentTextXml>();
            TextBox inputValue = new TextBox();
            for (int next = 0; next < contentText; next++)
            {
                ContentTextXml dynamicContentText = new ContentTextXml();
                dynamicContentText.id = next;
                inputValue = LogicalTreeHelper.FindLogicalNode(DigitalSignageConfig, string.Format("ContentTextBox{0}", next)) as TextBox;
                if (inputValue != null && inputValue.Text != "")
                {
                    dynamicContentText.Text = inputValue.Text;
                    contentTextList.Add(dynamicContentText);
                }
            }
            return contentTextList;
        }

        private List<TutorialXml> AddTotorials()
        {
            List<TutorialXml> tutorialXmlList = new List<TutorialXml>();
            for (int next = 1; next < 7; next++)
            {
                TutorialXml tutorialXml = new TutorialXml();
                switch (next)
                {
                    case 1:
                        tutorialXml.id = "Gallery";
                        break;
                    case 2:
                        tutorialXml.id = "GamePlug";
                        break;
                    case 3:
                        tutorialXml.id = "InteractiveImages";
                        break;
                    case 4:
                        tutorialXml.id = "Movie";
                        break;
                    case 5:
                        tutorialXml.id = "OpenOutProgram";
                        break;
                    case 6:
                        tutorialXml.id = "OpenWeb";
                        break;
                }

                TextBox inputValue = LogicalTreeHelper.FindLogicalNode(DigitalSignageConfig, string.Format("Tutorial{0}", next)) as TextBox;
                tutorialXml.Source = inputValue.Text;

                RadioButton radioButton = LogicalTreeHelper.FindLogicalNode(DigitalSignageConfig, string.Format("RadioTrueTutorial{0}", next)) as RadioButton;
                if (radioButton.IsChecked == true)
                    tutorialXml.Type = true;
                else
                    tutorialXml.Type = false;

                ComboBox comboBox = LogicalTreeHelper.FindLogicalNode(DigitalSignageConfig, string.Format("TutorialTrue{0}", next)) as ComboBox;
                tutorialXml.Active = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();

                ComboBox comboBoxValue = LogicalTreeHelper.FindLogicalNode(DigitalSignageConfig, string.Format("TutorialVolue{0}", next)) as ComboBox;

                switch (((ComboBoxItem)comboBoxValue.SelectedItem).Content.ToString())
                {
                    case "0%":
                        tutorialXml.Volue = "0,0";
                        break;
                    case "10%":
                        tutorialXml.Volue = "0,1";
                        break;
                    case "20%":
                        tutorialXml.Volue = "0,2";
                        break;
                    case "30%":
                        tutorialXml.Volue = "0,3";
                        break;
                    case "40%":
                        tutorialXml.Volue = "0,4";
                        break;
                    case "50%":
                        tutorialXml.Volue = "0,5";
                        break;
                    case "60%":
                        tutorialXml.Volue = "0,6";
                        break;
                    case "70%":
                        tutorialXml.Volue = "0,7";
                        break;
                    case "80%":
                        tutorialXml.Volue = "0,8";
                        break;
                    case "90%":
                        tutorialXml.Volue = "0,9";
                        break;
                    case "100%":
                        tutorialXml.Volue = "1";
                        break;
                }

                tutorialXmlList.Add(tutorialXml);
            }
            return tutorialXmlList;
        }

        private string EnableDisable(RadioButton button)
        {
            if (button.IsChecked == true)
                return "true";
            else
                return "false";
        }

        private void SaveConfig(object sender, RoutedEventArgs e)
        {
            AddButtonInList();
            AddContentTextList();
            bool EventIsStartUp;

            if (ScreenEvent.IsChecked == true)
                EventIsStartUp = true;
            else
                EventIsStartUp = false;

            string Layout = string.Empty;
            if (((ComboBoxItem)HorizontalVertical.SelectedItem).Content.ToString().Equals("Content Main, Left, Footer"))
                Layout = "true";
            else
                Layout = "false";


            string HorizontalVerticalButtonSize = string.Empty;
            switch (((ComboBoxItem)HorizontalVerticalHW.SelectedItem).Content.ToString())
            {
                case "130 x 130":
                    HorizontalVerticalButtonSize = "130";
                    break;
                case "140 x 140":
                    HorizontalVerticalButtonSize = "140";
                    break;
                case "150 x 150":
                    HorizontalVerticalButtonSize = "150";
                    break;
                case "160 x 160":
                    HorizontalVerticalButtonSize = "160";
                    break;
                case "170 x 170":
                    HorizontalVerticalButtonSize = "170";
                    break;
                case "180 x 180":
                    HorizontalVerticalButtonSize = "180";
                    break;
                case "190 x 190":
                    HorizontalVerticalButtonSize = "190";
                    break;
                case "200 x 200":
                    HorizontalVerticalButtonSize = "200";
                    break;
                case "210 x 210":
                    HorizontalVerticalButtonSize = "210";
                    break;
                case "220 x 220":
                    HorizontalVerticalButtonSize = "220";
                    break;
                case "230 x 230":
                    HorizontalVerticalButtonSize = "230";
                    break;
                case "240 x 240":
                    HorizontalVerticalButtonSize = "240";
                    break;
                case "250 x 250":
                    HorizontalVerticalButtonSize = "250";
                    break;
                case "260 x 260":
                    HorizontalVerticalButtonSize = "260";
                    break;
                case "270 x 270":
                    HorizontalVerticalButtonSize = "270";
                    break;
                case "280 x 280":
                    HorizontalVerticalButtonSize = "280";
                    break;
                case "290 x 290":
                    HorizontalVerticalButtonSize = "290";
                    break;
                case "300 x 300":
                    HorizontalVerticalButtonSize = "300";
                    break;
            }

            XDocument srcTree = new XDocument(
            new XDeclaration("1.0", "utf8", "yes"),
            new XComment("v.1"),
            new XElement("Config",
                new XElement("Buttons",
                    from button in AddButtonInList()
                    select new XElement("Button",
                           new XAttribute("id", button.id),
                           new XAttribute("Icon", button.Icon),
                           new XAttribute("Event", button.Event),
                           new XAttribute("IdContent", button.IdContent),
                           new XAttribute("Source", button.Source),
                           new XAttribute("Path1", button.Path1),
                           new XAttribute("Path2", button.Path2),
                           new XAttribute("EventStartUp", button.EventStartUp),
                           new XAttribute("WindowSize", button.WindowSize))
               ),
                    new XElement("BackGrounds",
                    new XElement("BackGround", new XAttribute("id", "BackGround"), new XAttribute("Source", BackGround.Text)),
                    new XElement("BackGround", new XAttribute("id", "Gallery"), new XAttribute("Source", BackGroundGallery.Text)),
                    new XElement("BackGround", new XAttribute("id", "InteractiveImages"), new XAttribute("Source", BackGroundInteractiveImages.Text)),
                    new XElement("BackGround", new XAttribute("id", "OpenWWWPage"), new XAttribute("Source", BackGroundOpenWWWPage.Text)),
                    new XElement("BackGround", new XAttribute("id", "Game"), new XAttribute("Source", BackGroundGame.Text)),
                    new XElement("BackGround", new XAttribute("id", "BackGroundMain"), new XAttribute("Source", BackGroundMain.Text)),
                    new XElement("BackGround", new XAttribute("id", "BackGroundRingAnimation"), new XAttribute("Source", BackGroundRingAnimation.Text)),
                    new XElement("BackGround", new XAttribute("id", "ShutDown"), new XAttribute("Source", ShutDown.Text)),
                    new XElement("BackGround", new XAttribute("id", "Movie"), new XAttribute("Source", BackGroundMovie.Text))
                    ),
                    new XElement("ContentTexts",
                    from content in AddContentTextList()
                    select new XElement("ContentText",
                           new XAttribute("id", content.id),
                           new XAttribute("Text", content.Text))
                    ),
                    new XElement("Tutorials",
                    from tutorial in AddTotorials()
                    select new XElement("Tutorial",
                           new XAttribute("id", tutorial.id),
                           new XAttribute("Source", tutorial.Source),
                           new XAttribute("Volue", tutorial.Volue),
                           new XAttribute("Type", tutorial.Type),
                           new XAttribute("Active", tutorial.Active))
                    ),
                    new XElement("WaterMarks",
                    new XElement("WaterMark", new XAttribute("id", "WaterMarkMovie"), new XAttribute("Source", WaterMarkMovieSource.Text)),
                    new XElement("WaterMark", new XAttribute("id", "WaterMarkOutSideProgram"), new XAttribute("Source", WaterMarkOutSideProgramSource.Text)),
                    new XElement("WaterMark", new XAttribute("id", "WaterMarkGallery"), new XAttribute("Source", WaterMarkGallerySource.Text)),
                    new XElement("WaterMark", new XAttribute("id", "WaterMarkInteractiveImages"), new XAttribute("Source", WaterMarkInteractiveImagesSource.Text)),
                    new XElement("WaterMark", new XAttribute("id", "WaterMarkOpenWeb"), new XAttribute("Source", WaterMarkOpenWebSource.Text)),
                    new XElement("WaterMark", new XAttribute("id", "WaterMarkGalleryParseFromFile"), new XAttribute("Source", WaterMarkGalleryParseFromFileSource.Text)),
                    new XElement("WaterMark", new XAttribute("id", "WaterMarkExitMark"), new XAttribute("Source", WaterMarkExitMarkSource.Text)),
                    new XElement("WaterMark", new XAttribute("id", "WaterMarkNextMark"), new XAttribute("Source", WaterMarkNextMarkSource.Text)),
                    new XElement("WaterMark", new XAttribute("id", "WaterMarkPrevMark"), new XAttribute("Source", WaterMarkPrevMarkSource.Text))
                    ),
                    new XElement("Logos",
                    new XElement("Logo", new XAttribute("id", "LogoLeft"), new XAttribute("Source", LogoLeft.Text)),
                    new XElement("Logo", new XAttribute("id", "MainLogo"), new XAttribute("Source", MainLogo.Text)),
                    new XElement("Logo", new XAttribute("id", "OffLogo"), new XAttribute("Source", OffLogo.Text))
                    ),
                    new XElement("Gestures",
                    new XElement("Gesture", new XAttribute("id", "VOLUME/ZOOM"), new XAttribute("enable", EnableDisable(gesture1))),
                    new XElement("Gesture", new XAttribute("id", "EXIT"), new XAttribute("enable", EnableDisable(gesture2))),
                    new XElement("Gesture", new XAttribute("id", "SYMULATE MOUSE CLIK"), new XAttribute("enable", EnableDisable(gesture3))),
                    new XElement("Gesture", new XAttribute("id", "DOWN"), new XAttribute("enable", EnableDisable(gesture4))),
                    new XElement("Gesture", new XAttribute("id", "LEFT"), new XAttribute("enable", EnableDisable(gesture5))),
                    new XElement("Gesture", new XAttribute("id", "RIGHT"), new XAttribute("enable", EnableDisable(gesture6))),
                    new XElement("Gesture", new XAttribute("id", "UP"), new XAttribute("enable", EnableDisable(gesture7))),
                    new XElement("Gesture", new XAttribute("id", "MINIMALIZE"), new XAttribute("enable", EnableDisable(gesture8))),
                    new XElement("Gesture", new XAttribute("id", "MAXYMALIZE"), new XAttribute("enable", EnableDisable(gesture9))),
                    new XElement("Gesture", new XAttribute("id", "OPEN WEB"), new XAttribute("enable", EnableDisable(gesture10))),
                    new XElement("Gesture", new XAttribute("id", "NEXT"), new XAttribute("enable", EnableDisable(gesture11))),
                    new XElement("Gesture", new XAttribute("id", "PRIEV"), new XAttribute("enable", EnableDisable(gesture12))),
                    new XElement("Gesture", new XAttribute("id", "POSSIBLE"), new XAttribute("enable", EnableDisable(gesture13)))
                    ),
                    new XElement("AccessTokenTwiits",
                    new XElement("AccessTokenTwiit", new XAttribute("OAuthConsumerKey", OAuthConsumerKey.Text), new XAttribute("OAuthConsumerSecret", OAuthConsumerSecret.Text))
                    ),
                    new XElement("Movies",
                    new XElement("Movie", new XAttribute("id", "MovieOne"), new XAttribute("Icon", ""), new XAttribute("Source", MovieOne.Text)),
                    new XElement("Movie", new XAttribute("id", "MovieMain"), new XAttribute("Icon", ""), new XAttribute("Source", MovieMain.Text))
                    ),
                    new XElement("Layouts",
                    new XElement("Layout", new XAttribute("WidthButton", HorizontalVerticalButtonSize), new XAttribute("HeightButton", HorizontalVerticalButtonSize),
                        new XAttribute("HorizontalVertical", Layout), new XAttribute("Animation", ((ComboBoxItem)AnimationBackGround.SelectedItem).Content.ToString()),
                        new XAttribute("Opacity", ((ComboBoxItem)Opacity.SelectedItem).Content.ToString()), new XAttribute("BorderBrush", BorderBrush.Text),
                        new XAttribute("TimeWatingScreenSaver", ((ComboBoxItem)timeWatingScreenSaver.SelectedItem).Content.ToString()), new XAttribute("ScreenEvent", EventIsStartUp),
                        new XAttribute("SpeedGallery", ((ComboBoxItem)speedGallery.SelectedItem).Content.ToString()),
                        new XAttribute("HorizontalAlignment", ((ComboBoxItem)MetroContent.SelectedItem).Content.ToString()))
                    )
        )
        );
            if (destination.Text != "" && RadioDestinationDefault.IsChecked == false)
            {
                try
                {
                    srcTree.Save(destination.Text);
                }
                catch (UnauthorizedAccessException) { MessageBox.Show("odmawia dostępu z powodu błędu We/Wy lub określonego typu Błąd zabezpieczeń."); }
            }
            else
                srcTree.Save(@"../../../../Config.xml");

            WriterlnRead.Text = "Saving successful";
        }

        private void ClearAllContent(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.OriginalSource;

            switch (button.Name)
            {
                case "TextButtonContentClearLeft":
                    LeftContent.Children.RemoveRange(26, LeftContent.Children.Count);
                    break;
                case "TextButtonContentClearMain":
                    MainContent.Children.RemoveRange(1, MainContent.Children.Count);
                    break;
                case "TextButtonContentClearFooter":
                    FooterContent.Children.RemoveRange(1, FooterContent.Children.Count);
                    break;
                case "TextButtonContentClearText":
                    TextContentRight.Children.RemoveRange(1, TextContentRight.Children.Count);
                    break;
            }
        }

        private void RemoveButton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.OriginalSource;
            string nameButton = button.Name;
            string prefix = nameButton.Remove(0, 9);

            DependencyObject dependencyObject = DigitalSignageConfig as DependencyObject;
            GroupBox groupBox = new GroupBox();

            groupBox = LogicalTreeHelper.FindLogicalNode(dependencyObject, string.Format("groupBoxContentMain{0}", prefix)) as GroupBox;
            if (groupBox != null)
            {
                MainContent.Children.Remove(groupBox);
            }

            groupBox = LogicalTreeHelper.FindLogicalNode(dependencyObject, string.Format("groupBoxContentFooter{0}", prefix)) as GroupBox;
            if (groupBox != null)
            {
                FooterContent.Children.Remove(groupBox);
            }

            groupBox = LogicalTreeHelper.FindLogicalNode(dependencyObject, string.Format("groupBoxContentLeft{0}", prefix)) as GroupBox;
            if (groupBox != null)
            {
                LeftContent.Children.Remove(groupBox);
            }
        }

        private void SwitchLayoutType(object sender, EventArgs e)
        {
            switch (((ComboBoxItem)HorizontalVertical.SelectedItem).Content.ToString())
            {
                case "Content Main, Left, Footer":
                    //HorizontalVerticalHW.Visibility = Visibility.Hidden;
                    //WindowSize.Visibility = Visibility.Hidden;
                    break;
                case "METRO":
                    //HorizontalVerticalHW.Visibility = Visibility.Visible;
                    //WindowSize.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void ClearInputMovie(object sender, EventArgs e)
        {
            switch (((ComboBoxItem)AnimationBackGround.SelectedItem).Content.ToString())
            {
                case "true":
                    MovieMainButton.IsEnabled = true;
                    BackGroundRingButton.IsEnabled = true;
                    BackGroundRingAnimation.IsEnabled = true;
                    MovieMain.IsEnabled = true;
                    break;
                case "false":
                    MovieMainButton.IsEnabled = false;
                    BackGroundRingButton.IsEnabled = false;
                    BackGroundRingAnimation.IsEnabled = false;
                    MovieMain.IsEnabled = false;
                    MovieMain.Text = "";
                    BackGroundRingAnimation.Text = "";
                    BackGroundRingButton.IsEnabled = false;
                    TextContentRight.Children.RemoveRange(1, TextContentRight.Children.Count);
                    break;
            }
        }

        private void DelButtonText(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.OriginalSource;
            string nameButton = button.Name;
            string prefix = nameButton.Remove(0, 13);

            DependencyObject dependencyObject = DigitalSignageConfig as DependencyObject;
            GroupBox groupBox = new GroupBox();

            groupBox = LogicalTreeHelper.FindLogicalNode(dependencyObject, string.Format("groupBoxTextContent{0}", prefix)) as GroupBox;
            if (groupBox != null)
            {
                TextContentRight.Children.Remove(groupBox);
            }
        }

        private void SaveFileDialog(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = XmlFiltr;
            saveFileDialog.FileName = "Config";
            saveFileDialog.DefaultExt = ".xml";
            if (saveFileDialog.ShowDialog() == true)
                destination.Text = saveFileDialog.FileName;
        }

        private void OpenFileDialog(object sender, RoutedEventArgs e)
        {
            openFileDialog.Filter = imagrFiltr;
            Button button = (Button)e.OriginalSource;
            string selectEvent = button.Name;
            string prefix = selectEvent.Remove(0, 2);

            if (selectEvent.Equals("configReadFromHDButton"))
            {
                openFileDialog.Filter = XmlFiltr;
            }

            if (openFileDialog.ShowDialog() == true)
            {
                if (selectEvent.Equals(string.Format("BS{0}", prefix)))
                    SetContent(DigitalSignageConfig, openFileDialog.FileName, string.Format("SourceContent{0}", prefix));
                else if (selectEvent.Equals(string.Format("BI{0}", prefix)))
                {
                    SetContent(DigitalSignageConfig, openFileDialog.FileName, string.Format("IconContent{0}", prefix));
                    SetImageButton(DigitalSignageConfig, openFileDialog.FileName, string.Format("iconButtonSurce{0}", prefix));
                }
                else
                {
                    switch (selectEvent)
                    {
                        case "MovieMainButton":
                            MovieMain.Text = openFileDialog.FileName;
                            break;
                        case "BackGroundButton":
                            BackGround.Text = openFileDialog.FileName;
                            break;
                        case "MovieOneButton":
                            MovieOne.Text = openFileDialog.FileName;
                            break;
                        case "configReadFromHDButton":
                            configReadFromHD.Text = openFileDialog.FileName;
                            break;
                        case "MainLogoButton":
                            MainLogo.Text = openFileDialog.FileName;
                            break;
                        case "OffLogoButton":
                            OffLogo.Text = openFileDialog.FileName;
                            break;
                        case "LogoLeftButton":
                            LogoLeft.Text = openFileDialog.FileName;
                            break;
                        case "BackGroundMovieButton":
                            BackGroundMovie.Text = openFileDialog.FileName;
                            break;
                        case "BackGroundGameButton":
                            BackGroundGame.Text = openFileDialog.FileName;
                            break;
                        case "BackGroundOpenWWWPageButton":
                            BackGroundOpenWWWPage.Text = openFileDialog.FileName;
                            break;
                        case "BackGroundInteractiveImagesButton":
                            BackGroundInteractiveImages.Text = openFileDialog.FileName;
                            break;
                        case "BackGroundGalleryButton":
                            BackGroundGallery.Text = openFileDialog.FileName;
                            break;
                        case "BackGroundRingButton":
                            BackGroundRingAnimation.Text = openFileDialog.FileName;
                            break;
                        case "BackGroundMMainButton":
                            BackGroundMain.Text = openFileDialog.FileName;
                            break;
                        case "ShutDownButton":
                            ShutDown.Text = openFileDialog.FileName;
                            break;
                        case "WaterMarkMovie":
                            WaterMarkMovieSource.Text = openFileDialog.FileName;
                            break;
                        case "WaterMarkOutSideProgram":
                            WaterMarkOutSideProgramSource.Text = openFileDialog.FileName;
                            break;
                        case "WaterMarkGallery":
                            WaterMarkGallerySource.Text = openFileDialog.FileName;
                            break;
                        case "WaterMarkInteractiveImages":
                            WaterMarkInteractiveImagesSource.Text = openFileDialog.FileName;
                            break;
                        case "WaterMarkOpenWeb":
                            WaterMarkOpenWebSource.Text = openFileDialog.FileName;
                            break;
                        case "WaterMarkGalleryParseFromFile":
                            WaterMarkGalleryParseFromFileSource.Text = openFileDialog.FileName;
                            break;
                        case "WaterMarkExitMark":
                            WaterMarkExitMarkSource.Text = openFileDialog.FileName;
                            break;
                        case "WaterMarkNextMark":
                            WaterMarkNextMarkSource.Text = openFileDialog.FileName;
                            break;
                        case "WaterMarkPrevMark":
                            WaterMarkPrevMarkSource.Text = openFileDialog.FileName;
                            break;
                        case "ButtonTutorial1":
                            Tutorial1.Text = openFileDialog.FileName;
                            break;
                        case "ButtonTutorial2":
                            Tutorial2.Text = openFileDialog.FileName;
                            break;
                        case "ButtonTutorial3":
                            Tutorial3.Text = openFileDialog.FileName;
                            break;
                        case "ButtonTutorial4":
                            Tutorial4.Text = openFileDialog.FileName;
                            break;
                        case "ButtonTutorial5":
                            Tutorial5.Text = openFileDialog.FileName;
                            break;
                        case "ButtonTutorial6":
                            Tutorial6.Text = openFileDialog.FileName;
                            break;
                    }
                }
            }
        }

        public string getSourceConfig()
        {
            string SourceConfig = string.Empty;
            if (configReadFromHD.Text != "" && configReadFromHDDefault.IsChecked == false)
                SourceConfig = configReadFromHD.Text;
            else
                SourceConfig = @"../../../../Config.xml";

            return SourceConfig;
        }

        private void ReadAllConfig(object sender, RoutedEventArgs e)
        {
            string SourceConfig = getSourceConfig();
            writeln = string.Empty;
            try
            {
                string line = string.Empty;
                using (StreamReader sr = new StreamReader(SourceConfig))
                {
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        writeln += " \n" + line;
                        line = sr.ReadLine();
                    }
                    WriterlnRead.Text = writeln;
                    writeln = string.Empty;
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                WriterlnRead.Text = string.Format("The file could not be read : ", ex.Message);
            }
        }
    }
}

