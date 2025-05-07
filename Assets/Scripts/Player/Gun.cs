using JollyPanda.LastFlag.Handlers;
using System;
using UnityEngine;

namespace JollyPanda.LastFlag.PlayerModule
{
    public class Gun : MonoBehaviour
    {
        [field: SerializeField]
        public int Id { get; private set; }

        [field: SerializeField] public Transform FirePoint { get; private set; }

        [SerializeField] private BulletPool BulletPool;

        public float fireRate = 0.5f;

        private float timer;

        private bool isActive = true;

        void Update()
        {
            if (!isActive)
                return;

            timer += Time.deltaTime;
            if (timer >= fireRate)
            {
                Fire();
                timer = 0f;
            }
        }

        void Fire()
        {
            var bullet = BulletPool.GetBullet();
            bullet.transform.position = FirePoint.position;
            bullet.transform.rotation = FirePoint.rotation;
        }

        internal void SetActive(bool isActive)
        {
            this.isActive = isActive;
        }
    }
}