using UnityEngine;
using UnityEngine.UI;

public class AddDmg : MonoBehaviour
{
    private Button button;
    public Bullet bullet;
    private SkillUI skillUI;

    private void Awake()
    {
        button = GetComponent<Button>();
        skillUI = GetComponent<SkillUI>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        bullet.newDamage(1.5f);
        Time.timeScale = 1.0f;
        skillUI.CloseSkillInterface();
    }
}
