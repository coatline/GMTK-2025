using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class LaptopUIClicker : MonoBehaviour
{
    public RectTransform cursor;
    public Canvas canvas;
    public EventSystem eventSystem;

    private GraphicRaycaster raycaster;

    void Start()
    {
        raycaster = canvas.GetComponent<GraphicRaycaster>();
    }

    void Update()
    {
        // Create pointer data
        PointerEventData pointerData = new PointerEventData(eventSystem);
        Vector2 screenPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            cursor.position,
            canvas.worldCamera,
            out screenPoint
        );
        pointerData.position = canvas.worldCamera.WorldToScreenPoint(cursor.position);

        // Raycast
        var results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        foreach (var result in results)
        {
            ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.pointerEnterHandler);
            if (Input.GetMouseButtonDown(0))
            {
                ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.pointerClickHandler);
            }
        }
    }
}
