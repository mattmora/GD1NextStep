using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class scene1Hole : MonoBehaviour
{
    public TMP_Text[] textWall;
    //public GameObject[] textWall;
    public float timer;
    public float timeGap;
    int index = 0;

    public float timeBeforeFall;

    public GameObject originalPlayer;
    // public GameObject cameraMoving;
    // public GameObject newPlayer;

    public Ease ease;
    public Ease fallEase;

    // public List<UnityEvent> onComplete;
    public Scene scene2;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RunAnimation()
    {
        transform.DORotate(new Vector3(0f, 360f, 0f), 3.33f, RotateMode.WorldAxisAdd).SetLoops(-1).SetEase(ease);
        Camera.main.DOColor(Color.white, 1f).SetDelay(3.6f);
        transform.DOMove(new Vector3(0f, 60f, 0f), 4.6f).SetEase(fallEase).SetDelay(0f).OnComplete(() =>
        {
            SceneManager.LoadScene("Evie_awakening");
            // foreach (var cb in onComplete)
            // {
            //     cb.Invoke();
                
            // }
        });
        Invoke("spawnNewWall", timer );
        Invoke("movetheCamera", timeBeforeFall);
    }

    void spawnNewWall()
    {
        if (index < textWall.Length)
        {
            textWall[index].DOFade(1f, 1f);
            //textWall[index].CrossFadeAlpha(1, 0.1f, true);
            // textWall[index].gameObject.SetActive(true);
            index += 1;
            timer -= timeGap;
            Invoke("spawnNewWall", timer);
        }
        
    }

    void movetheCamera()
    {
        // originalPlayer.DOMove(landing.position, 2f);
        // originalPlayer.SetActive(false);
        // cameraMoving.SetActive(true);
        // Invoke("fall", 2);
    }
}
