using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] private Transform _playerTrm;
    private Vector3 mousePos;
    private Vector3 current;
    private bool _cameraPivot;

    private void FixedUpdate()
    {
            transform.position = _playerTrm.position;
    }
}
