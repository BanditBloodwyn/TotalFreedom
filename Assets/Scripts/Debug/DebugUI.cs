using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Debug
{
    public class DebugUI : MonoBehaviour
    {
        public Transform ObjectToTrack;
        public Text chunkCoord;

        [Range(10, 256)]
        public int ChunkSize = 200;

        // Update is called once per frame
        private void Update()
        {
            Vector2 coord = new Vector2(
                Mathf.RoundToInt(ObjectToTrack.position.x / ChunkSize),
                Mathf.RoundToInt(ObjectToTrack.position.z / ChunkSize));

            chunkCoord.text = $"{ObjectToTrack.gameObject.name}: {coord}";
        }
    }
}
