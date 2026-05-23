using System.Collections;
using UnityEngine;

namespace Member.KimJoonYoung._01.Scripts.TestEnemy
{
    public class TestEnemySpawn : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        public static TestEnemySpawn Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Spawn();
        }

        public void Spawn()
        {
            StartCoroutine(WaitSpawn());
        }
        
        IEnumerator WaitSpawn()
        {
            yield return new WaitForSeconds(2f);
            Instantiate(prefab, transform);
        }

        private void OnDestroy()
        {
            StopCoroutine(WaitSpawn());
        }
    }
}
