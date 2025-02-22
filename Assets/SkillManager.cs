using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillType activeSkill = SkillType.None;  // Aktif skill

    public void SetActiveSkill(SkillType skill)
    {
        activeSkill = skill;
        ApplySkillEffects();
    }

    void ApplySkillEffects()
    {
        
    }
}