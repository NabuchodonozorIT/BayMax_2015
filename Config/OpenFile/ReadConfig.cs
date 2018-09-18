using System;
using System.Xml;
using System.Collections.Generic;
using System.Windows;

namespace OpenFile
{
    public class ReadConfig
    {
        public string xmlUrl { get; set; }
        XmlDocument doc = new XmlDocument();
        private void ConnectionXML()
        {
            try
            {
                doc.Load(xmlUrl);
            }
            catch (Exception)
            {
                MessageBox.Show("wrong source XML");
            }
        }

        public List<ButtonXml> GetButtonConfig()
        {
            List<ButtonXml> buttons = new List<ButtonXml>();

            ConnectionXML();
            try
            {
                XmlNode ListNode = doc.SelectSingleNode("/Config/Buttons");
                XmlNodeList NodeList = ListNode.SelectNodes("Button");


                foreach (XmlNode node in NodeList)
                {
                    ButtonXml button = new ButtonXml();
                    button.id = Convert.ToInt32(node.Attributes.GetNamedItem("id").Value);
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
            catch (Exception exc) { MessageBox.Show(exc.Message); }

            return buttons;
        }

        public List<TutorialXml> GetTutorialConfig()
        {
            List<TutorialXml> tutoriallist = new List<TutorialXml>();

            ConnectionXML();
            try
            {
                XmlNode ListNode = doc.SelectSingleNode("/Config/Tutorials");
                XmlNodeList NodeList = ListNode.SelectNodes("Tutorial");


                foreach (XmlNode node in NodeList)
                {
                    TutorialXml tutorial = new TutorialXml();
                    tutorial.id = node.Attributes.GetNamedItem("id").Value;
                    tutorial.Source = node.Attributes.GetNamedItem("Source").Value;
                    tutorial.Volue = node.Attributes.GetNamedItem("Volue").Value;
                    tutorial.Active = node.Attributes.GetNamedItem("Active").Value;
                    tutorial.Type = Convert.ToBoolean(node.Attributes.GetNamedItem("Type").Value);
                    tutoriallist.Add(tutorial);
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }

            return tutoriallist;
        }

        public List<ContentTextXml> GetCaruseleTextConfig()
        {
            List<ContentTextXml> contentTexts = new List<ContentTextXml>();

            ConnectionXML();
            try
            {
                XmlNode ListNode = doc.SelectSingleNode("/Config/ContentTexts");
                XmlNodeList NodeList = ListNode.SelectNodes("ContentText");


                foreach (XmlNode node in NodeList)
                {
                    ContentTextXml contentText = new ContentTextXml();
                    contentText.id = Convert.ToInt32(node.Attributes.GetNamedItem("id").Value);
                    contentText.Text = node.Attributes.GetNamedItem("Text").Value;
                    contentTexts.Add(contentText);
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }

            return contentTexts;
        }

        public List<WaterMarkXml> GetWaterMarksConfig()
        {
            List<WaterMarkXml> waterMarks = new List<WaterMarkXml>();

            ConnectionXML();
            try
            {
                XmlNode ListNode = doc.SelectSingleNode("/Config/WaterMarks");
                XmlNodeList NodeList = ListNode.SelectNodes("WaterMark");


                foreach (XmlNode node in NodeList)
                {
                    WaterMarkXml waterMark = new WaterMarkXml();
                    waterMark.id = node.Attributes.GetNamedItem("id").Value;
                    waterMark.Source = node.Attributes.GetNamedItem("Source").Value;
                    waterMarks.Add(waterMark);
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }

            return waterMarks;
        }

        public TweetsXml GetTweetConfig()
        {
            TweetsXml tweets = new TweetsXml();

            ConnectionXML();
            try
            {
                XmlNode ListNode = doc.SelectSingleNode("/Config/Tweet");
                XmlNodeList NodeList = ListNode.SelectNodes("Tweets");

                foreach (XmlNode node in NodeList)
                {
                    tweets.tweetsText = node.Attributes.GetNamedItem("tweets").Value;
                    tweets.tweetsNumber = Convert.ToInt32(node.Attributes.GetNamedItem("tweetsNumber").Value);
                    tweets.tweetthash = Convert.ToBoolean(node.Attributes.GetNamedItem("tweethash").Value);
                    tweets.IdContent = node.Attributes.GetNamedItem("IdContent").Value;
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }


            return tweets;
        }

        public AccessTokenTwiitXml GetAccessTokenTwiitXml()
        {
            AccessTokenTwiitXml accessTokenTwiitXml = new AccessTokenTwiitXml();

            ConnectionXML();
            try
            {
                XmlNode ListNode = doc.SelectSingleNode("/Config/AccessTokenTwiits");
                XmlNodeList NodeList = ListNode.SelectNodes("AccessTokenTwiit");

                foreach (XmlNode node in NodeList)
                {
                    accessTokenTwiitXml.OAuthConsumerKey = node.Attributes.GetNamedItem("OAuthConsumerKey").Value;
                    accessTokenTwiitXml.OAuthConsumerSecret = node.Attributes.GetNamedItem("OAuthConsumerSecret").Value;
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }

            return accessTokenTwiitXml;
        }

        public LayoutXml GetLayoutConfig()
        {
            LayoutXml layout = new LayoutXml();

            ConnectionXML();

            try
            {
                XmlNode ListNode = doc.SelectSingleNode("/Config/Layouts");
                XmlNodeList NodeList = ListNode.SelectNodes("Layout");

                foreach (XmlNode node in NodeList)
                {
                    layout.Animation = Convert.ToBoolean(node.Attributes.GetNamedItem("Animation").Value);
                    layout.HorizontalVertical = Convert.ToBoolean(node.Attributes.GetNamedItem("HorizontalVertical").Value);
                    layout.ScreenEvent = Convert.ToBoolean(node.Attributes.GetNamedItem("ScreenEvent").Value);
                    layout.HeightButtonWidthButton = node.Attributes.GetNamedItem("HeightButton").Value;
                    layout.BorderBrush = node.Attributes.GetNamedItem("BorderBrush").Value;
                    layout.Opacity = node.Attributes.GetNamedItem("Opacity").Value;
                    layout.timeWatingScreenSaver = node.Attributes.GetNamedItem("TimeWatingScreenSaver").Value;
                    layout.HorizontalAlignment = node.Attributes.GetNamedItem("HorizontalAlignment").Value;
                    layout.SpeedGallery = Convert.ToInt16(node.Attributes.GetNamedItem("SpeedGallery").Value);
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }

            return layout;
        }

        public List<BackGroundXml> GetBackGroundConfig()
        {
            List<BackGroundXml> backGrounds = new List<BackGroundXml>();

            ConnectionXML();
            try
            {
                XmlNode ListNode = doc.SelectSingleNode("/Config/BackGrounds");
                XmlNodeList NodeList = ListNode.SelectNodes("BackGround");


                foreach (XmlNode node in NodeList)
                {
                    BackGroundXml backGround = new BackGroundXml();
                    backGround.id = node.Attributes.GetNamedItem("id").Value;
                    backGround.Source = node.Attributes.GetNamedItem("Source").Value;
                    backGrounds.Add(backGround);
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }

            return backGrounds;
        }

        public List<LogoXml> GetBackLogoConfig()
        {
            List<LogoXml> Logo = new List<LogoXml>();

            ConnectionXML();
            try
            {
                XmlNode ListNode = doc.SelectSingleNode("/Config/Logos");
                XmlNodeList NodeList = ListNode.SelectNodes("Logo");


                foreach (XmlNode node in NodeList)
                {
                    LogoXml logo = new LogoXml();
                    logo.id = node.Attributes.GetNamedItem("id").Value;
                    logo.Source = node.Attributes.GetNamedItem("Source").Value;
                    Logo.Add(logo);
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }

            return Logo;
        }

        public List<MovieXml> GetMovieConfig()
        {
            List<MovieXml> Movies = new List<MovieXml>();

            ConnectionXML();
            try
            {
                XmlNode ListNode = doc.SelectSingleNode("/Config/Movies");
                XmlNodeList NodeList = ListNode.SelectNodes("Movie");


                foreach (XmlNode node in NodeList)
                {
                    MovieXml Movie = new MovieXml();
                    Movie.id = node.Attributes.GetNamedItem("id").Value;
                    Movie.Icon = node.Attributes.GetNamedItem("Icon").Value;
                    Movie.Source = node.Attributes.GetNamedItem("Source").Value;
                    Movies.Add(Movie);
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }

            return Movies;
        }
    }
}
