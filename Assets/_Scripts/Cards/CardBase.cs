using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CardBase : MonoBehaviour
{
    public string CardName;
    public string Brief;

    private GameObject Canvas;
    private bool isDragging = false;
    private GameObject startParent;
    private Vector2 startPosition;
    private bool isOverDropZone;

    private void Start()
    {
        Canvas = GameObject.Find("Main Canvas");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "DropZone")
        {
            isOverDropZone = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "DropZone")
        {
            isOverDropZone = false;
        }
    }

    public void StartDrag()
    {
        GUIManager.Instance.ShowDropZone(true);
        isDragging = true;
        startParent = transform.parent.gameObject;
        startPosition = transform.position;
    }

    public void EndDrag()
    {
        isDragging = false;
        if (isOverDropZone)
        {
            StartCoroutine(UseCard());
            transform.SetParent(GUIManager.Instance.DropZone.transform, false);
        }
        else
        {
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
        }
    }

    public IEnumerator UseCard()
    {
        yield return new WaitForSeconds(2);
        GUIManager.Instance.ShowDropZone(false);
        ExecuteCard();
        Destroy(gameObject);
    }

    // °õ¦æ¥d¤ù
    public abstract void ExecuteCard();

    private void Update()
    {
        if (isDragging)
        {
            Vector3 screenPos = Input.mousePosition;
            screenPos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPos);
            transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
            transform.SetParent(Canvas.transform, true);
        }
    }
}
