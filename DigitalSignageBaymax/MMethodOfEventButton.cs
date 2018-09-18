using Microsoft.Kinect.Toolkit.Controls;

namespace DigitalSignageBaymax
{
    public class ButtonPath
    {
        public string source { get; set; }
        public string path1 { get; set; }
        public string path2 { get; set; }
        public string eventType { get; set; }
    }

    public class MethodOfEventButton
    {

        public ButtonPath getButtonPath(KinectTileButton button)
        {
            ButtonPath buttonPath = new ButtonPath();

            foreach (Button buttonConfig in MainWindow.buttonsCopy)
            {
                if (button.Name.Equals(string.Format("ButtonId{0}", buttonConfig.id)))
                {
                    buttonPath.source = buttonConfig.Source;
                    buttonPath.path1 = buttonConfig.Path1;
                    buttonPath.path2 = buttonConfig.Path2;
                    buttonPath.eventType = buttonConfig.Event;
                    break;
                }
            }
            return buttonPath;
        }

        public ButtonPath getButtonPath(string button)
        {
            ButtonPath buttonPath = new ButtonPath();

            foreach (Button buttonConfig in MainWindow.buttonsCopy)
            {
                if (button.Equals(string.Format("{0}", buttonConfig.id)))
                {
                    buttonPath.source = buttonConfig.Source;
                    buttonPath.path1 = buttonConfig.Path1;
                    buttonPath.path2 = buttonConfig.Path2;
                    buttonPath.eventType = buttonConfig.Event;
                    break;
                }
            }
            return buttonPath;
        }
    }
}
