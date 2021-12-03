using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMP_TextEventCheck : MonoBehaviour
{

    public TLinkHandler TextEventHandler;

    private TMP_Text m_TextComponent;

    void OnEnable()
    {
        if (TextEventHandler != null)
        {
            // Get a reference to the text component
            m_TextComponent = TextEventHandler.GetComponent<TMP_Text>();

            TextEventHandler.onLinkEnter.AddListener(OnLinkEnter);
            TextEventHandler.onLinkExit.AddListener(OnLinkExit);
            TextEventHandler.onLinkClick.AddListener(OnLinkClick);
        }
    }


    void OnDisable()
    {
        if (TextEventHandler != null)
        {
            TextEventHandler.onLinkEnter.RemoveListener(OnLinkEnter);
            TextEventHandler.onLinkExit.RemoveListener(OnLinkExit);
            TextEventHandler.onLinkClick.RemoveListener(OnLinkClick);
        }
    }

    void OnLinkEnter(string linkID, string linkText, int linkIndex)
    {
        if (m_TextComponent != null)
        {
            TMP_LinkInfo linkInfo = m_TextComponent.textInfo.linkInfo[linkIndex];
        }

        Debug.Log("Link Index: " + linkIndex + " with ID [" + linkID + "] and Text \"" + linkText + "\" has been ENTERED.");
    }

    void OnLinkExit(string linkID, string linkText, int linkIndex)
    {
        if (m_TextComponent != null)
        {
            TMP_LinkInfo linkInfo = m_TextComponent.textInfo.linkInfo[linkIndex];
        }

        Debug.Log("Link Index: " + linkIndex + " with ID [" + linkID + "] and Text \"" + linkText + "\" has been EXITED.");
    }

    void OnLinkClick(string linkID, string linkText, int linkIndex)
    {
        if (m_TextComponent != null)
        {
            TMP_LinkInfo linkInfo = m_TextComponent.textInfo.linkInfo[linkIndex];
        }

        Debug.Log("Link Index: " + linkIndex + " with ID [" + linkID + "] and Text \"" + linkText + "\" has been CLICKED.");
    }
}
