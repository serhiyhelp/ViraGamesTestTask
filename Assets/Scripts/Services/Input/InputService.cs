namespace Services.Input
{
  public abstract class InputService : IInputService
  {
    public abstract bool OnClicked { get; }
  }
}