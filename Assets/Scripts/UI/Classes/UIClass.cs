using UnityEngine;

namespace Lindon.UserManager.Base
{
    public abstract class UIClass : MonoBehaviour
    {
        /// <summary>
        /// Call when this panel is active (<see cref="SetActive(bool)"/>)
        /// </summary>
        protected abstract void SetValues();

        public virtual void SetActive(bool value)
        {
            gameObject.SetActive(value);

            if (value)
            {
                SetValues();
            }
        }
    }
}