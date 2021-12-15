using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TConvergence : MonoBehaviour
{
    public List<Transform> transforms;

    public Transform convergence;
    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    public float duration = 10f;

    public bool delayCloseObjects;

    public Ease ease;

    float maxDistance;

    List<float> distances;

    public bool test;

    public bool sceneTransition;

    private void Start()
    {
        test = false;
        distances = new List<float>();

        maxDistance = 0f;
        foreach (Transform t in transforms)
        {
            float distance = Vector3.Distance(t.position, convergence.position);
            distances.Add(distance);
            maxDistance = Mathf.Max(distance, maxDistance);
        }
    }

    private void Update()
    {
        if (test)
        {
            ConvergeObjects();
            test = false;
        }
    }

    public void ConvergeObjects()
    {
        Tween move;
        for (int i = 0; i < transforms.Count; ++i)
        {
            float dur = duration;
            if (delayCloseObjects)
            {
                dur *= distances[i] / maxDistance;
            }
            float delay = duration - dur;
            move = transforms[i].DOMove(convergence.position + positionOffset, dur).SetEase(ease).SetDelay(delay);
            transforms[i].DORotate(convergence.rotation.eulerAngles + rotationOffset, dur).SetEase(ease).SetDelay(delay);
        }

        if (sceneTransition)
        {
            Camera.main.DOColor(Color.white, 1f).SetDelay(duration-1f).OnComplete(() =>
        {
            SceneManager.LoadScene("AkshaySceneHaoTweak");
        });
        }
        
    }
}
