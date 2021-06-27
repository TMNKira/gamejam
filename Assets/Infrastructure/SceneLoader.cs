using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    private const string MENU_NAME = "MainMenu";
    private const string FIRST_LEVEL_NAME = "FPS";
    private const string SECOND_LEVEL_NAME = "";
    private const string THIRD_LEVEL_NAME = "";

    private bool isLoadingScene = false;

    public void LoadMenu()
    {
        if (this.isLoadingScene == false)
            this.StartCoroutine(this.LoadSceneAsyncRoutine(MENU_NAME));
    }

    public void LoadFirstLevel()
    {
        if (this.isLoadingScene == false)
            this.StartCoroutine(this.LoadSceneAsyncRoutine(FIRST_LEVEL_NAME));
    }

    public void LoadSecondLevel()
    {
        if (this.isLoadingScene == false)
            this.StartCoroutine(this.LoadSceneAsyncRoutine(SECOND_LEVEL_NAME));
    }

    public void LoadThirdLevel()
    {
        if (this.isLoadingScene == false)
            this.StartCoroutine(this.LoadSceneAsyncRoutine(THIRD_LEVEL_NAME));
    }

    private IEnumerator LoadSceneAsyncRoutine(string sceneName)
    {
        LoadingScreen.Show();
        this.isLoadingScene = true;        

        var loadAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        loadAsync.allowSceneActivation = false;

        while (loadAsync.progress < 0.9f)        
            yield return null;        
                
        loadAsync.allowSceneActivation = true;        
        this.isLoadingScene = false;
        LoadingScreen.Hide();
    }
}
