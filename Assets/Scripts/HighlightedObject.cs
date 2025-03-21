using UnityEngine;

public class HighlightedObject : MonoBehaviour
{
    public Transform centerTransform;

    [Header("Highlight Settings")]

    public QuickOutline.Outline.Mode outlineMode = QuickOutline.Outline.Mode.OutlineVisible;

    public Color outlineColor = Color.white;

    [Range(0f, 10f)]
    public float outlineWidth = 10f;

    [Header("Rotating settings")]

    public float cameraDistance = 5f;
    public float cameraHeight = 5f;
    public float cameraSpeed = 2.0f;

}
