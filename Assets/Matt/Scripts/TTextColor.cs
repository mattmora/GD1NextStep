using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
public class TTextColor : MonoBehaviour
{
    TMP_Text text;
    TTextInteraction interaction;

    Color vertexColor;
    public Color usedVertexColor = Color.gray;

    public List<Color> textColors = new List<Color>();
    public List<Color> usedColors = new List<Color>();
    List<Color> hoverColors = new List<Color>();

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        interaction = GetComponent<TTextInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        vertexColor = text.color;

        string[] s = text.text.Split(new string[] { "<color" }, StringSplitOptions.None);
        while (textColors.Count < s.Length - 1)
        {
            textColors.Add(Color.black);
            usedColors.Add(Color.black);
            //hoverColors.Add(Color.black);
        }

        if (interaction.numUses > 0)
        {
            text.color = vertexColor;
        }
        else 
        {
            text.color = usedVertexColor;
        }

        string newText = s[0];
        for (int i = 1; i < s.Length; ++i)
        {
            if (interaction.numUses > 0)
            {
                newText += "<color=#" + ColorUtility.ToHtmlStringRGBA(textColors[i - 1]) + ">";
            }
            else 
            {
                newText += "<color=#" + ColorUtility.ToHtmlStringRGBA(usedColors[i - 1]) + ">";
            }
            newText += s[i].Split(new char[] { '>' }, 2, StringSplitOptions.None)[1];
        }

        text.text = newText;
    }
}
