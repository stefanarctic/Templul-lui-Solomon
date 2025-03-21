using UnityEngine;
using QuickOutline;
using System.Collections;
using Unity.VisualScripting;

public class HighlightScript : MonoBehaviour
{
    public string highlightObjectTag = "Highlight";
    public float raycastDistance = 8f;
    public Transform cameraTransform;
    public GameObject crossHair;
    public Camera playerCamera;
    public float transitionSpeed = 1f;

    private bool objectViewEnabled = false;

    public Outline outlineTemplate;

    private GameObject currentlyHighlightedObject = null;
    private GameObject currentlyFocusedObject;
    private Camera highlightCamera;

    private void Start()
    {
        if (!cameraTransform)
            cameraTransform = GetComponentInChildren<Camera>().transform;

        crossHair.SetActive(false);
    }

    private void Update()
    {
        RaycastHit hit;
        Vector3 origin = cameraTransform.position;
        Vector3 direction = cameraTransform.forward;

        int layerMask = ~LayerMask.GetMask("Player", "Particle");

        if (!objectViewEnabled && Physics.Raycast(origin, direction, out hit, raycastDistance, layerMask))
        {
            //print($"Hit object {hit.collider.name}");

            GameObject hitObject = hit.collider.gameObject;

            //GameObject parentGameObject = GetRootParent(hitObject);

            HighlightedObject highlightedObjectComponent = FindHighlightedObject(hitObject);
            if (highlightedObjectComponent == null)
            {
                crossHair.SetActive(false);
                return;
            }

            //print($"Highlighted object of {hitObject.name} is {highlightedObjectComponent.name}");

            crossHair.SetActive(true);


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
                //RemoveOutlineFromObject(currentlyHighlightedObject);
                //AddOutlineToObject(highlightedObject);
                currentlyHighlightedObject = highlightedObject;
            }

            // If player left clicks transition to object view
            if(Input.GetMouseButtonDown(0))
            {
                EnableObjectView(highlightedObject);
            }
        }
        else
        {
            //RemoveOutlineFromObject(currentlyHighlightedObject);
            currentlyHighlightedObject = null;
            crossHair.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(objectViewEnabled)
                ExitObjectView();
        }
    }

    public void EnableObjectView(GameObject obj)
    {
        print($"Enabling object view on {obj.name}");
        objectViewEnabled = true;
        currentlyFocusedObject = obj;
        //Camera newCamera = Instantiate(playerCamera);
        HighlightedObject objComponent = obj.GetComponent<HighlightedObject>();
        GameObject cameraObject = Instantiate(playerCamera.gameObject);
        cameraObject.name = "ObjectViewCamera";
        Vector3 cameraPosition = playerCamera.gameObject.transform.position;
        cameraObject.transform.position = new Vector3(cameraPosition.x, cameraPosition.y + objComponent.cameraHeight, cameraPosition.z);
        Destroy(cameraObject.GetComponent<MouseLook>());

        Camera newCamera = cameraObject.GetComponent<Camera>();
        highlightCamera = newCamera;
        playerCamera.gameObject.SetActive(false);

        // Disable player movement
        PlayerMovement playerMovement = playerCamera.transform.parent.gameObject.GetComponent<PlayerMovement>();
        playerMovement.enabled = false;

        //newCamera.transform.LookAt(obj.transform.GetComponent<Renderer>().bounds.center);

        // For now it behaves like a simple function
        StartCoroutine(StartObjectTransition(newCamera, obj, objComponent));
    }

    public void ExitObjectView()
    {
        print($"Disabling object view on {currentlyFocusedObject.name}");
        objectViewEnabled = false;
        RemoveOutlineFromObject(currentlyFocusedObject);
        currentlyFocusedObject = null;

        Destroy(highlightCamera.gameObject);
        playerCamera.gameObject.SetActive(true);

        // Enable player movement
        PlayerMovement playerMovement = playerCamera.transform.parent.gameObject.GetComponent<PlayerMovement>();
        playerMovement.enabled = true;

        highlightCamera = null;
    }

    public IEnumerator StartObjectTransition(Camera newCamera, GameObject obj, HighlightedObject objComponent)
    {
        Vector3 centerPosition = Vector3.zero;
        // If centerTransform is null get the center of the model
        if (objComponent.centerTransform != null)
            centerPosition = objComponent.centerTransform.position;
        else
        {
            Renderer objectRenderer = obj.GetComponent<Renderer>();
            if (!objectRenderer)
                objectRenderer = obj.GetComponentInChildren<Renderer>();

            Vector3 modelCenter = objectRenderer.bounds.center;
            centerPosition = modelCenter;
        }

        print($"Initial distance {Vector3.Distance(newCamera.transform.position, centerPosition)}");

        newCamera.transform.LookAt(centerPosition);
        CameraRotate cameraRotate = newCamera.gameObject.AddComponent<CameraRotate>();
        cameraRotate.amp = objComponent.cameraSpeed;
        cameraRotate.TargetPosition = centerPosition;

        AddOutlineToObject(obj);
        // Maybe in the future add smooth moving to position

        while (Vector3.Distance(newCamera.transform.position, centerPosition) > objComponent.cameraDistance)
        {
            newCamera.transform.Translate(Vector3.forward * transitionSpeed * Time.deltaTime);
            //yield return new WaitForSeconds(transitionSpeed * 0.1f);
        }

        print($"Focused on {obj.name}");

        yield return null;
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
            if (childGameObject.CompareTag("Particle") || childGameObject == obj || childGameObject.GetComponent<Outline>())
                continue;

            HighlightedObject highlightedObjectComponent = obj.GetComponent<HighlightedObject>();
            Outline childOutline = childGameObject.AddComponent<Outline>();
            childOutline.OutlineMode = highlightedObjectComponent.outlineMode;
            childOutline.OutlineColor = highlightedObjectComponent.outlineColor;
            childOutline.OutlineWidth = highlightedObjectComponent.outlineWidth;
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
