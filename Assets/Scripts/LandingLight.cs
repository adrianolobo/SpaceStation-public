using DG.Tweening;
using UnityEngine;

public class LandingLight : MonoBehaviour
{
    private void Start()
    {
        transform.localScale = new Vector3(0.2f, 0.2f, 1);
        transform.DOScale(new Vector3(1, 1, 0), 0.6f).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Restart);
    }
}
