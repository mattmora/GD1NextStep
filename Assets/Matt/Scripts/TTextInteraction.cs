using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using DG.Tweening;

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

    [SerializeField] float textMoveDuration = 1f;

    // [Header("Callbacks")]
    // [SerializeField] List<UnityEvent> onMouseDown;
    // [SerializeField] List<UnityEvent> onMouseUp;
    // [SerializeField] List<UnityEvent> onMouseEnter;
    // [SerializeField] List<UnityEvent> onMouseExit;
    // [SerializeField] List<UnityEvent> onMouseOver;

    [SerializeField] List<UnityEvent> onTimerEnd;

    [SerializeField] List<GameObject> linkedObjects;

    [SerializeField] bool useTimer = false;
    [SerializeField] float timerDuration;

    TPlayerController player;
    TMP_Text text;
    BoxCollider boxCollider;

    public bool interactable = true;

    public bool disableOnClick = true;
    public float disableFadeDuration = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<TPlayerController>();
        text = GetComponent<TMP_Text>();
        boxCollider = GetComponent<BoxCollider>();

        if (useTimer)
        {
            StartCoroutine(TimerCountdown());
        }
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
    // private void OnMouseDown()
    // {
    //     Debug.Log("Mouse Down on " + text.text);
    //     foreach (var e in onMouseDown)
    //     {
    //         e.Invoke();
    //     }
    // }

    // // When the mouse is released over the collider after a press (a complete "click")
    // private void OnMouseUpAsButton()
    // {
    //     foreach (var e in onMouseUp)
    //     {
    //         e.Invoke();
    //     }
    //     // MovePlayer();
    // }

    // // When the mouse enters the collider
    // private void OnMouseEnter()
    // {
    //     foreach (var e in onMouseEnter)
    //     {
    //         e.Invoke();
    //     }
    // }

    // // When the mouse exits the collider
    // private void OnMouseExit()
    // {
    //     foreach (var e in onMouseExit)
    //     {
    //         e.Invoke();
    //     }
    // }

    // // Every frame the mouse is over the collider
    // private void OnMouseOver()
    // {
    //     foreach (var e in onMouseOver)
    //     {
    //         e.Invoke();
    //     }
    // }

    public void SetTextColor(Color color)
    {
        text.color = color;
    }

    public void LinkToText(string linkId, string linkText, int linkIndex)
    {
        if (!interactable) return;
        // Debug.Log("Called link text");
        if (linkedObjects == null) return;
        // Debug.Log("Link objects valid");
        if (linkedObjects.Count <= linkIndex) return;
        
        if (linkedObjects[linkIndex] == null) return;
        // Debug.Log("Linked index valid");
        // Debug.Log("Link to " + linkedObjects[linkIndex].name);

        linkedObjects[linkIndex].SetActive(true);
        MovePlayer();
        if (disableOnClick) 
        {
            text.DOFade(0f, disableFadeDuration).OnComplete(() => 
            {
                gameObject.SetActive(false);
            });
        }
    }

    public void GoToText(int linkedObjectIndex)
    {
        linkedObjects[linkedObjectIndex].SetActive(true);
        gameObject.SetActive(false);
    }

    public void DisableInteraction()
    {
        interactable = false;
        text.color = Color.black;
    }

    IEnumerator TimerCountdown()
    {
        yield return new WaitForSeconds(timerDuration);
        foreach (var e in onTimerEnd)
        {
            e.Invoke();
        }
    }

    public void MoveTo(Transform dest)
    {
        transform.DOMove(dest.position, textMoveDuration);
    }

    public void MovePlayer()
    {
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
}
