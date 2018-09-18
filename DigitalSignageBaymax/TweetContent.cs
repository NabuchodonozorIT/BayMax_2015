using iTwitter;
using Microsoft.Kinect.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;


namespace DigitalSignageBaymax
{
    public class TweetContentClass
    {
        public int id { get; set; }
        public string textTime { get; set; }
        public string textTweet { get; set; }
        public int countNumber { get; set; }
        public List<TweetsUser> tweetlist { get; set; }

    }
    public class TweetContent
    {
        private DispatcherTimer timer;
        private int speed = 250;
        private int countTweet = 0;
        private List<TweetContentClass> tweetContentList = new List<TweetContentClass>();
        ReadConfig readConfig = new ReadConfig();
        private int randomTweets;
        Random rnd = new Random();
        public Grid MainGrid;

        public Layout HorizontalVertical()
        {
            return readConfig.GetLayoutConfig();
        }

        public TweetContentClass SetContentTweets(KinectTileButton button, WrapPanel MetroContent,
             WrapPanel MainContent, WrapPanel LeftContent, WrapPanel FooterContent, Button buttonConfig, List<TweetsUser> ListTweet,
            double sizeButton, WindowScaleXY xy)
        {
            TweetContentClass tweetContentClass = new TweetContentClass();
            List<Tweets> tweets = new List<Tweets>();
            Layout objectLayout = HorizontalVertical();

            Grid gridImage = new Grid();

            Grid grid = new Grid
            {
                Background = new SolidColorBrush(Colors.Black),
                Margin = new Thickness(0, 0, 0, 0),
                Opacity = 0.4
            };

            Image image = new Image
            {
                Source = new BitmapImage(new Uri(buttonConfig.Icon, UriKind.RelativeOrAbsolute)),
                Width = sizeButton / 2,
                Margin = new Thickness(-15, -15, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };

            ScrollViewer scrollViewer = new ScrollViewer
            {
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
            };

            TextBlock textBlockTime = new TextBlock
            {
                Name = string.Format("TimeTweets{0}", countTweet),
                Foreground = new SolidColorBrush(Colors.Orange),
                Margin = new Thickness((double)sizeButton / 3, 0, 0, 180),
                FontSize = sizeButton / 10
            };

            TextBlock tweetsText = new TextBlock
            {
                Name = string.Format("Tweets{0}", countTweet),
                Foreground = new SolidColorBrush(Colors.White),
                Margin = new Thickness(0, sizeButton / 3.5, 0, 0),
                TextWrapping = TextWrapping.Wrap,
                FontSize = sizeButton / 10
            };

            tweetContentClass.id = countTweet;
            tweetContentClass.textTweet = tweetsText.Name;
            tweetContentClass.textTime = textBlockTime.Name;
            tweetContentClass.countNumber = ListTweet.Count;
            tweetContentClass.tweetlist = ListTweet;

            countTweet++;

            Label label = new Label();

            label.Content = tweetsText;
            gridImage.Children.Add(image);
            gridImage.Children.Add(textBlockTime);
            gridImage.Children.Add(label);
            scrollViewer.Content = gridImage;
            grid.Children.Add(scrollViewer);

            grid.Background = new SolidColorBrush(Colors.Black);
            grid.Margin = new Thickness(0, 0, 0, 0);
            grid.Opacity = 0.8;
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Center;
            textBlockTime.FontSize = 24;
            button.Content = grid;


            switch (objectLayout.HorizontalVertical)
            {
                case true:
                    switch (buttonConfig.IdContent)
                    {
                        case "Content Main":
                            button.Height = button.Width = sizeButton * 1.5;
                            button.BorderThickness = new Thickness(0, 0, 0, 0);
                            MainContent.Children.Add(button);
                            break;
                        case "Content Left":
                            button.Height = button.Width = 170;
                            button.BorderThickness = new Thickness(0, 0, 0, 0);
                            LeftContent.Children.Add(button);
                            break;
                        case "Content Footer":
                            button.Height = button.Width = sizeButton * 1.5;
                            button.BorderThickness = new Thickness(0, 0, 0, 0);
                            FooterContent.Children.Add(button);
                            break;
                    }
                    break;

                case false:
                    button.Height = sizeButton * xy.SizeY;
                    button.Width = sizeButton * xy.SizeX;
                    MetroContent.Children.Add(button);
                    button.Margin = new Thickness(4, 24, 0, -25);
                    break;
            }

            grid.Width = button.Width;
            grid.Height = button.Height;

            return tweetContentClass;
        }

        public void SetContentTwiitInButton(KinectTileButton buttonTwet, Grid MainGridWindowAplication, WrapPanel MetroContent, WrapPanel MainContent,
            WrapPanel LeftContent, WrapPanel FooterContent, Button buttonConfig, List<TweetsUser> ListTweets, double sizeButton, WindowScaleXY xy)
        {
            tweetContentList.Add(SetContentTweets(buttonTwet, MetroContent, MainContent, LeftContent, FooterContent, buttonConfig, ListTweets, sizeButton, xy));
            MainGrid = MainGridWindowAplication;
        }

        public void InitTimerGetRandomTweet()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(SwitchTweets);
            timer.Interval = new TimeSpan(0, 0, 0, 0, speed * 9);
            timer.Start();
        }

        private void SwitchTweets(object sender, EventArgs e)
        {
            DependencyObject dependencyObject = MainGrid as DependencyObject;

            foreach (TweetContentClass tweets in tweetContentList)
            {
                foreach (TweetsUser tweet in tweets.tweetlist)
                {
                    randomTweets = rnd.Next(0, (tweets.countNumber - 1));
                    if (randomTweets == tweet.ID)
                    {
                        TextBlock tweetTime = LogicalTreeHelper.FindLogicalNode(dependencyObject, tweets.textTime) as TextBlock;
                        if (tweetTime != null)
                            tweetTime.Text = tweet.TimeCreateTwitt.Remove(19);

                        TextBlock tweetTweet = LogicalTreeHelper.FindLogicalNode(dependencyObject, tweets.textTweet) as TextBlock;
                        if (tweetTweet != null)
                            tweetTweet.Text = tweet.Twitt;
                    }
                }
            }
        }
    }
}
