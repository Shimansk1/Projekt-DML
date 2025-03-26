using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [Header("HP Bar")]
    [SerializeField] private Image HPBar;
    [SerializeField] private TextMeshProUGUI HPText;
    [SerializeField] private Image HPIcon;

    [Header("Hunger Bar")]
    [SerializeField] private Image HungerBar;
    [SerializeField] private TextMeshProUGUI HungerText;
    [SerializeField] private Image HungerIcon;

    [Header("Thirst Bar")]
    [SerializeField] private Image ThirstBar;
    [SerializeField] private TextMeshProUGUI ThirstText;
    [SerializeField] private Image ThirstIcon;

    [Header("Player Reference")]
    [SerializeField] private PlayerHealth playerHealth;

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (playerHealth != null)
        {
            // HP Bar
            UpdateBar(HPBar, HPText, HPIcon, playerHealth.currentHealth, playerHealth.maxHealth, Color.green, Color.red);

            // Hunger Bar (zatím placeholder, pro budoucí integraci)
            UpdateBar(HungerBar, HungerText, HungerIcon, 80, 100, new Color(0.8f, 0.5f, 0f), new Color(0.5f, 0.25f, 0f));

            // Thirst Bar (zatím placeholder, pro budoucí integraci)
            UpdateBar(ThirstBar, ThirstText, ThirstIcon, 50, 100, Color.blue, Color.cyan);

            //Debug.Log(playerHealth.currentHealth);
        }
    }

    private void UpdateBar(Image bar, TextMeshProUGUI text, Image icon, int current, int max, Color fullColor, Color lowColor)
    {
        float percentage = (float)current / max;
        bar.fillAmount = percentage;
        bar.color = Color.Lerp(lowColor, fullColor, percentage);
        text.text = $"{current} / {max}";
    }
}
