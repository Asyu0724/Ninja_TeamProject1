using System;
using UnityEngine;

public class CJY_CameraManager : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private BossMover bossMove;
    
    private int _isShake = Animator.StringToHash(name: "IsShake");
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool(_isShake, bossMove.IsShake);
    }
}
