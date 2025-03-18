using UnityEngine;
using QuickOutline;

public class HighlightScript : MonoBehaviour
{
    public string highlightObjectTag = "Highlight";
    public float raycastDistance = 8f;
    public Transform cameraTransform;
    public GameObject crossHair;

    public Outline outlineTemplate;

    [SerializeField]
    private GameObject currentlyHighlightedObject = null;

    private void Start()
    {
        if (!cameraTransform)
            cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    private void Update()
    {
        RaycastHit hit;
        Vector3 origin = cameraTransform.position;
        Vector3 direction = cameraTransform.forward;

        int layerMask = ~LayerMask.GetMask("Player", "Particle");

        if (Physics.Raycast(origin, direction, out hit, raycastDistance, layerMask))
        {
            print($"Hit object {hit.collider.name}");

            GameObject hitObject = hit.collider.gameObject;

            //GameObject parentGameObject = GetRootParent(hitObject);

            HighlightedObject highlightedObjectComponent = FindHighlightedObject(hitObject);
            if (highlightedObjectComponent == null)
                return;
            print($"Highlighted object of {hitObject.name} is {highlightedObjectComponent.name}");


            //GameObject parentGameObject;
            //if (hitObject.transform.parent != null)
            //    parentGameObject = hitObject.transform.parent.gameObject;
            //else
            //    parentGameObject = hitObject;
            //print($"Parent game object of {hitObject.name} is {parentGameObject.name}");
            //if (!parentGameObject.CompareTag(highlightObjectTag))
            //return;

            GameObject highlightedObject = highlightedObjectComponent.gameObject;
            if (currentlyHighlightedObject != highlightedObject)
            {
                RemoveOutlineFromObject(currentlyHighlightedObject);
                AddOutlineToObject(highlightedObject);
                currentlyHighlightedObject = highlightedObject;
            }
        }
        else
        {
            RemoveOutlineFromObject(currentlyHighlightedObject);
            currentlyHighlightedObject = null;
        }
    }

    public GameObject GetRootParent(GameObject obj)
    {
        Transform parentTransform = obj.transform;

        while (parentTransform.parent != null)
        {
            parentTransform = parentTransform.parent;
        }

        return parentTransform.gameObject;
    }

    public void AddOutlineToObject(GameObject obj)
    {
        Transform[] childrenTransform = obj.GetComponentsInChildren<Transform>();
        foreach (Transform childTransform in childrenTransform)
        {
            GameObject childGameObject = childTransform.gameObject;
            if (childGameObject.CompareTag("Particle") || childGameObject == obj)
                continue;

            Outline childOutline = childGameObject.AddComponent<Outline>();
            childOutline.OutlineMode = outlineTemplate.OutlineMode;
            childOutline.OutlineColor = outlineTemplate.OutlineColor;
            childOutline.OutlineWidth = outlineTemplate.OutlineWidth;
        }
    }

    public void RemoveOutlineFromObject(GameObject obj)
    {
        if (obj == null)
            return;

        Transform[] childrenTransform = obj.GetComponentsInChildren<Transform>();
        foreach (Transform childTransform in childrenTransform)
        {
            GameObject childGameObject = childTransform.gameObject;
            Outline outline = childGameObject.GetComponent<Outline>();
            if (outline != null)
            {
                Destroy(outline);
            }
        }
    }

    public bool CheckHighlighted(GameObject obj) => obj.GetComponent<HighlightedObject>() != null;

    public HighlightedObject FindHighlightedObject(GameObject obj)
    {
        // First check if object has component
        HighlightedObject highlightedObject = obj.GetComponent<HighlightedObject>();
        if (highlightedObject != null)
            return highlightedObject;

        // If not, search for it in the parents hierarchy
        Transform objectTransform = obj.transform;
        while (objectTransform.parent != null)
        {
            objectTransform = objectTransform.parent;
            highlightedObject = objectTransform.gameObject.GetComponent<HighlightedObject>();
            if (highlightedObject != null)
                return highlightedObject;
        }

        return null;
    }
}
