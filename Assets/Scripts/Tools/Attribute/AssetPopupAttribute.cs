using System;
using UnityEngine;

/// <summary>
/// Shows all compatible assets as a pop up.
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class AssetPopupAttribute : PropertyAttribute
{
    private Type m_Type;
    private bool m_IncludeNone;
    public Type ChosenType => m_Type;
    public bool IncludeNone => m_IncludeNone;

    /// <summary>
    /// Creates an instance of asset popup attribute.
    /// </summary>
    /// <param name="type">The type of asset to show as popup. It must derive from UnityEngine.Object in order to work.</param>
    /// <param name="includeNone">If true, it will include 'None' in the popup options, otherwise it doesn't.</param>
    public AssetPopupAttribute(Type type, bool includeNone = true)
    {
        m_Type = type;
        m_IncludeNone = includeNone;
    }
}

