using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoadingManager : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private GameObject _loadingScreen;

    [SerializeField]
    private List<AsyncOperation> _scenesLoading;

    [SerializeField]
    private Slider _progressBar;

    [SerializeField]
    private TextMeshProUGUI _loadingText;

    [SerializeField]
    private LevelLoadingDescription _levelLoadingDescription;

    #endregion

    #region Methods

    public void Init()
    {
        SceneManager.LoadSceneAsync((int)SceneIndexes.MainMenu, LoadSceneMode.Additive);

        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel()
    {
        _scenesLoading = new List<AsyncOperation>();

        _scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.MainMenu));
        _scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.Level, LoadSceneMode.Additive));

        StartCoroutine(SceneLoadProgress(GameMode.Playing));
    }

    public void RestartLevel()
    {
        _scenesLoading = new List<AsyncOperation>();

        _scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.Level));
        _scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.Level, LoadSceneMode.Additive));

        StartCoroutine(SceneLoadProgress(GameMode.Playing));
    }

    public void LoadMenu()
    {
        _scenesLoading = new List<AsyncOperation>();

        _scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.Level));
        _scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MainMenu, LoadSceneMode.Additive));

        StartCoroutine(SceneLoadProgress(GameMode.MainMenu));
    }

    private IEnumerator SceneLoadProgress(GameMode afterLoad)
    {
        _loadingText.text = "loading...";

        if (afterLoad.Equals(GameMode.Playing))
        {
            _levelLoadingDescription.UpdateText();
            _levelLoadingDescription.gameObject.SetActive(true);

            foreach (var scene in _scenesLoading)
            {
                scene.allowSceneActivation = false;
            }
        }
        _loadingScreen.gameObject.SetActive(true);

        for (int i = 0; i < _scenesLoading.Count; i++)
        {
            while (!_scenesLoading[i].isDone)
            {
                var progressValue = 0f;
                foreach (var scene in _scenesLoading)
                {
                    progressValue += scene.progress;
                }
                _progressBar.value = progressValue / _scenesLoading.Count;

                if (afterLoad.Equals(GameMode.Playing) && _progressBar.value >= 0.9f)
                {
                    _loadingText.text = "press any key to continue";

                    bool pressedKey = false;
                    while (!pressedKey)
                    {
                        if (Input.anyKeyDown)
                        {
                            pressedKey = Input.anyKeyDown;
                            foreach (var scene in _scenesLoading)
                            {
                                scene.allowSceneActivation = true;
                            }
                        }
                        yield return null;
                    }
                }

                yield return null;
            }
        }

        if (afterLoad.Equals(GameMode.Playing))
            _levelLoadingDescription.gameObject.SetActive(false);
        _loadingScreen.gameObject.SetActive(false);
        GameManager.Instance.SetGameMode(afterLoad);
    }

    #endregion

}
