using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TPlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public List<TMP_Text> stepTexts;
    public List<TMP_Text> turnTexts;

    public Ease defaultMoveEase;
    public Ease defaultRotateEase;

    Camera mainCamera;

    public float fovChangeDuration = 1f;

    string stepWord
    {
        get { return stepWord; }
        set 
        { 
            stepWord = value; 
            foreach (var text in stepTexts)
            {
                text.text = stepWord;
            }
        }
    }

    string turnWord
    {
        get { return turnWord; }
        set
        {
            turnWord = value;
            foreach (var text in turnTexts)
            {
                text.text = turnWord;
            }
        }
    }

    private void Awake() 
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Tween MoveTo(Vector3 dest, float duration, Ease ease = Ease.Unset)
    {
        if (ease == Ease.Unset) ease = defaultMoveEase;
        return transform.DOMove(dest, duration).SetEase(ease);
    }

    public void RotateTo(Vector3 dest, float duration, Ease ease = Ease.Unset)
    {
        if (ease == Ease.Unset) ease = defaultRotateEase;
        transform.DORotate(dest, duration).SetEase(ease);
    }

    public void SetFOVChangeDuration(float dur)
    {
        fovChangeDuration = dur;
    }

    public void ChangeFOV(float fov)
    {
        mainCamera.DOFieldOfView(fov, fovChangeDuration);
    }
}
