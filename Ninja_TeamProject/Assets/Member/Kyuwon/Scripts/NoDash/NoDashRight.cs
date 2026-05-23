using System;
using UnityEngine;

public class NoDashRight : MonoBehaviour
{
    [SerializeField] private bool isFacingRight;
    private bool touched = false;
    private bool NoDash = false;

    private void FixedUpdate()
    {
        isFacingRight = transform.rotation.y == 0 ? true : false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        touched = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        touched = false;
    }
    
    private void Update()
    {
        if (isFacingRight == true && touched == true)
        {
            NoDash = true;
        }
        else
        {
            NoDash = false;
        }
    }
}
