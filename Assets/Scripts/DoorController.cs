using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public Transform door; // Assign the door in the Inspector
    public float openAngle = 90f; // Adjust as needed
    public float speed = 2f;
    private bool isOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Press 'E' to open/close
        {
            isOpen = !isOpen;
            float targetAngle = isOpen ? openAngle : 0f;
            StartCoroutine(RotateDoor(targetAngle));
        }
    }

    IEnumerator RotateDoor(float targetAngle)
    {
        Quaternion startRotation = door.rotation;
        Quaternion endRotation = Quaternion.Euler(startRotation.x, startRotation.y, startRotation.z + targetAngle);
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime * speed;
            door.rotation = Quaternion.Lerp(startRotation, endRotation, time);
            yield return null;
        }
    }
}
