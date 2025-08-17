using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Blueprint", menuName = "Blueprint Data")]
public class BlueprintData : ScriptableObject
{
    public string BlueprintName;
    public Sprite Icon;
    public ResourceAmount FirstResource;
    public ResourceAmount SecondResource;
}
