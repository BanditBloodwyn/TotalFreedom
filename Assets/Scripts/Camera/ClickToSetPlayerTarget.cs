using Assets.Scripts.Gameplay.Characters.Player;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class ClickToSetPlayerTarget : MonoBehaviour
    {
        public WalkToClickedPosition walkingScript;

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
                SetWalkingTarget();
        }

        private void SetWalkingTarget()
        {
            if (UnityEngine.Camera.main == null)
                return;

            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out RaycastHit hit);

            if (hit.collider.gameObject.CompareTag("Walkable"))
            {
                Vector3 newTarget = hit.point + new Vector3(0, 1, 0);
                walkingScript.Target = newTarget;
            }
        }
    }
}
