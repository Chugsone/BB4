using UnityEngine;


[CreateAssetMenu(fileName = "TooltipData", menuName = "Scriptable Objects/TooltipData")]
public class TooltipData : ScriptableObject
{
    public string title;
    [TextArea] public string description;
}
