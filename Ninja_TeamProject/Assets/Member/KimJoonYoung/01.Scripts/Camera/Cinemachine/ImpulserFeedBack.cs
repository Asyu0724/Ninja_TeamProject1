using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Member.KimJoonYoung._01.Scripts.Camera.Cinemachine
{
    public class ImpulserFeedBack : MonoBehaviour
    {
        [SerializeField] private Vector3 velocity;
        [SerializeField] private CinemachineImpulseSource impulser;

        public void Impulse()
        {
            impulser.GenerateImpulse(velocity);
        }
    }
}
