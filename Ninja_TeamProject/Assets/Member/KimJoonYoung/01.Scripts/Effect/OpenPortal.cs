using DG.Tweening;
using UnityEngine;

public class OpenPortal : MonoBehaviour
{
    [SerializeField] GameObject player;
    void Start()
    {
        Sequence seq =  DOTween.Sequence();
        seq.PrependInterval(3.5f);
        seq.Append(transform.DOScaleX(0.5f, 0.5f).SetEase(Ease.OutCubic));
        seq.AppendInterval(1f).OnStepComplete(PlayerEnable);
        seq.Append(transform.DOScaleX(0f, 1.5f).SetEase(Ease.InCubic));
    }

    private void PlayerEnable()
    {
        player.SetActive(true);
    }
}
