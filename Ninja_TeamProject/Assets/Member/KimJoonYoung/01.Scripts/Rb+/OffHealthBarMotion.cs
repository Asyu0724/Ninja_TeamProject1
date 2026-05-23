using DG.Tweening;
using UnityEngine;

namespace Member.KimJoonYoung._01.Scripts.Rb_
{
    public class OffHealthBarMotion : MonoBehaviour
    {
        public void StartMotion()
        {
            transform.DOLocalMoveY(500, 1.25f).SetEase(Ease.OutCubic);
        }
    }
}