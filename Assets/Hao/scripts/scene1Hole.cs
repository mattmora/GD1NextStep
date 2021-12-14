using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scene1Hole : MonoBehaviour
{
    public TMP_Text[] textWall;
    //public GameObject[] textWall;
    public float timer;
    public float timeGap;
    int index = 0;

    public float timeBeforeFall;

    public GameObject originalPlayer;
    public GameObject cameraMoving;
    public GameObject newPlayer;

    void Start()
    {
        Invoke("spawnNewWall", timer );
        Invoke("movetheCamera", timeBeforeFall );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnNewWall()
    {
        if (index < textWall.Length)
        {
            textWall[index].alpha = 1;
            //textWall[index].CrossFadeAlpha(1, 0.1f, true);
            textWall[index].gameObject.SetActive(true);
            index += 1;
            timer -= timeGap;
            Invoke("spawnNewWall", timer);
        }
        
    }

    void movetheCamera()
    {
        originalPlayer.SetActive(false);
        cameraMoving.SetActive(true);
        Invoke("fall", 2);
    }


    void fall()
    {
        cameraMoving.SetActive(false);
        newPlayer.SetActive(true);

    }

}
