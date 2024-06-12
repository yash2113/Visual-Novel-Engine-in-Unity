using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VISUALNOVEL;

public class MainMenu : MonoBehaviour
{
    public const string MAIN_MENU_SCENE = "Main Menu";

    public static MainMenu instance {  get; private set; }

    public AudioClip menuMusic;
    public CanvasGroup mainPanel;
    private CanvasGroupController mainCG;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        mainCG = new CanvasGroupController(this, mainPanel);
        AudioManager.instance.PlayTrack(menuMusic, channel: 0, startingVolume: 0.2f, volumeCap: 0.4f);
    }

    public void StartNewGame()
    {
        VNGameSave.activeFile = new VNGameSave();

        StartCoroutine(StartingGame());
    }

    public void LoadGame(VNGameSave file)
    {
        VNGameSave.activeFile = file;

        StartCoroutine(StartingGame());
    }

    private IEnumerator StartingGame()
    {
        mainCG.Hide(speed: 0.3f);
        AudioManager.instance.StopTrack(0);

        while(mainCG.isVisible)
            yield return null;

        UnityEngine.SceneManagement.SceneManager.LoadScene("VisualNovel");
    }

}
