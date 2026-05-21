using System.Collections;
using UnityEngine;

public class BurnDissolveController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer targetRenderer;
    [SerializeField] private Material burnDissolveMaterial;
    [SerializeField] private float duration = 1.2f;

    private Material runtimeMaterial;

    private static readonly int DissolveAmountHash = Shader.PropertyToID("_DissolveAmount");

    private void Awake()
    {
        if (targetRenderer == null)
        {
            targetRenderer = GetComponent<SpriteRenderer>();
        }

        if (targetRenderer != null && burnDissolveMaterial != null)
        {
            runtimeMaterial = Instantiate(burnDissolveMaterial);
            runtimeMaterial.SetFloat(DissolveAmountHash, 0f);
            targetRenderer.material = runtimeMaterial;
        }
    }

    public void Play()
    {
        StopAllCoroutines();
        StartCoroutine(DissolveRoutine());
    }

    private IEnumerator DissolveRoutine()
    {
        if (runtimeMaterial == null)
        {
            yield break;
        }

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            runtimeMaterial.SetFloat(DissolveAmountHash, Mathf.Clamp01(time / duration));
            yield return null;
        }

        runtimeMaterial.SetFloat(DissolveAmountHash, 1f);
    }
}
