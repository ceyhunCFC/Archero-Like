using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public SkillManager skillManager;
    public Button arrowMultiplicationButton;
    public Button bounceDamageButton;
    public Button burnDamageButton;
    public Button attackSpeedButton;
    public Button rageModeButton;

    void Start()
    {
        arrowMultiplicationButton.onClick.AddListener(() => skillManager.SetActiveSkill(SkillType.ArrowMultiplication));
        bounceDamageButton.onClick.AddListener(() => skillManager.SetActiveSkill(SkillType.BounceDamage));
        burnDamageButton.onClick.AddListener(() => skillManager.SetActiveSkill(SkillType.BurnDamage));
        attackSpeedButton.onClick.AddListener(() => skillManager.SetActiveSkill(SkillType.AttackSpeedIncrease));
        rageModeButton.onClick.AddListener(() => skillManager.SetActiveSkill(SkillType.RageMode));
    }
}