using UnityEngine;
using UnityEngine.UI;

public class AddSpd : MonoBehaviour
{
    private Button button;
    private PlayerController playContr;
    private SkillUI skillUI;

    private void Start()
    {
        playContr = FindFirstObjectByType<PlayerController>();
        if (playContr == null)
        {
            Debug.LogError("PlayerController not found in the scene!");
        }
    }

    private void Awake()
    {
        button = GetComponent<Button>();
        skillUI = GetComponent<SkillUI>();
        

        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("Button component not found on this GameObject!");
        }

        skillUI = FindFirstObjectByType<SkillUI>(); // Измените на FindObjectOfType, если SkillUI на другом объекте
        if (skillUI == null)
        {
            Debug.LogError("SkillUI not found in the scene!");
        }

        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        playContr.speed += 0.2f;
        Time.timeScale = 1.0f;
        skillUI.CloseSkillInterface();
    }
}
