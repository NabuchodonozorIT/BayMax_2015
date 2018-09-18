
namespace OpenFile
{
    public class ButtonXml
    {
        public int id { get; set; }
        public string Icon { get; set; }
        public string Event { get; set; }
        public string IdContent { get; set; }
        public string Source { get; set; }
        public string Path1 { get; set; }
        public string Path2 { get; set; }
        public string WindowSize { get; set; }
        public bool EventStartUp { get; set; }
    }

    public class TutorialXml
    {
        public string id { get; set; }
        public string Source { get; set; }
        public string Volue { get; set; }
        public string Active { get; set; }
        public bool Type { get; set; }
    }

    public class ContentTextXml
    {
        public int id { get; set; }
        public string Text { get; set; }
    }

    public class AccessTokenTwiitXml
    {
        public string OAuthConsumerKey { get; set; }
        public string OAuthConsumerSecret { get; set; }
    }

    public class WaterMarkXml
    {
        public string id { get; set; }
        public string Source { get; set; }
    }
    public class BackGroundXml
    {
        public string id { get; set; }
        public string Source { get; set; }
    }
    public class LogoXml
    {
        public string id { get; set; }
        public string Source { get; set; }
    }
    public class MovieXml
    {
        public string id { get; set; }
        public string Icon { get; set; }
        public string Source { get; set; }
    }
    public class TweetsXml
    {
        public string tweetsText { get; set; }
        public int tweetsNumber { get; set; }
        public bool tweetthash { get; set; }
        public string IdContent { get; set; }
    }
    public class LayoutXml
    {
        public bool HorizontalVertical { get; set; }
        public string HeightButtonWidthButton { get; set; }
        public string BorderBrush { get; set; }
        public string Opacity { get; set; }
        public bool Animation { get; set; }
        public bool ScreenEvent { get; set; }
        public string HorizontalAlignment { get; set; }
        public string timeWatingScreenSaver { get; set; }
        public int SpeedGallery { get; set; }
    }
}
