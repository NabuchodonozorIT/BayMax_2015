using iTwitter;
using Microsoft.Kinect.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfScreenHelper;

namespace DigitalSignageBaymax
{
    public class WindowScaleXY
    {
        public double SizeX { get; set; }
        public double SizeY { get; set; }

        public WindowScaleXY(double SizeX, double SizeY)
        {
            this.SizeX = SizeX;
            this.SizeY = SizeY;
        }
    }

    public class MainContent
    {
        ReadConfig readConfig = new ReadConfig();
        TweetContent tweetContent = new TweetContent();
        Tweeter twitterSetGet = new Tweeter();
        TweetContent tweetContentEvent = new TweetContent();
        private bool TweetSedInContend = false;
        private double HeighWindow = Screen.PrimaryScreen.Bounds.Height;

        public void AnimationChack(Layout objectLayout, List<Movie> MovieCopy, Image Animation, MediaElement MovieOne, MediaElement MovieMain)
        {
            MovieOne.Visibility = Visibility.Visible;
            if (objectLayout.Animation == true)
            {
                foreach (Movie movie in MovieCopy)
                {
                    switch (movie.id)
                    {
                        case "MovieMain":
                            if (!movie.Source.Equals(""))
                            {
                                Animation.Visibility = Visibility.Visible;
                                MovieMain.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                MessageBox.Show("You must first set the source for the movie");
                            }
                            break;
                    }
                }
            }
        }

        public WindowScaleXY GetScaleButtonX(Button button)
        {
            switch (button.WindowSize)
            {
                case "1 : 1":
                    return new WindowScaleXY(1, 1);
                case "1 : 2":
                    return new WindowScaleXY(1, 2);
                case "2 : 2":
                    return new WindowScaleXY(2, 2);
                case "2 : 1":
                    return new WindowScaleXY(2, 1);
            }
            return null;
        }

        public void SetWaterMarkInButton(Image image, string NameWaterMark)
        {
            foreach (WaterMark waterMarkIcon in MainWindow.WaterMarkList)
            {
                try
                {
                    if (waterMarkIcon.id.Equals(NameWaterMark))
                    {
                        image.Source = new BitmapImage(new Uri(waterMarkIcon.Source, UriKind.RelativeOrAbsolute));
                        break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("chack the source, ByMax cant found the file {0}", ex.Message));
                }
            }
        }

        public bool AddButtonInMainContent(Grid LeftContent, KinectScrollViewer footerScroll, KinectScrollViewer scrollViewerAddons, KinectScrollViewer scrollViewer,
            Grid MainGridWindowAplication, MediaElement MovieMain, MediaElement MovieOne,
            WrapPanel MetroContent, Image Animation, Grid MetroStyle, WrapPanel MainCOntent, WrapPanel LeftContetn,
            WrapPanel FooterContent, Action<object, RoutedEventArgs> Movie, Action<object, RoutedEventArgs> ExeOpenWithParameter, Action<object,
            RoutedEventArgs> Gallery, Action<object, RoutedEventArgs> InteractiveImages, Action<object, RoutedEventArgs> OpenWWWPage, Action<object,
            RoutedEventArgs> GalleryTwo, Action<object, RoutedEventArgs> GamePlug)
        {
            Layout horizontalVertical = readConfig.GetLayoutConfig();
            string colour = horizontalVertical.BorderBrush;
            Color bordelColor = Color.FromRgb(Convert.ToByte("cc", 16), Convert.ToByte("4d", 16), Convert.ToByte("c6", 16));
            try
            {
                bordelColor = Color.FromRgb(Convert.ToByte(colour.Substring(1, 2), 16), Convert.ToByte(colour.Substring(3, 2), 16), Convert.ToByte(colour.Substring(5, 2), 16));
            }
            catch (FormatException)
            {
                MessageBox.Show("Check the borderBrush color, if you dont know what is Hexa Color, chack this page http://www.color-hex.com/");
            }

            AnimationChack(horizontalVertical, MainWindow.movieCopy, Animation, MovieOne, MovieMain);
            double sizeButton = ((HeighWindow * ((double)horizontalVertical.HeightButton)) / 1000);

            scrollViewerAddons.Width = sizeButton;
            scrollViewer.Width = footerScroll.Width = sizeButton * 1.5;
            LeftContent.Width = sizeButton * 1.5 + 60;

            foreach (Button buttonConfig in MainWindow.buttonsCopy)
            {
                if (!buttonConfig.Icon.Equals("") && !buttonConfig.Event.Equals(""))
                {
                    KinectTileButton button = new KinectTileButton
                    {
                        Name = string.Format("ButtonId{0}", buttonConfig.id),
                        Margin = new Thickness(5, 0, 0, 5),
                        BorderBrush = new SolidColorBrush(bordelColor),
                        BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5),
                        Opacity = horizontalVertical.Opacity
                    };

                    Image waterMark = new Image()
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 80,
                        Width = 80,
                        Opacity = 0.5
                    };

                    switch (buttonConfig.Event)
                    {
                        case "Movie":
                            button.Click += new RoutedEventHandler(Movie);
                            SetWaterMarkInButton(waterMark, "WaterMarkMovie");
                            break;
                        case "ExeOpenWithParameter":
                            button.Click += new RoutedEventHandler(ExeOpenWithParameter);
                            SetWaterMarkInButton(waterMark, "WaterMarkOutSideProgram");
                            break;
                        case "Gallery":
                            button.Click += new RoutedEventHandler(Gallery);
                            SetWaterMarkInButton(waterMark, "WaterMarkGallery");
                            break;
                        case "InteractiveImages":
                            button.Click += new RoutedEventHandler(InteractiveImages);
                            SetWaterMarkInButton(waterMark, "WaterMarkInteractiveImages");
                            break;
                        case "OpenWWWPage":
                            button.Click += new RoutedEventHandler(OpenWWWPage);
                            SetWaterMarkInButton(waterMark, "WaterMarkOpenWeb");
                            break;
                        case "GalleryTwo":
                            button.Click += new RoutedEventHandler(GalleryTwo);
                            SetWaterMarkInButton(waterMark, "WaterMarkGalleryParseFromFile");
                            break;
                        case "GamePlay":
                            button.Click += new RoutedEventHandler(GamePlug);
                            break;
                        case "Twiiter":
                            TweetSedInContend = true;
                            break;
                    }

                    if (TweetSedInContend == true)
                    {
                        WindowScaleXY xy = GetScaleButtonX(buttonConfig);
                        button.Click += new RoutedEventHandler(OpenWWWPage);
                        List<TweetsUser> ListTweets = new List<TweetsUser>();
                        twitterSetGet.SourceConfig = MainWindow.xmlUrl;
                        ListTweets = twitterSetGet.GetTwitts(buttonConfig.Source, Convert.ToInt32(buttonConfig.Path1), Convert.ToBoolean(buttonConfig.Path2));
                        tweetContentEvent.SetContentTwiitInButton(button, MainGridWindowAplication, MetroContent, MainCOntent, LeftContetn,
                        FooterContent, buttonConfig, ListTweets, sizeButton, xy);
                    }

                    else
                    {
                        Grid gridImage = new Grid();

                        Grid grid = new Grid
                        {
                            Margin = new Thickness(10, 15, 10, 5)
                        };

                        Image image = new Image();
                        try
                        {
                            image.Source = new BitmapImage(new Uri(buttonConfig.Icon, UriKind.RelativeOrAbsolute));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(string.Format("chack the source, ByMax cant found the file {0}", ex.Message));
                        }

                        gridImage.Children.Add(image);
                        gridImage.Children.Add(waterMark);
                        button.Content = gridImage;
                        grid.Children.Add(button);

                        switch (horizontalVertical.HorizontalVertical)
                        {
                            case true:
                                switch (buttonConfig.IdContent)
                                {
                                    case "Content Main":
                                        SetWidthHeightButton(button, image, (sizeButton * 1.5));
                                        MainCOntent.Children.Add(grid);
                                        break;
                                    case "Content Left":
                                        SetWidthHeightButton(button, image, 170);
                                        LeftContetn.Children.Add(grid);
                                        break;
                                    case "Content Footer":
                                        SetWidthHeightButton(button, image, (sizeButton * 1.5));
                                        FooterContent.Children.Add(grid);
                                        break;
                                }
                                break;

                            case false:
                                WindowScaleXY xy = GetScaleButtonX(buttonConfig);
                                button.Height = image.Height = sizeButton * xy.SizeY;
                                button.Width = image.Width = sizeButton * xy.SizeX;
                                waterMark.Height = horizontalVertical.HeightButton;
                                waterMark.Width = horizontalVertical.WidthButton;
                                grid.Margin = new Thickness(0, 25, 0, -30);

                                MetroContent.Children.Add(grid);
                                MetroStyle.Visibility = Visibility.Visible;
                                break;
                        }
                    }
                    TweetSedInContend = false;
                }
            }
            tweetContentEvent.InitTimerGetRandomTweet();
            return horizontalVertical.HorizontalVertical;
        }

        public void SetWidthHeightButton(KinectTileButton button, Image image, double value)
        {
            button.Height = image.Height = value;
            button.Width = image.Width = value;
        }

    }
}
