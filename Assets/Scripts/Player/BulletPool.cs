using JollyPanda.LastFlag.EnviromentModule;
using System.Collections.Generic;
using UnityEngine;

namespace JollyPanda.LastFlag.PlayerModule
{
    [System.Serializable]
    public class BulletPool
    {
        public Bullet bulletPrefab;
        public Transform parent;
        private readonly List<Bullet> pool = new();

        public Bullet GetBullet()
        {
            foreach (var bullet in pool)
            {
                if (!bullet.gameObject.activeInHierarchy)
                {
                    bullet.gameObject.SetActive(true);
                    return bullet;
                }
            }

            var newBullet = Object.Instantiate(bulletPrefab, SpawnPoint.Instance.transform);
            pool.Add(newBullet);
            return newBullet;
        }

        public void ReturnBullet(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }
    }
}