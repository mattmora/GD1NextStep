using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSpriteRendererManager : MonoBehaviour
{
    public List<SpriteRenderer> renderers;

    float targetAlpha;
    float fadeDuration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Fade()
    {
        foreach (var r in renderers)
        {
            r.DOFade(targetAlpha, fadeDuration);
        }
    }
}
