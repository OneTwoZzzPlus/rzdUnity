using System;

namespace Interfaces
{
    public interface IWindowController
    {
        IWindow ShowWindow(Type type);
        void HideWindow(Type window);
    }
}