using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropManager : StaticInstance<DragDropManager>
{
    public bool isDragging = false;

    public void StartDrag()
    {
        isDragging = true;
    }

    public void EndDrag()
    {
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 screenPos = Input.mousePosition;
            screenPos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPos);
            transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
        }
    }
}
