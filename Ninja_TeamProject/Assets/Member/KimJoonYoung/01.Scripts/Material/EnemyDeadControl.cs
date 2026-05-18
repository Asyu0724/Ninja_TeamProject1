using System;
using System.Collections;
using Member.KimJoonYoung._01.Scripts.TestEnemy;
using UnityEngine;

namespace Member.KimJoonYoung._01.Scripts.Material
{
   public class EnemyDeadControl : MonoBehaviour
   {
      [SerializeField] private UnityEngine.Material material;
      private HealthSystem _healthSystem;
      private readonly int _splitValue = Shader.PropertyToID("_SplitValue");
      private float s;

      private void Awake()
      {
         _healthSystem = GetComponent<HealthSystem>();
      }

      private void Start()
      {
         StartCoroutine(ReviveValueChange());
         _healthSystem.Dead += Dead;
      }

      IEnumerator ReviveValueChange()
      {
         material.SetFloat(_splitValue , 0);
         while (s <= 1)
         {
            material.SetFloat(_splitValue , s);
            s+=Time.deltaTime;
            yield return null;
         }
      }

      private void Dead()
      {
         StartCoroutine(ValueChange());
      }

      IEnumerator ValueChange()
      {
         while (s >= 0)
         {
            material.SetFloat(_splitValue , s);
            s-=Time.deltaTime;
            yield return null;
         }
         TestEnemySpawn.Instance?.Spawn();
         Destroy(gameObject);
      }
   }
}
