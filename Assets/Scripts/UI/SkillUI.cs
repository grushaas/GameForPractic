using UnityEngine;

public class SkillUI : MonoBehaviour
{
    public GameObject skillPanel;
    
    public void OpenSkillInterface()
    {
        skillPanel.SetActive(true);
    }

    public void CloseSkillInterface()
    {
        skillPanel.SetActive(false);
    }
}
