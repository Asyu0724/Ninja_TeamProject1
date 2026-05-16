using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Member.KimJoonYoung._01.Scripts.UI.Portal
{
    public class PortalButtonOnOffMotion : MonoBehaviour
    {
        [SerializeField] private GameObject onButton;
        

        private void OnEnable()
        {
            StartCoroutine(OnMotion());
        }

        IEnumerator OnMotion()
        {
            onButton.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(OffMotion());

        }

        IEnumerator OffMotion()
        {
            onButton.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(OnMotion());
        }
    }
}