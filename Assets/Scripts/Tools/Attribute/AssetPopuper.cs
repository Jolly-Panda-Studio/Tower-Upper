using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;


/// <summary>
/// Utility to show asset object fields as a popup.
/// </summary>
public static class AssetPopuper
{
    private struct PopupInfo
    {
        public List<Object> m_Assets;
        public GUIContent[] m_PopupOptions;
        public double m_UpdateTime;
    }

    private const int DuplicateSearchDepth = 4;
    private const float SwitchButtonWidth = 30f;
    private const string ShowingPopupKey = "ugs_framework_attributes_assetPopup";
    private const int UpdateAssetsPeriod = 5;

    private readonly static GUIContent m_NoneOption = new GUIContent("---NONE---", "Nothing selected (Null reference)");
    private readonly static GUIContent m_SwitchButtonContent = new GUIContent("<->", "Switch between popup and object reference");
    private static Dictionary<Type, PopupInfo> m_FoundAssets = new Dictionary<Type, PopupInfo>();

    private static bool IsShowingPopup
    {
        get => EditorPrefs.GetBool(ShowingPopupKey, true);
        set => EditorPrefs.SetBool(ShowingPopupKey, value);
    }

    /// <summary>
    /// Show the object field of provided type as a popup.
    /// </summary>
    /// <param name="type">The type of asset to show as popup. It must derive from UnityEngine.Object in order to work.</param>
    public static void DrawAssetPopup(Rect position, SerializedProperty property, GUIContent label, Type type, bool includeNone = true)
    {
        bool isCompatible = IsCompatible(property, type);
        if (isCompatible)
        {
            DrawProperty(position, property, label, type, includeNone);
        }
        else
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }

    /// <summary>
    /// Get height of the object field of provided type as popup.
    /// </summary>
    /// <param name="type">The type of asset to show as popup. It must derive from UnityEngine.Object in order to work.</param>
    public static float GetAssetPopupHeight(SerializedProperty property, GUIContent label, Type type)
    {
        bool isCompatible = IsCompatible(property, type);
        if (isCompatible)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        return EditorGUI.GetPropertyHeight(property, true);
    }

    public static List<Object> GetAssetOptions(Type type, out GUIContent[] popupOptions, bool includeNone)
    {
        PopupInfo popupInfo;
        if (!m_FoundAssets.ContainsKey(type))
        {
            popupInfo = InitializeType(type, includeNone);
            m_FoundAssets.Add(type, popupInfo);
        }
        else
        {
            popupInfo = m_FoundAssets[type];
            if (EditorApplication.timeSinceStartup - popupInfo.m_UpdateTime > UpdateAssetsPeriod)
            {
                popupInfo = InitializeType(type, includeNone);
                m_FoundAssets[type] = popupInfo;
            }
            else
            {
                if (includeNone)
                {
                    if (popupInfo.m_PopupOptions.Length == 0)
                    {
                        popupInfo.m_PopupOptions = new GUIContent[1] { m_NoneOption };
                    }
                    else if (popupInfo.m_PopupOptions[0] != m_NoneOption)
                    {
                        popupOptions = new GUIContent[popupInfo.m_PopupOptions.Length + 1];
                        popupOptions[0] = m_NoneOption;
                        for (int i = 1; i < popupOptions.Length; i++)
                        {
                            popupOptions[i] = popupInfo.m_PopupOptions[i - 1];
                        }
                        popupInfo.m_PopupOptions = popupOptions;
                    }
                }
                else
                {
                    if (popupInfo.m_PopupOptions.Length > 0 && popupInfo.m_PopupOptions[0] == m_NoneOption)
                    {
                        popupOptions = new GUIContent[popupInfo.m_PopupOptions.Length - 1];
                        for (int i = 0; i < popupOptions.Length; i++)
                        {
                            popupOptions[i] = popupInfo.m_PopupOptions[i + 1];
                        }
                        popupInfo.m_PopupOptions = popupOptions;
                    }
                }
            }
        }

        popupOptions = popupInfo.m_PopupOptions;
        return popupInfo.m_Assets;
    }

    private static bool IsCompatible(SerializedProperty property, Type type)
    {
        return property.propertyType == SerializedPropertyType.ObjectReference && typeof(Object).IsAssignableFrom(type);
    }

    private static void DrawProperty(Rect position, SerializedProperty property, GUIContent label, Type type, bool includeNone)
    {
        Rect popupPosition = new Rect(position.x, position.y, position.width - SwitchButtonWidth, position.height);
        popupPosition = RatioLabel(popupPosition, label, GUI.skin.label, 0.44f);
        Rect buttonPosition = new Rect(popupPosition.xMax, position.y, SwitchButtonWidth, position.height);

        if (IsShowingPopup)
        {
            DrawPopup(popupPosition, property, type, includeNone);
        }
        else
        {
            EditorGUI.ObjectField(popupPosition, property, type, GUIContent.none);
        }

        bool isButtonPressed = GUI.Button(buttonPosition, m_SwitchButtonContent);
        if (isButtonPressed)
        {
            IsShowingPopup = !IsShowingPopup;
        }
    }

    private static Rect RatioLabel(Rect position, GUIContent label, GUIStyle style, float labelRatio)
    {
        float labelWidth = position.width * labelRatio;
        float Indent = EditorGUI.indentLevel * 15;
        Rect labelPosition = new Rect(position.x + Indent, position.y, labelWidth - Indent, position.height);
        EditorGUI.LabelField(labelPosition, label, style);
        return new Rect(position.x + labelWidth, position.y, position.width - labelWidth, position.height);
    }

    private static void DrawPopup(Rect position, SerializedProperty property, Type type, bool includeNone)
    {
        List<Object> assets = GetAssetOptions(type, out GUIContent[] popupOptions, includeNone);

        if (assets != null && assets.Count > 0)
        {
            int chosenOptionIndex = 0;
            for (int i = 0; i < assets.Count; i++)
            {
                if (property.objectReferenceValue == assets[i])
                {
                    chosenOptionIndex = includeNone ? i + 1 : i;
                    break;
                }
            }

            chosenOptionIndex = EditorGUI.Popup(position, GUIContent.none, chosenOptionIndex, popupOptions);
            if (includeNone)
            {
                if (chosenOptionIndex == 0)
                {
                    property.objectReferenceValue = null;
                }
                else
                {
                    property.objectReferenceValue = assets[chosenOptionIndex - 1];
                }
            }
            else
            {
                property.objectReferenceValue = assets[chosenOptionIndex];
            }
        }
        else
        {
            EditorGUI.Popup(position, GUIContent.none, 0, popupOptions);
            property.objectReferenceValue = null;
        }
    }

    private static PopupInfo InitializeType(Type type, bool includeNone)
    {
        List<Object> assets = FindAssetsByType<Object>(type, false, null);
        int optionsCount = includeNone ? assets.Count + 1 : assets.Count;
        GUIContent[] popupOptions = new GUIContent[optionsCount];
        if (includeNone)
        {
            popupOptions[0] = m_NoneOption;
        }

        HashSet<int> optionDuplicateIndices = null;
        int i = includeNone ? 1 : 0;
        for (; i < popupOptions.Length; i++)
        {
            int assetIndex = includeNone ? i - 1 : i;
            string path = AssetDatabase.GetAssetPath(assets[assetIndex]);
            popupOptions[i] = new GUIContent(assets[assetIndex].name, "At: " + path);
            CheckDuplicates(popupOptions, ref optionDuplicateIndices, assets[assetIndex].name, i);
        }

        if (optionDuplicateIndices != null)
        {
            ResolveDuplicates(popupOptions, optionDuplicateIndices);
        }

        return new PopupInfo() { m_Assets = assets, m_PopupOptions = popupOptions, m_UpdateTime = EditorApplication.timeSinceStartup };
    }

    private static List<T> FindAssetsByType<T>(Type type, bool canSearchInFolders, string[] searchInFolders) where T : Object
    {
        List<T> assets = new List<T>();
        string[] guids;
        if (canSearchInFolders)
        {
            guids = AssetDatabase.FindAssets(string.Format("t:{0}", type), searchInFolders);
        }
        else
        {
            guids = AssetDatabase.FindAssets(string.Format("t:{0}", type));
        }

        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            T asset = AssetDatabase.LoadAssetAtPath(assetPath, type) as T;
            if (asset != null)
            {
                assets.Add(asset);
            }
        }

        return assets;
    }

    private static void CheckDuplicates(GUIContent[] popupOptions, ref HashSet<int> duplicateIndices, string assetName, int optionIndex)
    {
        bool hasDuplicatedName = IsDuplicated(popupOptions, assetName, optionIndex, out int duplicatedIndex);
        if (hasDuplicatedName)
        {
            if (duplicateIndices == null)
            {
                duplicateIndices = new HashSet<int>();
            }

            duplicateIndices.Add(duplicatedIndex);
            duplicateIndices.Add(optionIndex);
        }
    }

    private static bool IsDuplicated(GUIContent[] options, string assetName, int optionIndex, out int duplicatedIndex)
    {
        for (int i = 0; i < optionIndex; i++)
        {
            if (options[i].text == assetName)
            {
                duplicatedIndex = i;
                return true;
            }
        }

        duplicatedIndex = -1;
        return false;
    }

    private static void ResolveDuplicates(GUIContent[] options, HashSet<int> duplicateIndices)
    {
        int safetyIndex = 0;
        while (duplicateIndices.Count > 1 && safetyIndex < DuplicateSearchDepth)
        {
            safetyIndex++;

            foreach (int duplicatedIndex in duplicateIndices)
            {
                GUIContent option = options[duplicatedIndex];
                string folder = GetContainingFolder(option.tooltip, option.text);
                option.text = folder + "/" + option.text;
            }

            RemoveUniquePaths(options, duplicateIndices);
        }

        duplicateIndices.Clear();
    }

    private static string GetContainingFolder(string fullPath, string relativePath)
    {
        int relativeIndex = fullPath.LastIndexOf(relativePath);
        //remove relative path and slash before it
        string parentPath = fullPath.Remove(relativeIndex - 1);
        int parentSlashIndex = parentPath.LastIndexOf('/');
        return parentPath.Substring(parentSlashIndex + 1);
    }

    private static void RemoveUniquePaths(GUIContent[] options, HashSet<int> duplicateIndices)
    {
        int[] indicesArray = new int[duplicateIndices.Count];
        duplicateIndices.CopyTo(indicesArray);
        for (int i = 0; i < indicesArray.Length; i++)
        {
            int firstIndex = indicesArray[i];
            bool isDuplicated = false;
            for (int j = i + 1; j < indicesArray.Length; j++)
            {
                int secondIndex = indicesArray[j];
                if (firstIndex != secondIndex && options[firstIndex].text == options[secondIndex].text)
                {
                    isDuplicated = true;
                    break;
                }
            }

            if (!isDuplicated)
            {
                ReplaceSlash(options, firstIndex);
                duplicateIndices.Remove(firstIndex);
            }
        }
    }

    private static void ReplaceSlash(GUIContent[] options, int optionIndex)
    {
        GUIContent uniqueContent = options[optionIndex];
        uniqueContent.text = uniqueContent.text.Replace('/', '\\');
    }
}

