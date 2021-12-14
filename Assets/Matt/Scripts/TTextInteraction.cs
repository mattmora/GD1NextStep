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

    TTextColor textColor;

    public bool interactable = true;

    public int numUses = 1;

    public bool disableOnClick = true;
    public float disableFadeDuration = 0f;

    public float enableFadeAlpha = 1f;
    public float enableFadeDuration = 0f;

    public float enableDelay;

    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<TPlayerController>();
        text = GetComponent<TMP_Text>();
        boxCollider = GetComponent<BoxCollider>();
        textColor = GetComponent<TTextColor>();
    }

    private void OnEnable() 
    {
        if (enableFadeAlpha != text.alpha || enableFadeDuration != 0f || enableDelay != 0f)
        {
            text.alpha = 0f;
            text.DOFade(enableFadeAlpha, enableFadeDuration).SetDelay(enableDelay).OnComplete(() =>
            {
                if (useTimer)
                {
                    StartCoroutine(TimerCountdown());
                }
            });
        }
        else
        {
            text.alpha = 1f;
            if (useTimer) StartCoroutine(TimerCountdown());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void SetTextColor(Color color)
    {
        text.color = color;
    }

    public void FadeOutAndDisable()
    {
        // Debug.Log("Start fade out");
        text.DOFade(0f, disableFadeDuration).OnComplete(() => 
        {
            // Debug.Log("End fade out");
            gameObject.SetActive(false);
        });   
    }

    public void GoToText(int linkIndex)
    {
        if (linkedObjects != null && linkedObjects.Count > linkIndex && linkedObjects[linkIndex] != null)
        {
            // Debug.Log("Enabled " + linkedObjects[linkIndex].name);
            linkedObjects[linkIndex].SetActive(true);
        }

        if (numUses == 0)
        {
            textColor.SetUsed(true);
        }
        
        if (disableOnClick) 
        {
            FadeOutAndDisable();
        }
    }

    public void LinkToText(string linkId, string linkText, int linkIndex)
    {
        // Debug.Log("link");
        if (!interactable) return;

        if (numUses == 0) return;

        numUses--;

        // Debug.Log("Called link text");

        MovePlayer();

        GoToText(linkIndex);
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
