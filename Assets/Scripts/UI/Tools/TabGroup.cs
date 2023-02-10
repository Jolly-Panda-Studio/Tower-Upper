using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class TabGroup : MonoBehaviour
{
    [SerializeField] private ToggleGroup toggleGroup;

    private List<TabToggle> tabToggles;

    public System.Action<int> onTabChanged;

    private Dictionary<int, List<GameObject>> itemsCollection;

    private void FindToggles()
    {
        tabToggles = new List<TabToggle>();
        var list = GetComponentsInChildren<TabToggle>();
        for (int i = 0; i < list.Length; i++)
        {
            var toggle = list[i];
            toggle.transform.SetSiblingIndex(i);
            tabToggles.Add(toggle);
        }
    }

    private void ChangeTab(int index, bool value)
    {
        if (value)
        {
            onTabChanged?.Invoke(index);
        }
    }

    public void Initialization(Queue<int> indexs)
    {
        toggleGroup ??= GetComponent<ToggleGroup>();

        FindToggles();
        if (tabToggles.Count == 0) return;
        for (int i = 0; i < tabToggles.Count; i++)
        {
            TabToggle tabToggle = tabToggles[i];
            var index = indexs.Dequeue();
            tabToggle.SetData(toggleGroup, (value) => ChangeTab(index, value));
        }

        tabToggles[0].GetComponent<Toggle>().onValueChanged?.Invoke(true);
        //myToggle.onValueChanged?.Invoke(value);

    }
}
