using System;

namespace DigitalSignageBaymax
{
    public class Button
    {
        public string id { get; set; }
        public string Icon { get; set; }
        public string Event { get; set; }
        public string IdContent { get; set; }
        public string Source { get; set; }
        public string Path1 { get; set; }
        public string Path2 { get; set; }
        public string WindowSize { get; set; }
        public bool EventStartUp { get; set; }
    }

    public class Tutorial
    {
        public string id { get; set; }
        public string Source { get; set; }
        public double Volue { get; set; }
        public bool Active { get; set; }
        public bool Type { get; set; }
    }

    public class Gesture
    {
        public string id { get; set; }
        public bool enable { get; set; }
    }

    public class ContentText
    {
        public int id { get; set; }
        public string Text { get; set; }
    }

    public class WaterMark
    {
        public string id { get; set; }
        public string Source { get; set; }
    }

    public class BackGround
    {
        public string id { get; set; }
        public string Source { get; set; }
    }

    public class Logo
    {
        public string id { get; set; }
        public string Source { get; set; }
    }

    public class Movie
    {
        public string id { get; set; }
        public string Icon { get; set; }
        public string Source { get; set; }
    }

    public class Tweets
    {
        public string tweetsText { get; set; }
        public int tweetsNumber { get; set; }
        public bool tweetthash { get; set; }
        public string IdContent { get; set; }
    }

    public class Layout
    {
        public bool HorizontalVertical { get; set; }
        public bool Animation { get; set; }
        public int HeightButton { get; set; }
        public int WidthButton { get; set; }
        public string BorderBrush { get; set; }
        public double Opacity { get; set; }
        public bool ScreenEvent { get; set; }
        public Int16 timeWatingScreenSaver { get; set; }
        public int SpeedGallery { get; set; }
        public string HorizontalAlignment { get; set; }
    }
}
