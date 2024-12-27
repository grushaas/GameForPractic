using UnityEngine;
using UnityEngine.UI;

public class AddAmo : MonoBehaviour
{
    private Button button;
    public Shooting shooting;
    private SkillUI skillUI;

    private void Awake()
    {
        button = GetComponent<Button>();
        skillUI = GetComponent<SkillUI>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        shooting.countAmmo += 2;
        Time.timeScale = 1.0f;
        skillUI.CloseSkillInterface();
    }
}
