using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

[ExecuteInEditMode]
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
            string stippedBlock = RemoveBetween(textBlock, "[", "]");
            string[] texts = textBlock.Split(delimiters, 1000000, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (var t in texts)
            {
                string text = t.Trim();
                Instantiate(textPrefab);
                textPrefab.GetComponent<TMP_Text>().text = text;
            }
            activate = false;
        }
    }

    public static string RemoveBetween(string original, string firstTag, string secondTag)
{
   string pattern = firstTag + "(.*?)" + secondTag;
   Regex regex = new Regex(pattern, RegexOptions.RightToLeft);

   foreach(Match match in regex.Matches(original))
   {
      original = original.Replace(match.Groups[1].Value, string.Empty);
   }

   return original;
}
}
