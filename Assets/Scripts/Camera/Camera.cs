using Assets.Scripts.Framework.GameManagement.UI;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class Camera : MonoBehaviour
    {
        private readonly CameraState m_TargetCameraState = new CameraState();
        private readonly CameraState m_InterpolatingCameraState = new CameraState();
        private float currentHeight = 5;
        private float lastDetectedTerrainHeight;


        [Header("Movement Settings")]

        [Tooltip(""), Range(1f, 5f)]
        public float MovingSpeed = 4f;
        
        [Tooltip(""), Range(0.001f, 1f)]
        public float ScrollSpeed = 0.5f;
        
        [Tooltip(""), Range(5f, 20f)]
        public float MinimumHeight = 10f;
        
        [Tooltip(""), Range(5f, 100f)]
        public float MaximumHeight = 20f;

        [Tooltip("Time it takes to interpolate camera position 99% of the way to the target."), Range(0.001f, 1f)]
        public float positionLerpTime = 0.5f;
       
        [Tooltip("Time it takes to interpolate camera rotation 99% of the way to the target."), Range(0.001f, 1f)]
        public float rotationLerpTime = 0.01f;

        // Update is called once per frame
        private void Update()
        {
            // Translation
            Vector3 translation = GetInputTranslationDirection() * Time.deltaTime;
            
            // Speed up movement when shift key held
            if (IsBoostPressed())
            {
                translation *= 5.0f;
            }

            // Modify movement by a boost factor (defined in Inspector and modified in play mode through the mouse scroll wheel)
            translation *= Mathf.Pow(2.0f, MovingSpeed);

            m_TargetCameraState.Translate(translation);
            m_TargetCameraState.y = GetCameraHeight();
            m_TargetCameraState.pitch = 60;

            // Framerate-independent interpolation
            // Calculate the lerp amount, such that we get 99% of the way to our target in the specified time
            var positionLerpPct = 1f - Mathf.Exp(Mathf.Log(1f - 0.99f) / positionLerpTime * Time.deltaTime);
            var rotationLerpPct = 1f - Mathf.Exp(Mathf.Log(1f - 0.99f) / rotationLerpTime * Time.deltaTime);
            m_InterpolatingCameraState.LerpTowards(m_TargetCameraState, positionLerpPct, rotationLerpPct);
            
            m_InterpolatingCameraState.UpdateTransform(transform);
        }

        private float GetCameraHeight()
        {
            if (! UIManager.instance.IsMenuOpen)
            {
                currentHeight -= Input.mouseScrollDelta.y * ScrollSpeed;
                if (currentHeight < MinimumHeight)
                    currentHeight = MinimumHeight;
                if (currentHeight > MaximumHeight)
                    currentHeight = MaximumHeight;
            }
            return currentHeight + GetTerrainHeight();
        }

        private float GetTerrainHeight()
        {
            Ray ray = new Ray(transform.position, Vector3.down);

            Physics.Raycast(ray, out RaycastHit hit);

            if (hit.collider != null && (hit.collider.gameObject.CompareTag("Terrain") || hit.collider.gameObject.CompareTag("Walkable")))
            {
                lastDetectedTerrainHeight = hit.point.y;
                return lastDetectedTerrainHeight;
            }
            return lastDetectedTerrainHeight;
        }

        private Vector3 GetInputTranslationDirection()
        {
            Vector3 direction = Vector3.zero;

            if (UIManager.instance.IsMenuOpen)
                return Vector3.zero;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                direction += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                direction += Vector3.back;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                direction += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                direction += Vector3.right;
            }

            return direction;
        }

        private bool IsBoostPressed()
        {
            return Input.GetKey(KeyCode.LeftShift);
        }
    }
}
