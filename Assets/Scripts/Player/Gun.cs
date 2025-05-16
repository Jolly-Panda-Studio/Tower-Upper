using UnityEngine;

namespace JollyPanda.LastFlag.PlayerModule
{
    public class Gun : MonoBehaviour
    {
        [field: SerializeField]
        public int Id { get; private set; }

        [field: SerializeField] public Transform FirePoint { get; private set; }

        [SerializeField] private BulletPool BulletPool;

        private float fireRate = 0.5f;
        private int bulletDamage;
        private float bulletSize;
        private float bulletSpeed;

        private float timer;

        private bool isActive = false;

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
            var bullet = BulletPool.GetBullet(bulletDamage, bulletSize, bulletSpeed, true);
            bullet.transform.position = FirePoint.position;
            bullet.transform.rotation = FirePoint.rotation;
        }

        internal void SetActive(bool isActive)
        {
            this.isActive = isActive;
        }

        internal void DisableAllBullets()
        {
            BulletPool.ReturnAllBullet();
        }

        internal void PauseBulletsMoving(bool isPause)
        {
            if (isPause)
            {
                BulletPool.PauseMoving();
            }
            else
            {
                BulletPool.ResumeMoving();
            }
        }

        public void SetFireRate(float fireRate)
        {
            this.fireRate = fireRate;
        }

        public void SetDamage(float damage)
        {
            bulletDamage = (int)damage;
        }

        public void SetBulletSize(float scale)
        {
            bulletSize = scale;
        }

        public void SetBulletSpeed(float speed)
        {
            bulletSpeed = speed;
        }
    }
}