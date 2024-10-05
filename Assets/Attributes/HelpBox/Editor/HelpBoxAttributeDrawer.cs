using UnityEngine;
using UnityEditor;
 
[CustomPropertyDrawer(typeof(HelpBoxAttribute))]
public class HelpBoxAttributeDrawer : DecoratorDrawer {
 
    public override float GetHeight() {
        var helpBoxAttribute = attribute as HelpBoxAttribute;
        if (helpBoxAttribute == null) return base.GetHeight();


        var helpBoxStyle = EditorStyles.helpBox;
        if (helpBoxStyle == null) return base.GetHeight();
        return helpBoxAttribute.height > 0 ? helpBoxAttribute.height : Mathf.Max(40,helpBoxStyle.CalcHeight(new GUIContent(helpBoxAttribute.text), EditorGUIUtility.currentViewWidth)+4);
    }
 
    public override void OnGUI(Rect position) {
        var helpBoxAttribute = attribute as HelpBoxAttribute;
        if (helpBoxAttribute == null) return;
        EditorGUI.HelpBox(position, helpBoxAttribute.text, GetMessageType(helpBoxAttribute.messageType));
    }
 
    private MessageType GetMessageType(HelpBoxMessageType helpBoxMessageType) {
        switch (helpBoxMessageType) {
            default:
            case HelpBoxMessageType.None: return MessageType.None;
            case HelpBoxMessageType.Info: return MessageType.Info;
           case HelpBoxMessageType.Warning: return MessageType.Warning;
            case HelpBoxMessageType.Error: return MessageType.Error;
        }
    }
}