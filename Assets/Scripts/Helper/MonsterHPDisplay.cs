using UnityEngine;
using TMPro;

[RequireComponent(typeof(Monsters))]
public class MonsterHPDisplay : MonoBehaviour
{
    public TextMeshProUGUI hpText;      // 2D TMP text on Canvas
    public RectTransform canvasRect;    // Canvas RectTransform
    private float yOffset = 0.5f;        // Height above monster

    private IHasHealth healthEntity;

    void Awake()
    {
        healthEntity = GetComponent<IHasHealth>();
        if (hpText == null)
            Debug.LogWarning("HP Text not assigned. Please assign a TextMeshProUGUI object.");
        if (canvasRect == null)
            Debug.LogWarning("Canvas RectTransform not assigned.");
    }

    void LateUpdate()
    {
        if (healthEntity == null || hpText == null || canvasRect == null) return;

        // Update HP text
        hpText.text = healthEntity.Health.ToString();

        // World position above monster
        Vector3 worldPos = transform.position + Vector3.up * yOffset;

        // Convert world position to screen point
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPos);

        // Convert screen point to canvas local position
        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, null, out canvasPos);

        // Apply to the TMP text
        hpText.rectTransform.localPosition = canvasPos;
    }
}
