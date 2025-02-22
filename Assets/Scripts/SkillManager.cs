using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillType activeSkill = SkillType.None;

    public static void SetActiveSkill(SkillType skillType)
    {
        if (activeSkill == skillType)
        {
            // Eğer skill zaten aktifse, devre dışı bırak
            activeSkill = SkillType.None;
            Debug.Log("Skill deactivated: " + skillType);
        }
        else
        {
            // Yeni skill'i aktif et
            activeSkill = skillType;
            Debug.Log("Skill activated: " + skillType);
        }
    }
}
