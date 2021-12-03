using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class TLinkHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Serializable]
    public class LinkSelectionEvent : UnityEvent<string, string, int> { }

    /// <summary>
    /// Event delegate triggered when pointer is over a link.
    /// </summary>
    public LinkSelectionEvent onLinkEnter
    {
        get { return m_OnLinkEnter; }
        set { m_OnLinkEnter = value; }
    }
    [SerializeField]
    private LinkSelectionEvent m_OnLinkEnter = new LinkSelectionEvent();

    public LinkSelectionEvent onLinkExit
    {
        get { return m_OnLinkExit; }
        set { m_OnLinkExit = value; }
    }
    [SerializeField]
    private LinkSelectionEvent m_OnLinkExit = new LinkSelectionEvent();

    public LinkSelectionEvent onLinkClick
    {
        get { return m_OnLinkClick; }
        set { m_OnLinkClick = value; }
    }
    [SerializeField]
    private LinkSelectionEvent m_OnLinkClick = new LinkSelectionEvent();


    private TMP_Text m_TextComponent;

    private Camera m_Camera;
    private Canvas m_Canvas;

    private int m_selectedLink = -1;

    void Awake()
    {
        // Get a reference to the text component.
        m_TextComponent = gameObject.GetComponent<TMP_Text>();

        // Get a reference to the camera rendering the text taking into consideration the text component type.
        if (m_TextComponent.GetType() == typeof(TextMeshProUGUI))
        {
            m_Canvas = gameObject.GetComponentInParent<Canvas>();
            if (m_Canvas != null)
            {
                if (m_Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                    m_Camera = null;
                else
                    m_Camera = m_Canvas.worldCamera;
            }
        }
        else
        {
            m_Camera = Camera.main;
        }
    }


    void LateUpdate()
    {
        if (TMP_TextUtilities.IsIntersectingRectTransform(m_TextComponent.rectTransform, Input.mousePosition, m_Camera))
        {
            // Check if mouse intersects with any links.
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(m_TextComponent, Input.mousePosition, m_Camera);

            // Handle new Link selection.
            if (linkIndex != -1 && linkIndex != m_selectedLink)
            {
                m_selectedLink = linkIndex;

                // Get information about the link.
                TMP_LinkInfo linkInfo = m_TextComponent.textInfo.linkInfo[linkIndex];

                // Send the event to any listeners.
                SendOnLinkEnter(linkInfo.GetLinkID(), linkInfo.GetLinkText(), linkIndex);

                //m_TextComponent.textInfo.linkInfo[linkIndex].
            }
        }
        else
        {
            if (m_selectedLink != -1)
            {
                TMP_LinkInfo linkInfo = m_TextComponent.textInfo.linkInfo[m_selectedLink];
                SendOnLinkExit(linkInfo.GetLinkID(), linkInfo.GetLinkText(), m_selectedLink);
            }

            // Reset all selections given we are hovering outside the text container bounds.
            m_selectedLink = -1;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (m_selectedLink != -1)
            {
                TMP_LinkInfo linkInfo = m_TextComponent.textInfo.linkInfo[m_selectedLink];
                SendOnLinkClick(linkInfo.GetLinkID(), linkInfo.GetLinkText(), m_selectedLink);
            }
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter()");
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit()");
    }

    private void SendOnLinkEnter(string linkID, string linkText, int linkIndex)
    {
        Debug.Log("Enter " + linkIndex);
        if (onLinkEnter != null)
            onLinkEnter.Invoke(linkID, linkText, linkIndex);
    }

    private void SendOnLinkExit(string linkID, string linkText, int linkIndex)
    {
        Debug.Log("Exit " + linkIndex);
        if (onLinkExit != null)
            onLinkExit.Invoke(linkID, linkText, linkIndex);
    }

    private void SendOnLinkClick(string linkID, string linkText, int linkIndex)
    {
        Debug.Log("Click " + linkIndex);
        if (onLinkClick != null)
            onLinkClick.Invoke(linkID, linkText, linkIndex);
    }
}
