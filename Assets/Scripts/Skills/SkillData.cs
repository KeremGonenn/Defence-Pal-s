using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillManager", order = 1)]
public class SkillData : ScriptableObject
{
    public int Id;
    public string Header;
    public Sprite SkillImage;
    public string Description;
    public SkillEnums SkillEnums;
    public GameObject Particles;
    public UnityEvent Event;
}
