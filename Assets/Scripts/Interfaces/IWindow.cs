namespace Interfaces
{
    public interface IWindow
    {
        void Show();
        void Hide();
        bool IsShown { get; }
    }
}