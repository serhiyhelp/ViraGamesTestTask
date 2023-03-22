namespace Services.Input
{
    public class StandaloneInputService : InputService
    {
        public override bool OnClicked => IsFireButtonDown();
        private static bool IsFireButtonDown() => UnityEngine.Input.GetMouseButtonDown(0);
    }
}