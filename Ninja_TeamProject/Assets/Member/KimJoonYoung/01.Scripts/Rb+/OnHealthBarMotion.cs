using DG.Tweening;
using UnityEngine;

namespace Member.KimJoonYoung._01.Scripts.Rb_
{
    public class OnHealthBarMotion : MonoBehaviour
    {
        public void StartMotion()
        {
            transform.DOLocalMoveY(0, 1.25f).SetEase(Ease.OutCubic);
        }
    }
}
