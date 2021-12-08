using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

[ExecuteInEditMode]
public class TTextObjectGenerator : MonoBehaviour
{
    [TextArea]
    public string textBlock;

    string[] delimiters;

    public GameObject textPrefab;

    public bool activate;

    // Start is called before the first frame update
    void Start()
    {
        delimiters = new string[] { "\n" };
        textPrefab.name = "InteractableText";
    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            int objNum = 1;
            // string strippedBlock = RemoveBetween(textBlock, "[", "]");
            string strippedBlock = textBlock;
            string[] texts = strippedBlock.Split('\n');
            foreach (var t in texts)
            {
                string text = t.Trim();
                if (text == string.Empty) continue;
                var go = Instantiate(textPrefab);
                go.GetComponent<TMP_Text>().text = text;
                go.name = objNum.ToString() + " " + text.Substring(0, text.Length > 15 ? 15 : text.Length);
                objNum++;
            }
            activate = false;
        }
    }

    public static string RemoveBetween(string original, string firstTag, string secondTag)
    {
        string pattern = firstTag + "(.*?)" + secondTag;
        Regex regex = new Regex(pattern);

        foreach (Match match in regex.Matches(original))
        {
            if (match.Value != string.Empty)
            {
                original = original.Replace(match.Value, string.Empty);
            }
        }
        return original;
    }
}
