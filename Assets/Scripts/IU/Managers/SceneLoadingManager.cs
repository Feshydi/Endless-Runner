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

    #endregion

    #region Methods

    private void Awake()
    {
        _scenesLoading = new List<AsyncOperation>();
        SceneManager.LoadSceneAsync((int)SceneIndexes.MainMenu, LoadSceneMode.Additive);

        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel()
    {
        SceneManager.UnloadSceneAsync((int)SceneIndexes.MainMenu);
        _scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.Level, LoadSceneMode.Additive));

        StartCoroutine(SceneLoadProgress(GameMode.Playing));
    }

    public void LoadMenu()
    {
        SceneManager.UnloadSceneAsync((int)SceneIndexes.Level);
        _scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MainMenu, LoadSceneMode.Additive));

        StartCoroutine(SceneLoadProgress(GameMode.MainMenu));
    }

    private IEnumerator SceneLoadProgress(GameMode afterLoad)
    {
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

                yield return null;
            }
        }

        _loadingScreen.gameObject.SetActive(false);
        GameManager.Instance.SetGameMode(afterLoad);
    }

    #endregion

}
