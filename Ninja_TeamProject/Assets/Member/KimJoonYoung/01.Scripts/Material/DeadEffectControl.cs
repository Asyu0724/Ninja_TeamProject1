using System;
using System.Collections;
using UnityEngine;

namespace Member.KimJoonYoung._01.Scripts.Material
{
   public class DeadEffectControl : MonoBehaviour
   {
      [SerializeField] private UnityEngine.Material material;
      private HealthSystem _healthSystem;
      private readonly int _splitValue = Shader.PropertyToID("_SplitValue");
      private float s = 1;

      private void Awake()
      {
         _healthSystem = GetComponent<HealthSystem>();
         material.SetFloat(_splitValue, s);
      }

      private void Start()
      {
         _healthSystem.Dead += Dead;
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
         Destroy(gameObject);
      }

      private void OnDestroy()
      {
         material.SetFloat(_splitValue , 1);
      }
   }
}
