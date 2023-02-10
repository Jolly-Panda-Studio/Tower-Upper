using Lindon.UserManager.Base.Element;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Lindon.UserManager.Base.Page
{
    public abstract class UIPage : UIClass
    {
        [Header("Page Info")]
        [SerializeField] protected string title;
        [SerializeField] private LoadTiming loadTiming = LoadTiming.InAwake;
        //[Scene, SerializeField] private int loadScene;
        [SerializeField] private List<UIElement> elements;

        [Header("Events")]
        [SerializeField] private UnityEvent onOpen;
        [SerializeField] private UnityEvent onClose;

        public string Title
        {
            get
            {
                if (title == null || title == string.Empty)
                {
                    title = gameObject.name;
                }
                return title;
            }
        }

        /// <summary>
        /// Call in game loading
        /// </summary>
        protected abstract void SetValuesOnSceneLoad();

        public virtual void DoLoad(LoadTiming mode)
        {
            if (loadTiming == mode)
            {
                SetValuesOnSceneLoad();
            }
        }

        public override void SetActive(bool value)
        {
            base.SetActive(value);

            LoadElements(value);
            InvokeEvents(value);
        }

        private void InvokeEvents(bool value)
        {
            if (value)
            {
                onOpen?.Invoke();
            }
            else
            {
                onClose?.Invoke();
            }
        }

        private void LoadElements(bool value)
        {
            foreach (var element in elements)
            {
                element.SetActive(value);
            }
        }
    }

    public enum LoadTiming
    {
        InAwake,
        InStart
    }
}