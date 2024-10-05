using UnityEngine;
 
public enum HelpBoxMessageType { None, Info, Warning, Error }
 
public class HelpBoxAttribute : PropertyAttribute {
 
    public string text;
    public HelpBoxMessageType messageType;
    public float height;
 
    public HelpBoxAttribute(string text, HelpBoxMessageType messageType = HelpBoxMessageType.None, float height = 0) {
        this.text = text;
        this.messageType = messageType;
        this.height = height;
    }
}