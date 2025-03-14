using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator animator1;
    public Animator animator2;

    private void OnTriggerEnter(Collider other)
    {
        animator1.SetBool("isOpen", true);
        animator2.SetBool("isOpen", true);
    }

    private void OnTriggerExit(Collider other)
    {
        animator1.SetBool("isOpen", false);
        animator2.SetBool("isOpen", false);
    }
}
