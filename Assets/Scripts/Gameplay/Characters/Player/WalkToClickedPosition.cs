using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Characters.Player
{
    public class WalkToClickedPosition : MonoBehaviour
    {
        [Header("Movement")]
        public float Speed;
        public float SmoothTime;
        public float TurnSpeed;

        public Vector3 Target
        {
            get => target;
            set
            {
                target = value;
                
                StopCoroutine("Movement");
                StartCoroutine("Movement", target);
            }
        }

        private Vector3 target;
        private CharacterController characterController;
        private float smoothSpeed;
        private float smoothSpeedVelocity;
        private float currentAngle;

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
        }

        // ReSharper disable once UnusedMember.Local because this is called via String in StartCoroutine
        private IEnumerator Movement(Vector3 newTarget)
        {
            
            while (Vector3.Distance(transform.position, newTarget) > 0.1f)
            {
                Vector3 position = transform.position;

                Vector3 offset = newTarget - position;
                smoothSpeed = Mathf.SmoothDamp(smoothSpeed, Mathf.Clamp(offset.magnitude, -Speed, Speed), ref smoothSpeedVelocity, SmoothTime);

                float targetAngle = Mathf.Atan2(target.x - position.x, target.z - position.z) * Mathf.Rad2Deg;
                currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, TurnSpeed * Time.deltaTime);

                transform.eulerAngles = Vector3.up * currentAngle;
                characterController.SimpleMove(offset.normalized * smoothSpeed);

                yield return null;
            }

            smoothSpeed = 0;
        }
    }
}
