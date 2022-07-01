using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Panel_Pause : PanelController
{
    public void OnClickBackToMainMenu()
    {
        SceneManager.LoadScene("SelectCharacter");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickResumeGame()
    {
        isEnd = true;
        gM.panelM.isPause = false;
    }
}
