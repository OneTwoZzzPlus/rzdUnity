using Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class RootController : MonoBehaviour
{
    public static RootController Instance;

    [SerializeField] private FakeTargetTracker FakeTargetTracker;
    [SerializeField] private RealTargetTracker realTargetTracker;

    public ITargetTracker TargetTracker
    {
        get
        {
#if UNITY_EDITOR
            return FakeTargetTracker as ITargetTracker;
#else
            return releaseTargetTracker;
#endif
        }
    }

    private void Awake()
    {
       var rootControllers = GetComponentsInChildren<RootController>();
        if (rootControllers.Length > 1 && rootControllers[0] != this)
        {
            Destroy(gameObject);
        }

        Instance = this;


    }
}
