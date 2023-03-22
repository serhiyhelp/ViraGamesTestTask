using Infrastructure.Services;

namespace Services.Input
{
  public interface IInputService : IService
  {
    bool OnClicked { get; }
  }
}