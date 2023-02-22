using Lindon.UserManager;
using Lindon.UserManager.Base.Page;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceShortcuts : MonoBehaviour
{
    [SerializeField] private List<Shortcuts> shortcuts;

    private void Update()
    {
        foreach (Shortcuts shortcut in shortcuts)
        {
            shortcut.Update();
        }
    }

    [System.Serializable]
    private struct Shortcuts
    {
        [SerializeField] private UIPage page;
        [SerializeField] private KeyCode key;

        public void Update()
        {
            if (Input.GetKeyDown(key))
            {
                UserInterfaceManager.Open(page);
            }
        }
    }
}
