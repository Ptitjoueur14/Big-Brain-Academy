using Modes.Stretching;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus
{
    public class GameEndMenu : MonoBehaviour
    {
        public Stretching stretching;
        public TMP_Text medalTypeText;
        public TMP_Text brainMassText;
        
        public void Start()
        {
            stretching = FindFirstObjectByType<Stretching>();
            UpdateMedalTexts();
        }

        public void UpdateMedalTexts()
        {
            medalTypeText.text = stretching.obtainedMedal.ToString();
            switch (stretching.obtainedMedal)
            {
                case MedalType.Platinum:
                    medalTypeText.color = Color.cyan;
                    break;
                case MedalType.Gold:
                    medalTypeText.color = Color.yellow;
                    break;
                case MedalType.Silver:
                    medalTypeText.color = Color.gray;
                    break;
                case MedalType.Bronze:
                    medalTypeText.color = new Color(1f, 0.5f, 0f);
                    break;
                case MedalType.None:
                    // TODO : Text for case None
                    break;
            }
            
            brainMassText.text = stretching.brainMass + " g";
        }

        public void OnMainMenuButtonClicked()
        {
            SceneManager.LoadScene("MainMenu");
            // TODO: Destroy Stretching from DontDestroyOnLoad
            Destroy(stretching.gameObject);
        }
    }
}