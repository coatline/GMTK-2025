using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LaptopCursor : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] RectTransform canvasRect;
    [SerializeField] RectTransform cursor;
    [SerializeField] GraphicRaycaster raycaster;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] float speed = 1000f;
    [SerializeField] RectTransform point;

    Vector2 currentPos;
    bool mouseDown;

    void Start()
    {
        currentPos = canvasRect.rect.center;
    }

    void Update()
    {
        RegisterInput();
    }

    public void Move(Vector2 delta)
    {
        delta *= speed;
        currentPos += delta;

        // Clamp cursor inside canvas
        currentPos.x = Mathf.Clamp(currentPos.x, -canvasRect.rect.width / 2f, canvasRect.rect.width / 2f);
        currentPos.y = Mathf.Clamp(currentPos.y, -canvasRect.rect.height / 2f, canvasRect.rect.height / 2f);

        cursor.anchoredPosition = currentPos;
    }

    void RegisterInput()
    {
        // Create pointer data
        PointerEventData pointerData = new PointerEventData(eventSystem);
        Vector2 screenPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            point.position,
            canvas.worldCamera,
            out screenPoint
        );
        pointerData.position = canvas.worldCamera.WorldToScreenPoint(point.position);

        // Raycast
        var results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        foreach (var result in results)
        {
            ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.pointerEnterHandler);

            if (mouseDown)
                ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.pointerClickHandler);
        }

        mouseDown = false;
    }

    public void SetMouseDown() => this.mouseDown = true;
}