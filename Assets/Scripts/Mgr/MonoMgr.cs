using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoMgr : SingletonMono<MonoMgr>
{
    public UnityAction updateAction;
    public UnityAction fixedUpdateAction;


    public void AddUpdateAction(UnityAction action)
    {
        updateAction += action;
    }

    public void RemoveUpdateAcion(UnityAction action)
    {
        updateAction -= action;
    }

    public void AddFixedUpdateAction(UnityAction action)
    {
        fixedUpdateAction += action;
    }

    public void RemoveFixedUpdateAction(UnityAction action)
    {
        fixedUpdateAction -= action;
    }



    void Update()
    {
        updateAction?.Invoke();
    }

    void FixedUpdate()
    {
        fixedUpdateAction?.Invoke();
    }
}
