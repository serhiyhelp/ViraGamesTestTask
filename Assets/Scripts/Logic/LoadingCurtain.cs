using System.Collections;
using UnityEngine;

namespace Logic
{
  public class LoadingCurtain : MonoBehaviour
  {
    [SerializeField] private CanvasGroup _canvas;

    private void Awake()
    {
      DontDestroyOnLoad(this);
    }

    public void Show()
    {
      gameObject.SetActive(true);
      _canvas.alpha = 1;
    }
    
    public void Hide() => StartCoroutine(DoFadeIn());
    
    private IEnumerator DoFadeIn()
    {
      while (_canvas.alpha > 0)
      {
        _canvas.alpha -= 0.03f;
        yield return new WaitForSecondsRealtime(0.03f);
      }
      
      gameObject.SetActive(false);
    }
  }
}