using System;
using System.Xml;
using System.Collections.Generic;
using System.Windows;

namespace DigitalSignageBaymax
{
    class ReadConfig
    {
        XmlDocument doc = new XmlDocument();

        private void ConnectionXML()
        {
            try
            {
                doc.Load(MainWindow.xmlUrl);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        public List<Button> GetButtonConfig()
        {
            List<Button> buttons = new List<Button>();

            try
            {
                XmlNode ListNode = doc.SelectSingleNode("/Config/Buttons");
                XmlNodeList NodeList = ListNode.SelectNodes("Button");

                foreach (XmlNode node in NodeList)
                {
                    Button button = new Button();
                    button.id = node.Attributes.GetNamedItem("id").Value;
                    button.Icon = node.Attributes.GetNamedItem("Icon").Value;
                    button.Event = node.Attributes.GetNamedItem("Event").Value;
                    button.IdContent = node.Attributes.GetNamedItem("IdContent").Value;
                    button.Source = node.Attributes.GetNamedItem("Source").Value;
                    button.Path1 = node.Attributes.GetNamedItem("Path1").Value;
                    button.Path2 = node.Attributes.GetNamedItem("Path2").Value;
                    button.WindowSize = node.Attributes.GetNamedItem("WindowSize").Value;
                    button.EventStartUp = Convert.ToBoolean(node.Attributes.GetNamedItem("EventStartUp").Value);
                    buttons.Add(button);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }   
            return buttons;
        }

        public List<ContentText> GetCaruseleTextConfig()
        {
            List<ContentText> contentTexts = new List<ContentText>();

            try
            {
                XmlNode ListNode = doc.SelectSingleNode("/Config/ContentTexts");
                XmlNodeList NodeList = ListNode.SelectNodes("ContentText");

                foreach (XmlNode node in NodeList)
                {
                    ContentText contentText = new ContentText();
                    contentText.id = Convert.ToInt32(node.Attributes.GetNamedItem("id").Value);
                    contentText.Text = node.Attributes.GetNamedItem("Text").Value;
                    contentTexts.Add(contentText);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

            
            return contentTexts;
        }

        public List<Gesture> GetGestureConfig()
        {
            List<Gesture> gestureList = new List<Gesture>();

            try
            {
                XmlNode ListNode = doc.SelectSingleNode("/Config/Gestures");
                XmlNodeList NodeList = ListNode.SelectNodes("Gesture");

                foreach (XmlNode node in NodeList)
                {
                    Gesture gesture = new Gesture();
                    gesture.id = node.Attributes.GetNamedItem("id").Value;
                    gesture.enable = Convert.ToBoolean(node.Attributes.GetNamedItem("enable").Value);
                    gestureList.Add(gesture);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

           
            return gestureList;
        }

        public List<Tutorial> GetTutorialConfig()
        {
            List<Tutorial> tutoriallist = new List<Tutorial>();

            try
            {
                XmlNode ListNode = doc.SelectSingleNode("/Config/Tutorials");
                XmlNodeList NodeList = ListNode.SelectNodes("Tutorial");

                foreach (XmlNode node in NodeList)
                {
                    Tutorial tutorial = new Tutorial();
                    tutorial.id = node.Attributes.GetNamedItem("id").Value;
                    tutorial.Source = node.Attributes.GetNamedItem("Source").Value;
                    tutorial.Volue = Convert.ToDouble(node.Attributes.GetNamedItem("Volue").Value);
                    tutorial.Active = Convert.ToBoolean(node.Attributes.GetNamedItem("Active").Value);
                    tutorial.Type = Convert.ToBoolean(node.Attributes.GetNamedItem("Type").Value);
                    tutoriallist.Add(tutorial);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            
            return tutoriallist;
        }

        public List<WaterMark> GetWaterMarksConfig()
        {
            List<WaterMark> waterMarks = new List<WaterMark>();

            try
            {
                XmlNode ListNode = doc.SelectSingleNode("/Config/WaterMarks");
                XmlNodeList NodeList = ListNode.SelectNodes("WaterMark");

                foreach (XmlNode node in NodeList)
                {
                    WaterMark waterMark = new WaterMark();
                    waterMark.id = node.Attributes.GetNamedItem("id").Value;
                    waterMark.Source = node.Attributes.GetNamedItem("Source").Value;
                    waterMarks.Add(waterMark);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
           
            return waterMarks;
        }

        public List<Tweets> GetTweetConfig()
        {
            List<Tweets> tweets = new List<Tweets>();

            try
            {
                XmlNode ListNode = doc.SelectSingleNode("/Config/Tweet");
                XmlNodeList NodeList = ListNode.SelectNodes("Tweets");

                foreach (XmlNode node in NodeList)
                {
                    Tweets tweet = new Tweets();
                    tweet.tweetsText = node.Attributes.GetNamedItem("tweets").Value;
                    tweet.tweetsNumber = Convert.ToInt32(node.Attributes.GetNamedItem("tweetsNumber").Value);
                    tweet.tweetthash = Convert.ToBoolean(node.Attributes.GetNamedItem("tweethash").Value);
                    tweet.IdContent = node.Attributes.GetNamedItem("IdContent").Value;
                    tweets.Add(tweet);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

            
            
            return tweets;
        }

        public Layout GetLayoutConfig()
        {
            Layout layout = new Layout();

            try
            {
                XmlNode ListNode = doc.SelectSingleNode("/Config/Layouts");
                XmlNodeList NodeList = ListNode.SelectNodes("Layout");

                foreach (XmlNode node in NodeList)
                {
                    layout.HorizontalVertical = Convert.ToBoolean(node.Attributes.GetNamedItem("HorizontalVertical").Value);
                    layout.HeightButton = Convert.ToInt32(node.Attributes.GetNamedItem("HeightButton").Value);
                    layout.WidthButton = Convert.ToInt32(node.Attributes.GetNamedItem("WidthButton").Value);
                    layout.Animation = Convert.ToBoolean(node.Attributes.GetNamedItem("Animation").Value);
                    layout.BorderBrush = node.Attributes.GetNamedItem("BorderBrush").Value;
                    layout.Opacity = Convert.ToDouble(node.Attributes.GetNamedItem("Opacity").Value);
                    layout.ScreenEvent = Convert.ToBoolean(node.Attributes.GetNamedItem("ScreenEvent").Value);
                    layout.timeWatingScreenSaver = Convert.ToInt16(node.Attributes.GetNamedItem("TimeWatingScreenSaver").Value);
                    layout.HorizontalAlignment = node.Attributes.GetNamedItem("HorizontalAlignment").Value;
                    layout.SpeedGallery = Convert.ToInt16(node.Attributes.GetNamedItem("SpeedGallery").Value);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }          
            return layout;
        }

        public List<BackGround> GetBackGroundConfig()
        {
            List<BackGround> backGrounds = new List<BackGround>();

            try
            {
                XmlNode ListNode = doc.SelectSingleNode("/Config/BackGrounds");
                XmlNodeList NodeList = ListNode.SelectNodes("BackGround");

                foreach (XmlNode node in NodeList)
                {
                    BackGround backGround = new BackGround();
                    backGround.id = node.Attributes.GetNamedItem("id").Value;
                    backGround.Source = node.Attributes.GetNamedItem("Source").Value;
                    backGrounds.Add(backGround);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }          
            return backGrounds;
        }

        public List<Logo> GetBackLogoConfig()
        {
            List<Logo> Logo = new List<Logo>();

            try
            {
                XmlNode ListNode = doc.SelectSingleNode("/Config/Logos");
                XmlNodeList NodeList = ListNode.SelectNodes("Logo");
                foreach (XmlNode node in NodeList)
                {
                    Logo logo = new Logo();
                    logo.id = node.Attributes.GetNamedItem("id").Value;
                    logo.Source = node.Attributes.GetNamedItem("Source").Value;
                    Logo.Add(logo);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }     
            return Logo;
        }

        public List<Movie> GetMovieConfig()
        {
            List<Movie> Movies = new List<Movie>();

            try
            {
                XmlNode ListNode = doc.SelectSingleNode("/Config/Movies");
                XmlNodeList NodeList = ListNode.SelectNodes("Movie");
                foreach (XmlNode node in NodeList)
                {
                    Movie Movie = new Movie();
                    Movie.id = node.Attributes.GetNamedItem("id").Value;
                    Movie.Icon = node.Attributes.GetNamedItem("Icon").Value;
                    Movie.Source = node.Attributes.GetNamedItem("Source").Value;
                    Movies.Add(Movie);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return Movies;
        }
    }
}
