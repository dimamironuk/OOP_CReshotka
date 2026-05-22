using UnityEngine;
using UnityEngine.UI;

public class Attack_Button : MonoBehaviour
{
        [SerializeField] private MainController mainController;
        private Button button;

        void Awake()
        {
            button = GetComponent<Button>();

            if (button == null)
            {
                Debug.LogError("AttackButton requires a Button component on the same GameObject.");
                return;
            }

            if (mainController == null)
            {
                Debug.LogError("MainController link is missing in AttackButton inspector!");
                return;
            }

            button.onClick.AddListener(OnAttackClicked);
        }

        private void OnAttackClicked()
        {
            mainController.PerformAttack_B();
        }

        void OnDestroy()
        {
            if (button != null)
            {
                button.onClick.RemoveListener(OnAttackClicked);
            }
        }
}
