using System;
using Member.KimJoonYoung._01.Scripts.SO;
using UnityEngine;
using UnityEngine.Events;

public class OnStart : MonoBehaviour
{
    public UnityEvent onStart;
    
    private void Start()
    {
        onStart?.Invoke();
    }
}
