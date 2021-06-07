using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject mainPanel;
    public GameObject scorePanel;
    public GameObject PLAYPanel;
    public GameObject GameOptionsPanel;
    public GameObject VideoOptionsPanel;
    Resolution[] resolutions;
    public Dropdown resolutionsDropdown;
    public Slider slider;
    public Text text1;
    public Text text2;
    public Text text3;
    // Start is called before the first frame update
    private void Start()
    {
        text1.text = "";
        text2.text = "";
        text3.text = "";
        resolutions=Screen.resolutions;
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
        resolutionsDropdown.RefreshShownValue();
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
        settingsPanel.SetActive(true);
        mainPanel.SetActive(false);
        PLAYPanel.SetActive(false);
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
        text3.text = "";
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

    public void closeScore()
    {
        scorePanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void displayPlayOptions()
    {
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
}
