using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerforScene4 : MonoBehaviour
{

    public float[] triggerPos;

    public GameObject[] letter;
    public GameObject[] keepMoving;

    public GameObject player;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        for (int i= 0; i< letter.Length; i++)
        {
            if (player.transform.position.x > triggerPos[i])
            {
                letter[i].SetActive(true);
                keepMoving[i].SetActive(true);
            }
        }

        if (player.transform.position.x > triggerPos[14])
        {
            SceneManager.LoadScene("Scene5");
        }





    }
}
