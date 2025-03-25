using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JerusalemScript : MonoBehaviour
{
    public Transform parent;
    public new Camera camera;
    public GameObject transitionWall;

    public float sizeMultiplier = 1.5f;
    public float cameraSpeed = 1.5f;
    public float stoppingTime = 0.08f;
    public float transitionSpeed = 15f;

    private bool isZooming = false;
    private float t = 0f;

    private void Start()
    {
        if (!parent)
            parent = transform.parent;

        if (!camera)
            camera = FindObjectOfType<Camera>();

        transitionWall.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!isZooming)
            return;

        t += Time.fixedDeltaTime * cameraSpeed;
        camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position, t);
        print(t);
        if (t >= stoppingTime)
        {
            isZooming = false;
            print("Stopped");
            SceneManager.instance.NextSceneAsync();
        }
    }

    private void OnMouseEnter()
    {
        print("Mouse entered Jerusalem");
        parent.localScale = new Vector3(parent.localScale.x, parent.localScale.y * sizeMultiplier, parent.localScale.z * sizeMultiplier);
    }

    private void OnMouseExit()
    {
        print("Mouse exited Jerusalem");
        parent.localScale = new Vector3(parent.localScale.x, parent.localScale.y / sizeMultiplier, parent.localScale.z / sizeMultiplier);
    }

    private void OnMouseDown()
    {
        //camera.transform.LookAt(gameObject.transform.position);
        isZooming = true;
        transitionWall.SetActive(true);
        TransitionManager.instance.TransitionShow(transitionWall, transitionSpeed);
    }

}
