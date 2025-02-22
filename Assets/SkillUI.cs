using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillUI : MonoBehaviour
{
    public Button PanelButton;
    public Button arrowMultiplicationButton;
    public Button bounceDamageButton;
    public Button burnDamageButton;
    public Button attackSpeedButton;
    public Button rageModeButton;

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
        PanelButton.onClick.AddListener(() => TogglePanel());
        arrowMultiplicationButton.onClick.AddListener(() => ToggleSkill(SkillType.ArrowMultiplication));
        bounceDamageButton.onClick.AddListener(() => ToggleSkill(SkillType.BounceDamage));
        burnDamageButton.onClick.AddListener(() => ToggleSkill(SkillType.BurnDamage));
        attackSpeedButton.onClick.AddListener(() => ToggleSkill(SkillType.AttackSpeedIncrease));
        rageModeButton.onClick.AddListener(() => ToggleSkill(SkillType.RageMode));
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

    private void ToggleSkill(SkillType skillType)
    {
        // Eğer seçilen skill zaten aktifse, devre dışı bırak
        if (SkillManager.activeSkill == skillType)
        {
            SkillManager.SetActiveSkill(SkillType.None); // Skill'i kapat
        }
        else
        {
            SkillManager.SetActiveSkill(skillType); // Skill'i aç
        }
    }
}