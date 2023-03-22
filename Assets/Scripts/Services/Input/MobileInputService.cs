namespace Services.Input
{
  public class MobileInputService : InputService
  {
    public override bool OnClicked => UnityEngine.Input.touchCount > 0;
  }
}