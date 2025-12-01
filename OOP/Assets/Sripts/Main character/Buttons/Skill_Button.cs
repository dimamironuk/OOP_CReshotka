using UnityEngine;
using UnityEngine.UI;

public class Skill_Button : MonoBehaviour
{
    [SerializeField] private MainController mainController;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();

        if (button == null)
        {
            Debug.LogError("SkillButton requires a Button component on the same GameObject.");
            return;
        }

        if (mainController == null)
        {
            Debug.LogError("MainController link is missing in SkillButton inspector!");
            return;
        }

        button.onClick.AddListener(OnSkillClicked);
    }

    public void OnSkillClicked()
    {
        mainController.PerformSkill_B();
    }

    void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(OnSkillClicked);
        }
    }
}