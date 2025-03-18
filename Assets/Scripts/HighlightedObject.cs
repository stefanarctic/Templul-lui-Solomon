using UnityEngine;

public class HighlightedObject : MonoBehaviour
{
    [Header("Highlight Settings")]

    public QuickOutline.Outline.Mode outlineMode = QuickOutline.Outline.Mode.OutlineVisible;

    public Color outlineColor = Color.white;

    [Range(0f, 10f)]
    public float outlineWidth = 10f;
}
