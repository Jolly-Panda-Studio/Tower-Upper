using Lindon.UserManager;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UserInterfaceShortcuts : MonoBehaviour
{
    //[SerializeField] private List<Shortcuts> shortcuts;

    //private void Update()
    //{
    //    foreach (Shortcuts shortcut in shortcuts)
    //    {
    //        shortcut.Update();
    //    }
    //}

    //private void Reset()
    //{
    //    foreach (Shortcuts shortcut in shortcuts)
    //    {
    //        shortcut.Reset();
    //    }
    //}

    //[System.Serializable]
    //private struct Shortcuts
    //{
    //    [Dropdown(nameof(GetValues)),SerializeField] private UIPage page;
    //    [SerializeField] private KeyCode key;

    //    DropdownList<UIPage> list;
    //    private DropdownList<UIPage> GetValues()
    //    {
    //        list ??= new DropdownList<UIPage>();

    //        if (list.Count() > 0) return list;

    //        var pages = FindObjectsOfType<UIPage>(true);
    //        foreach (var page in pages)
    //        {
    //            list.Add(page.name, page);
    //        }

    //        return list;
    //    }

    //    public void Update()
    //    {
    //        if (Input.GetKeyDown(key))
    //        {
    //            UserInterfaceManager.Open(page);
    //        }
    //    }

    //    public void Reset()
    //    {
    //        list = new DropdownList<UIPage>();
    //    }
    //}
}
