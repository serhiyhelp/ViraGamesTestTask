using UnityEngine;

namespace Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
    
        private static readonly int IsFighting = Animator.StringToHash("IsFighting");

        private void OnEnable()
        {
            IsFightingAnimation(true);
        }

        public void IsFightingAnimation(bool isPlaying)
        {
            animator.SetBool(IsFighting, isPlaying);
        }
    }
}
