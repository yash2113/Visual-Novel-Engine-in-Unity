using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VNMenuManager : MonoBehaviour
{
    public static VNMenuManager instance;

    private MenuPage activePage = null;
    private bool isOpen = false;

    public AudioClip menuMusic;

    [SerializeField] private CanvasGroup root;
    [SerializeField] private MenuPage[] pages;

    private CanvasGroupController rootCG;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rootCG = new CanvasGroupController(this, root);
    }

    private MenuPage GetPage(MenuPage.PageType pageType)
    {
        return pages.FirstOrDefault(page => page.pageType == pageType); 
    }    

    public void OpenSavePage()
    {
        var page = GetPage(MenuPage.PageType.SaveAndLoad);
        var slm = page.anim.GetComponentInParent<SaveAndLoadMenu>();
        slm.menuFunction = SaveAndLoadMenu.MenuFunction.save;
        OpenPage(page);
    }

    public void OpenLoadPage()
    {
        var page = GetPage(MenuPage.PageType.SaveAndLoad);
        var slm = page.anim.GetComponentInParent<SaveAndLoadMenu>();
        slm.menuFunction = SaveAndLoadMenu.MenuFunction.load;
        OpenPage(page);
    }

    public void OpenConfigPage()
    {
        var page = GetPage(MenuPage.PageType.Config);
        OpenPage(page);
    }

    public void OpenHelpPage()
    {
        var page = GetPage(MenuPage.PageType.Help);
        OpenPage(page);
    }

    private void OpenPage(MenuPage page)
    {
        if (page == null)
        {
            return;
        }

        if (activePage != null && activePage != page)
        {
            activePage.Close();
        }

        page.Open();
        activePage = page;

        if(!isOpen)
        {
            OpenRoot();
        }
    }

    public  void OpenRoot()
    {
        rootCG.Show();
        rootCG.SetInteractableState(true);
        isOpen = true;
    }

    public void CloseRoot()
    {
        rootCG.Hide();
        rootCG.SetInteractableState(false);
        isOpen = false;
    }

    public void Click_Home()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(MainMenu.MAIN_MENU_SCENE);

        AudioManager.instance.StopTrack(0);
        AudioManager.instance.StopTrack(1);
        AudioManager.instance.PlayTrack(menuMusic, channel: 0, startingVolume: 0.2f, volumeCap: 0.4f);
    }

    public void Click_Quit()
    {
        Application.Quit();
    }

}
