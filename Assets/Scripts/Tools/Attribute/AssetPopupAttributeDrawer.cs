using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AssetPopupAttribute))]
public class AssetPopupAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        AssetPopupAttribute typePopup = attribute as AssetPopupAttribute;
        AssetPopuper.DrawAssetPopup(position, property, label, typePopup.ChosenType, typePopup.IncludeNone);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        AssetPopupAttribute typePopup = attribute as AssetPopupAttribute;
        return AssetPopuper.GetAssetPopupHeight(property, label, typePopup.ChosenType);
    }
}
