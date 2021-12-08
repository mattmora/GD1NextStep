using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode;]
public class TTextObjectGenerator : MonoBehaviour
{
    public string textBlock;

    public string[] delimiters;

    public GameObject textPrefab;
    
    public bool activate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            textBlock.Split()
            activate = false;
        }
    }
}
