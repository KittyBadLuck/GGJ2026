using MyBox;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject selectLevelMenu;

    public void OpenSelectLevelMenu()
    {
        selectLevelMenu.SetActive(true);
    }
    public void CloseSelectLevelMenu()
    {
        selectLevelMenu.SetActive(false);
    }

    [ButtonMethod]
    private void DeletePlayerPref()
    {
        PlayerPrefs.DeleteAll();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
