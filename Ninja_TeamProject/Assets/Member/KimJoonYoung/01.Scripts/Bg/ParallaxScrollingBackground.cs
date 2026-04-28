using System;
using Unity.Cinemachine;
using UnityEditor.Search;
using UnityEngine;

public class ParallaxScrollingBackground : MonoBehaviour
{
    [SerializeField] private float bgSpeed;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    
    private void FixedUpdate()
    {
            _rb.linearVelocityX = GameManager.Instance.player.Speed * bgSpeed;
    }
}