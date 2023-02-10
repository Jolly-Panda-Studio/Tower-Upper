using Lindon.UserManager.Base.Element;
using Lindon.UserManager.Base.Page;
using System.Collections.Generic;
using UnityEngine;

namespace Lindon.UserManager
{
    [RequireComponent(typeof(UserInterfaceShortcuts))]
    [RequireComponent(typeof(UserIntefaceData))]
    public class UserInterfaceManager : MonoBehaviour
    {
        public static UserInterfaceManager instance;

        [Header("Panels, Dialogs & Elements")]
        private List<UIPage> allPages;
        private List<UIElement> allElements;

        private Stack<UIPage> openPagesStack;

        public static UserIntefaceData Data => instance.gameObject.GetOrAddComponent<UserIntefaceData>();

        public void Initialization()
        {
            if (instance == null)
            {
                instance = this;

                DontDestroyOnLoad(gameObject);

                LoadDatas();

                CheckComponents();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void CheckComponents()
        {

        }

        public UserIntefaceData GetData()
        {
            return gameObject.GetOrAddComponent<UserIntefaceData>();
        }

        private void LoadDatas()
        {
            openPagesStack = new Stack<UIPage>();

            allElements = new List<UIElement>(GetComponentsInChildren<UIElement>(true));
            ElementInitialization();

            allPages = new List<UIPage>(GetComponentsInChildren<UIPage>(true));
            PagesInitialization(LoadTiming.InAwake);
        }

        private void Start()
        {
            PagesInitialization(LoadTiming.InStart);

            //Open<EntryPage>();
        }

        #region Pages's Function

        /// <summary>
        /// Initial setup of pages
        /// </summary>
        private void PagesInitialization(LoadTiming timing)
        {
            foreach (UIPage page in allPages)
            {
                // LOAD
                page.DoLoad(timing);

                // CLOSE
                page.SetActive(false);
            }
        }

        public static UIPage Open<T>() where T : UIPage
        {
            return Open(GetPageOfType<T>());
        }

        public static UIPage Open(UIPage newPage)
        {
            if (!(newPage is UIPopup))
            {
                instance.TurnOffTopPage();
            }

            foreach (var page in instance.allPages)
            {
                if (page == newPage)
                {
                    instance.openPagesStack.Push(page);
                    page.SetActive(true);
                    return page;
                }
            }
            return null;
        }

        public static T GetPageOfType<T>() where T : UIPage
        {
            for (int i = 0; i < instance.allPages.Count; i++)
            {
                if (instance.allPages[i] is T t)
                    return t;
            }
            return null;
        }

#if UNITY_EDITOR
        public static List<UIPage> GetAllPage()
        {
            return new List<UIPage>(FindObjectsOfType<UIPage>());
        }
#endif

        private bool CloseTopPage()
        {
            if (openPagesStack.Count > 0)
            {
                var top = openPagesStack.Pop();
                top.SetActive(false);
                return true;
            }

            return false;
        }

        #endregion

        #region Elements's functions

        private void ElementInitialization()
        {
            foreach (var element in allElements)
            {
                element.DoCreate();
            }
        }

        public static UIElement GetElementOfType<T>() where T : UIElement
        {
            for (int i = 0; i < instance.allElements.Count; i++)
            {
                if (instance.allElements[i] is T t)
                    return t;
            }
            return null;
        }

        #endregion

        public static void OnBackPressed()
        {
            if (instance.openPagesStack.Count > 0)
            {
                //if (instance.openPagesStack.Peek() == GetPageOfType<EntryPage>())
                //{
                //    Application.Quit();
                //}
                //else
                {
                    if (instance.CloseTopPage())
                    {
                        if (instance.openPagesStack.Count < 1)
                        {
                            //Open<EntryPage>();
                        }
                        else
                        {
                            Open(instance.openPagesStack.Pop());
                        }
                    }
                    else
                    {
                        Debug.Log("ERORR!!");
                    }
                }
            }
        }

        private void TurnOffTopPage()
        {
            if (openPagesStack.Count > 0)
            {
                var top = openPagesStack.Peek();
                top.SetActive(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>return active page/dialog title</returns>
        public static string GetTitle() => instance.openPagesStack.Count > 0 ? instance.openPagesStack.Peek().Title : string.Empty;
    }

    public static class UserInterface
    {
        public static UserIntefaceData Data => UserInterfaceManager.instance.GetData();
        //
    }
}