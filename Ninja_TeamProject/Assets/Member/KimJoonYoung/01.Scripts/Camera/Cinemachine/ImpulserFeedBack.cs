using System;
using Unity.Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.KimJoonYoung._01.Scripts.Camera.Cinemachine
{
    public class ImpulserFeedBack : MonoBehaviour
    {
        [SerializeField] private float sec;
        [SerializeField] private Vector3 velocityRange1;
        [SerializeField] private Vector3 velocityRange2;
        [SerializeField] private CinemachineImpulseSource impulser;

        public void Impulse()
        {
            impulser.DefaultVelocity = new Vector3(
                Random.Range(velocityRange1.x, velocityRange2.x)
                , Random.Range(velocityRange1.y, velocityRange2.y)
                , Random.Range(velocityRange1.z, velocityRange2.z));
            
            impulser.ImpulseDefinition.ImpulseShape = CinemachineImpulseDefinition.ImpulseShapes.Bump;
            impulser.ImpulseDefinition.ImpactRadius = sec;
            impulser.GenerateImpulse();
        }
    }
}
