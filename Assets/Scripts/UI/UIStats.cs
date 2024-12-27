using UnityEngine;
using UnityEngine.UI;

public class UIStats : MonoBehaviour
{
    [Header("UI labels")]
    public Text levelLabel;
    public Text healthLabel;
    public Text strengthLabel;
    public Text ammoLabel;

    public void UpdateHealthLabel(float health)
    {
        if (healthLabel != null)
        {
            healthLabel.text = "Health: " + health.ToString();
        }
    }

    public void UpdateLevelLabel(int level)
    {
        if (levelLabel != null)
        {
            levelLabel.text = "Level: " + level.ToString();
        }
    }

    public void UpdateStrengthLabel(float damage)
    {
        if (strengthLabel != null)
        {
            strengthLabel.text = "Strength: " + damage.ToString();
        }
    }

    public void UpdateAmmoLabel(float ammo)
    {
        if (ammoLabel != null)
        {
            ammoLabel.text = "Ammo: " + ammo.ToString();
        }
    }
}
