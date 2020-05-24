using DG.Tweening;
using UnityEngine;

public class LandingLight : MonoBehaviour
{
    public float startDelay = 0;
    private void Start()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 1);
        transform.DOScale(new Vector3(0.6f, 0.6f, 0), 0.65f)
            .SetDelay(startDelay)
            .SetEase(Ease.InOutCubic)
            .SetLoops(-1, LoopType.Restart);
    }
}
