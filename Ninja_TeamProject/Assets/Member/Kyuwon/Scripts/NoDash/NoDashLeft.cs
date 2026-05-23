using System;
using UnityEngine;

public class NoDashLeft : MonoBehaviour
{
    [SerializeField] private bool isFacingLeft;
    private bool touched = false;
    private bool NoDash = false;

    private void FixedUpdate()
    {
        isFacingLeft = transform.rotation.y != 0 ? true : false;
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
        if (isFacingLeft == true && touched == true)
        {
            NoDash = true;
        }
        else
        {
            NoDash = false;
        }
    }
}
