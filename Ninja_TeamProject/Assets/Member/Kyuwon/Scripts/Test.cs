using UnityEngine;
using UnityEngine.InputSystem;

namespace Member.Kyuwon.Scripts
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        private Rigidbody2D _rigid;
        private Vector2 _moveDir;
        [SerializeField] private float jumpPower = 10f;


        private void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _rigid.linearVelocityX = _moveDir.x * speed;
            if(Keyboard.current.spaceKey.wasPressedThisFrame)
            {

                _rigid.linearVelocityY = jumpPower;
            }
        }

        private void OnMove(InputValue value)
        {
            _moveDir = value.Get<Vector2>();   
        }

    }
}
