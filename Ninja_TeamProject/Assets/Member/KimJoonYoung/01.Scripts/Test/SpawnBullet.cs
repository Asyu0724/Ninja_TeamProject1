using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private float timer = 2f;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            GameObject bul = Instantiate(bullet);
            bul.transform.position = transform.position;
            timer = 2f;
        }
    }
}
