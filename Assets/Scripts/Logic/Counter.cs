using TMPro;
using UnityEngine;

namespace Logic
{
    public class Counter : MonoBehaviour
    {
        [SerializeField] private TextMeshPro counterText;

        public void ChangeCounterValue(int amount)
        {
            counterText.text = amount.ToString();
        }
    }
}