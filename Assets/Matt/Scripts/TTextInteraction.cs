using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TTextInteraction : MonoBehaviour
{
    [Tooltip("Transform to align the player to on clicking the text. Will use the player position with given position and rotation adjustments if null.")]
    [SerializeField] Transform targetView;
    [Tooltip("Position offset from target view.")]
    [SerializeField] Vector3 positionAdjustment;
    [Tooltip("Rotation offset from target view.")]
    [SerializeField] Vector3 rotationAdjustment;

    [SerializeField] float moveDuration = 1f;
    [SerializeField] float rotateDuration = 1f;

    [Header("Callbacks")]
    [SerializeField] List<UnityEvent> onMouseDown;
    [SerializeField] List<UnityEvent> onMouseUp;
    [SerializeField] List<UnityEvent> onMouseEnter;
    [SerializeField] List<UnityEvent> onMouseExit;
    [SerializeField] List<UnityEvent> onMouseOver;

    [SerializeField] List<GameObject> linkedObjects;

    TPlayerController player;
    TMP_Text text;
    BoxCollider boxCollider;

    public bool interactable = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<TPlayerController>();
        text = GetComponent<TMP_Text>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the collider to match the text bounds
        // (the extra size compared to mesh bounds feels better on slanted text)
        //boxCollider.center = text.textBounds.center;
        //boxCollider.size = text.textBounds.size;

        // Or the mesh bounds? Subtly different, mesh is tighter
        boxCollider.center = text.mesh.bounds.center;
        boxCollider.size = text.mesh.bounds.size;
    }

    private void LateUpdate()
    {
        
    }

    // When the mouse is pressed on the collider
    private void OnMouseDown()
    {
        Debug.Log("Mouse Down on " + text.text);
        foreach (var e in onMouseDown)
        {
            e.Invoke();
        }
    }

    // When the mouse is released over the collider after a press (a complete "click")
    private void OnMouseUpAsButton()
    {
        foreach (var e in onMouseUp)
        {
            e.Invoke();
        }

        // Apply adjustments
        Vector3 targetPosition = player.transform.TransformDirection(positionAdjustment);
        Vector3 targetRotation = rotationAdjustment;
        // If there is no target view, just adjust the current view
        if (targetView == null)
        {
            targetPosition += player.transform.position;
            targetRotation += player.transform.eulerAngles;
        }
        // Otherwise use the target view
        else
        {
            targetPosition += targetView.position;
            targetRotation += targetView.eulerAngles;
        }
        // Apply new position and rotation
        player.MoveTo(targetPosition, moveDuration);
        player.RotateTo(targetRotation, rotateDuration);
    }

    // When the mouse enters the collider
    private void OnMouseEnter()
    {
        foreach (var e in onMouseEnter)
        {
            e.Invoke();
        }
    }

    // When the mouse exits the collider
    private void OnMouseExit()
    {
        foreach (var e in onMouseExit)
        {
            e.Invoke();
        }
    }

    // Every frame the mouse is over the collider
    private void OnMouseOver()
    {
        foreach (var e in onMouseOver)
        {
            e.Invoke();
        }
    }

    public void SetTextColor(Color color)
    {
        text.color = color;
    }

    public void LinkToText(string linkId, string linkText, int linkIndex)
    {
        linkedObjects[linkIndex].SetActive(true);
        gameObject.SetActive(false);
    }

    public void DisableInteraction()
    {
        interactable = false;
        text.color = Color.black;
    }
}