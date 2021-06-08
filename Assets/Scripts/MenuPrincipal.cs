using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
namespace SlimUI.ModernMenu
{
    public class MenuPrincipal : MonoBehaviour
    {
        public GameObject settingsPanel;
        public GameObject mainPanel;
        public GameObject scorePanel;
        public GameObject PLAYPanel;
        public GameObject GameOptionsPanel;
        public GameObject VideoOptionsPanel;
        public GameObject yousurePanel;


        Resolution[] resolutions;
        public Dropdown resolutionsDropdown;
        public Slider slider;
        public Text text1;
        public Text text2;
        public TMP_Text InputNombre;
        public TMP_Text Warning;


        public enum Theme { custom1, custom2, custom3 };
        [Header("Theme Settings")]
        public Theme theme;
        int themeIndex;
        public ThemeEditor themeController;



        // Start is called before the first frame update
        private void Start()
        {
            text1.text = "";
            text2.text = "";
            /*resolutions=Screen.resolutions;
            resolutionsDropdown.ClearOptions();
            List<string> options = new List<string>();
            int currentResolutionIndex = 0;
            for(int i=0;i<resolutions.Length;i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                    currentResolutionIndex = i;
            }
            resolutionsDropdown.AddOptions(options);
            resolutionsDropdown.value = currentResolutionIndex;
            resolutionsDropdown.RefreshShownValue();*/
        }

        void Awake()
        {
            SetThemeColors();
        }

        void SetThemeColors()
        {
            if (theme == Theme.custom1)
            {
                themeController.currentColor = themeController.custom1.graphic1;
                themeController.textColor = themeController.custom1.text1;
                themeIndex = 0;
            }
            else if (theme == Theme.custom2)
            {
                themeController.currentColor = themeController.custom2.graphic2;
                themeController.textColor = themeController.custom2.text2;
                themeIndex = 1;
            }
            else if (theme == Theme.custom3)
            {
                themeController.currentColor = themeController.custom3.graphic3;
                themeController.textColor = themeController.custom3.text3;
                themeIndex = 2;
            }
        }
        public void EmpezarJuego()
        {
            SceneManager.LoadScene(1);
        }
        public void SalirJuego()
        {
            Debug.Log("Quitting");
            Application.Quit();

        }
        public void OpenSettings()
        {
            Warning.gameObject.SetActive(false);
            scorePanel.SetActive(false);
            yousurePanel.SetActive(false);
            PLAYPanel.SetActive(false);
            settingsPanel.SetActive(true);
            mainPanel.SetActive(false);
            GameOptionsPanel.SetActive(true);
            VideoOptionsPanel.SetActive(false);
        }
        public void CloseSettings()
        {
            //setVariables();
            settingsPanel.SetActive(false);
            mainPanel.SetActive(true);
        }



        public void setVariables()
        {
            float vol = slider.value;
            Camera.main.GetComponent<AudioSource>().volume = vol;
            //GameVariables.setVolume(vol);
            Resolution res = resolutions[resolutionsDropdown.value];
            Debug.Log("width: " + res.width + " height " + res.height);
            Screen.SetResolution(res.width, res.height, true);
            //GameVariables.setResolution(res);
        }

        public void verPuntuaciones()
        {
            text1.text = "";
            text2.text = "";
            mainPanel.SetActive(false);
            scorePanel.SetActive(true);
            /*List<PlayerScore> players=SaveSystem.LoadPlayers();
            players.Sort();
            while(players.Count>10)
            {
                players.RemoveAt(players.Count - 1);
            }
            foreach(PlayerScore p in players)
            {
                text1.text = text1.text + p.name + "\n";
                text2.text = text2.text + p.getMonedas() + "\n";
                text3.text = text3.text + p.getKills()+ "\n";
            }*/
        }

       

        public void displayPlayOptions()
        {
            scorePanel.SetActive(false);
            yousurePanel.SetActive(false);
            if (PLAYPanel.activeSelf)
                PLAYPanel.SetActive(false);
            else
                PLAYPanel.SetActive(true);
        }

        public void gameOptionsOpen()
        {
            GameOptionsPanel.SetActive(true);
            VideoOptionsPanel.SetActive(false);
        }
        public void VideoOptionsOpen()
        {
            GameOptionsPanel.SetActive(false);
            VideoOptionsPanel.SetActive(true);
        }
        public void setNombre()
        {
            string nombre = InputNombre.text;
            if (!string.IsNullOrEmpty(nombre) && nombre.Length > 1)
            {
                Warning.gameObject.SetActive(false);
            }
            else
            {
                Warning.gameObject.SetActive(true);
            }
        }

        public void displayPuntuacion()
        {
            PLAYPanel.SetActive(false);
            yousurePanel.SetActive(false);
            if (scorePanel.activeSelf)
                scorePanel.SetActive(false);
            else
                scorePanel.SetActive(true);

            verPuntuaciones();
        }

        public void displayYouSure()
        {
            PLAYPanel.SetActive(false);
            scorePanel.SetActive(false);
            if (yousurePanel.activeSelf)
                yousurePanel.SetActive(false);
            else
                yousurePanel.SetActive(true);
        }

    }
}

