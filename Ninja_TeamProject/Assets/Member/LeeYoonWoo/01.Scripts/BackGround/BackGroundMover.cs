using UnityEngine;

public class BackGroundMover : MonoBehaviour
{
    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        transform.position = startPos +
            new Vector3(Mathf.Sin(Time.time * 0.2f) * 0.05f, 0, 0);
    }
}
