using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
public class TTextColor : MonoBehaviour
{
    TMP_Text text;

    public List<Color> textColors = new List<Color>();
    List<Color> hoverColors = new List<Color>();

    bool mouseIsOver;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string[] s = text.text.Split(new string[] { "<color" }, StringSplitOptions.None);
        if (textColors.Count != s.Length - 1)
        {
            textColors.Clear();
            hoverColors.Clear();

            for (int i = 1; i < s.Length; ++i)
            {
                textColors.Add(Color.black);
                hoverColors.Add(Color.black);
            }
        }

        string newText = s[0];
        for (int i = 1; i < s.Length; ++i)
        {
            if (!mouseIsOver)
            {
                newText += "<color=#" + ColorUtility.ToHtmlStringRGBA(textColors[i - 1]) + ">";
            }
            else 
            {
                newText += "<color=#" + ColorUtility.ToHtmlStringRGBA(hoverColors[i - 1]) + ">";
            }
            newText += s[i].Split(new char[] { '>' }, 2, StringSplitOptions.None)[1];
        }

        text.text = newText;

        mouseIsOver = false;
    }

    private void OnMouseOver()
    {
        //mouseIsOver = true ;
    }
}
