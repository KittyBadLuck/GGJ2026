using MyBox;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject selectLevelMenu;
    public GameObject creditMenu;

    public void OpenSelectLevelMenu()
    {
        selectLevelMenu.SetActive(true);
    }
    public void CloseSelectLevelMenu()
    {
        selectLevelMenu.SetActive(false);
    }

    public void OpenCredit()
    {
        creditMenu.SetActive(true);
    }
    public void CloseCredit()
    {
        creditMenu.SetActive(false);
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
