using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTeleportation : MonoBehaviour
{
    public Transform newPosition;
    public GameObject player;
    private static bool inside = false;

    private void OnCollisionEnter(Collision collision)
    {
        //GameObject player = GameObject.Find("Player");
        //if (!player)
        //player = FindObjectOfType<PlayerMovement>().gameObject;


        if (!player)
            player = GameObject.Find("Player");

        if (!player)
            player = GameObject.FindObjectOfType<PlayerMovement>().gameObject;

        //pm.controller.Move(newPosition.position);

        //Debug.Log("Collided with ", collision.collider.gameObject.transform.parent.gameObject);
        //if (collision.collider.gameObject != player || collision.collider.gameObject.transform.parent.gameObject != player)
        //    return;


        PlayerMovement pm = player.GetComponent<PlayerMovement>();
        pm.controller.enabled = false;
        player.transform.position = new Vector3(newPosition.position.x, newPosition.position.y, newPosition.position.z);
        if(!inside)
        {
            player.transform.localScale = new Vector3(player.transform.localScale.x, 1.0f, player.transform.localScale.z);
            pm.speed /= 2;
            inside = true;
        }
        else
        {
            player.transform.localScale = new Vector3(player.transform.localScale.x, 1.5f, player.transform.localScale.z);
            pm.speed *= 2;
            inside = false;
        }
        pm.controller.enabled = true;
        pm.move = Vector3.zero;
        //player.transform.position = newPosition.position;
        //player.transform.position.x = newPosition.position.x;
        //string newPositionString = newPosition.position.ToString();
        //Debug.Log($"Player teleported to {newPositionString}");
    }

}
