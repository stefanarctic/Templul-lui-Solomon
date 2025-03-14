using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //[HideInInspector]
    //public GameObject player;

    private static GameManager Instance = null;
    public static GameManager instance
    {
        get
        {
            if (Instance == null)
                Instance = FindObjectOfType<GameManager>();
            return Instance;
        }
    }

    private void Start()
    {
        //player = FindObjectOfType<PlayerMovement>().gameObject;
        //PlayerMovement pm = FindObjectOfType<PlayerMovement>();
        //player = pm == null ? pm.gameObject : null;
    }

}
