using Unity.VisualScripting;
using UnityEngine;

namespace Member.KimJoonYoung._01.Scripts.Interface
{
    public interface ICollisionAttackable
    {
        public void OnCollisionEnter2D(Collision2D collision);
    }
}