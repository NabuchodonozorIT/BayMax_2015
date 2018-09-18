using System.Windows.Controls;

namespace PluginInterface
{
    public interface IPlugin //: IDisposable
    {
        void Initialize(ref Grid gridScreen, ref Grid gridlayout, ref Grid gridEvent);
        void GameSnakePlay();
        void LeftGesturePassive();
        void RightGesturePassive();
        void DownGesturePassive();
        void UpGesturePassive();
    }
}
