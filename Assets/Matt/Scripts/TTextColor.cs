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

    bool used;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        interaction = GetComponent<TTextInteraction>();
    }

    void Start()
    {
        vertexColor = text.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (text == null)
        {
            text = GetComponent<TMP_Text>();
        }
        if (interaction == null)
        {
            interaction = GetComponent<TTextInteraction>();
        }

        UpdateColors();
    }

    public void SetUsed(bool u)
    {
        used = u;
        text.color = used ? usedVertexColor : vertexColor;

        UpdateColors();
    }

    private void UpdateColors()
    {
        string[] s = text.text.Split(new string[] { "<color" }, StringSplitOptions.None);
        while (textColors.Count < s.Length - 1)
        {
            textColors.Add(Color.black);
            //hoverColors.Add(Color.black);
        }
        while (usedColors.Count < s.Length - 1)
        {
            usedColors.Add(Color.black);
        }

        if (s.Length <= 1) return;

        string newText = s[0];
        for (int i = 1; i < s.Length; ++i)
        {
            if (!used)
            {
                // Debug.Log(textColors[i - 1]);
                newText += "<color=#" + ColorUtility.ToHtmlStringRGBA(textColors[i - 1]) + ">";
            }
            else 
            {
                newText += "<color=#" + ColorUtility.ToHtmlStringRGBA(usedColors[i - 1]) + ">";
            }
            string[] post = s[i].Split(new char[] { '>' }, 2, StringSplitOptions.None);
            if (post.Length > 1) newText += post[1];
        }

        text.text = newText;
    }
}
