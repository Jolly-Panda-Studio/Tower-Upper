using UnityEngine;

namespace JollyPanda.LastFlag.EnviromentModule
{
    public class SpawnPoint : MonoBehaviour
    {
        public static SpawnPoint Instance { get; private set; }

        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        // Optional: provide direct access to position/transform
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
    }
}