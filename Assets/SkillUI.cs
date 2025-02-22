using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public Toggle arrowMultiplicationToggle;
    public Toggle bounceDamageToggle;
    public Toggle burnDamageToggle;
    public Toggle attackSpeedToggle;
    public Toggle rageModeToggle;

    public ToggleGroup skillToggleGroup; // Toggle'ları gruplamak için ToggleGroup
    private bool isOn = false;
    private RectTransform panelRectTransform;
    private Vector2 targetPosition;
    private float animationDuration = 0.5f; // Animasyon süresi (saniye cinsinden)

    void Awake()
    {
        panelRectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {
        // Toggle'ları ToggleGroup'a ekle
        arrowMultiplicationToggle.group = skillToggleGroup;
        bounceDamageToggle.group = skillToggleGroup;
        burnDamageToggle.group = skillToggleGroup;
        attackSpeedToggle.group = skillToggleGroup;
        rageModeToggle.group = skillToggleGroup;

        // Toggle'ların değer değişikliğini dinle
        arrowMultiplicationToggle.onValueChanged.AddListener((isOn) => ToggleSkill(SkillType.ArrowMultiplication, isOn));
        bounceDamageToggle.onValueChanged.AddListener((isOn) => ToggleSkill(SkillType.BounceDamage, isOn));
        burnDamageToggle.onValueChanged.AddListener((isOn) => ToggleSkill(SkillType.BurnDamage, isOn));
        attackSpeedToggle.onValueChanged.AddListener((isOn) => ToggleSkill(SkillType.AttackSpeedIncrease, isOn));
        rageModeToggle.onValueChanged.AddListener((isOn) => ToggleSkill(SkillType.RageMode, isOn));
    }

    private void ToggleSkill(SkillType skillType, bool isOn)
    {
        if (isOn)
        {
            SkillManager.SetActiveSkill(skillType); // Skill'i aç
        }
        else
        {
            SkillManager.SetActiveSkill(SkillType.None); // Skill'i kapat
        }
    }

    public void TogglePanel()
    {
        if (isOn)
        {
            // Panel kapalıysa 0 konumuna (sola) kaydır
            targetPosition = new Vector2(0, panelRectTransform.anchoredPosition.y);
        }
        else
        {
            // Panel açıksa -150 konumuna (sağa) kaydır
            targetPosition = new Vector2(-150, panelRectTransform.anchoredPosition.y);
        }

        // Animasyonu başlat
        StartCoroutine(AnimatePanel(targetPosition));

        // Durumu tersine çevir
        isOn = !isOn;
    }

    private IEnumerator AnimatePanel(Vector2 targetPos)
    {
        Vector2 startPos = panelRectTransform.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            // Lerp kullanarak yumuşak geçiş sağla
            panelRectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // Bir sonraki frame'e kadar bekle
        }

        // Animasyon sonunda tam konuma ayarla
        panelRectTransform.anchoredPosition = targetPos;
    }

}