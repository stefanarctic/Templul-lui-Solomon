using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public GameObject menuObject;

    public virtual void OnStart() { }
    public virtual void OnAwake() { }
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }

    private void Start()
    {
        OnStart();
    }

    private void Awake()
    {
        OnAwake();
    }

    private void Update()
    {
        OnUpdate();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate();
    }

    public virtual void Show() { }
    public virtual void Hide() { }

}
