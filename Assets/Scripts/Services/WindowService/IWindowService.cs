using System;
using Infrastructure.Services;

namespace Services.WindowService
{
    public interface IWindowService : IService
    {
        void Open(WindowID windowID, Action windowAction = null);
    }
}