using UnityEngine;

public class LaptopCursor : MonoBehaviour
{
    public RectTransform cursor;
    public RectTransform canvasRect;
    public float speed = 1000f;

    private Vector2 currentPos;

    void Start()
    {
        currentPos = canvasRect.rect.center;
    }

    void Update()
    {
        Vector2 move = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * speed * Time.deltaTime;
        currentPos += move;

        // Clamp cursor inside canvas
        currentPos.x = Mathf.Clamp(currentPos.x, -canvasRect.rect.width / 2f, canvasRect.rect.width / 2f);
        currentPos.y = Mathf.Clamp(currentPos.y, -canvasRect.rect.height / 2f, canvasRect.rect.height / 2f);

        cursor.anchoredPosition = currentPos;
    }
}