using System.Collections;
using UnityEngine;

public class Min_AttackPreView : MonoBehaviour
{
    public void AttackPreView()
    {
        if (gameObject.tag == "Attack1")
        {
            gameObject.SetActive(true);
        }

        if (gameObject.tag == "Attack2")
        {
            gameObject.SetActive(true);
        }
        if (gameObject.tag == "Attack3")
        {
            gameObject.SetActive(true);
        }

        StartCoroutine(Previewtime());
    }

    private IEnumerator Previewtime()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
