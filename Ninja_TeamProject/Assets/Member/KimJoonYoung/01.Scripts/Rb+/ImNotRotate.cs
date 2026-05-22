using System;
using UnityEngine;

public class ImNotRotate : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0,0,0);
    }
}
