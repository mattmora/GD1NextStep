using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCameraColor : MonoBehaviour
{
    public void SetToBlack()
    {
        Camera.main.backgroundColor = Color.black;
    }

    public void SetToWhite()
    {
        Camera.main.backgroundColor = Color.white;
    }
}
