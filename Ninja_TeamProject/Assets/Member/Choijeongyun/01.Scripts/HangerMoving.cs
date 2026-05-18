using System;
using DG.Tweening;
using UnityEngine;

namespace Member.Choijeongyun._01.Scripts
{
    public class HangerMoving : MonoBehaviour
    {
        [SerializeField] private float sAngle;
        [SerializeField] private float eAngle;
        [SerializeField] private float dur;
        private void Start()
        {
            HangerRotate();
        }

        private void HangerRotate()
        {
            Sequence seq = DOTween.Sequence();
            seq.Prepend(transform.DOLocalRotateQuaternion(Quaternion.Euler(0f, 0f, sAngle), dur).SetEase(Ease.InOutCubic));
            seq.Append(transform.DOLocalRotateQuaternion(Quaternion.Euler(0f, 0f, eAngle), dur).SetEase(Ease.InOutCubic));
            seq.OnComplete(HangerRotate);
        }
    }
}
