using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : StaticInstance<DragDrop>
{
    public GameObject Canvas;

    private bool isDragging = false;
    private GameObject startParent;
    private Vector2 startPosition;
    private bool isOverDropZone;

    // FIXME:
    private ItemType itemType = ItemType.Bomb;

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

    private IEnumerator UseCard()
    {
        yield return new WaitForSeconds(2);
        GUIManager.Instance.ShowDropZone(false);

        // TODO: 這邊要吃卡片的類型
        switch (itemType)
        {
            case ItemType.Bomb:
                UnitManager.Instance.SpawnBomb();
                break;
            default:
                break;
        }

        Destroy(gameObject);
    }


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
