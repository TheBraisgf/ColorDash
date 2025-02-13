using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic; // ✅ Agregar esta línea para usar List<>
using UnityEngine.UI;

public class ClickDebugger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Click en PC
        {
            DetectUIClick(Input.mousePosition);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) // Toque en Android
        {
            DetectUIClick(Input.GetTouch(0).position);
        }
    }

    void DetectUIClick(Vector2 screenPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = screenPosition;

        List<RaycastResult> results = new List<RaycastResult>(); // ✅ Ya no dará error
        EventSystem.current.RaycastAll(eventData, results);

        if (results.Count > 0)
        {
            foreach (RaycastResult result in results)
            {
                Debug.Log($"✅ Click en UI: {result.gameObject.name}");
            }
        }
        else
        {
            Debug.Log("❌ No se hizo click en ningún objeto de UI.");
        }
    }
}
