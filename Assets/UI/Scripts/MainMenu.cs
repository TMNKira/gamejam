using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private SceneLoader sceneLoader;

    private void Awake()
    {
        this.sceneLoader = SceneLoader.Instance;
    }

    private void OnEnable()
    {
        this.playButton.onClick.AddListener(this.PlayButtonClicked);
    }

    private void OnDisable()
    {
        this.playButton.onClick.RemoveListener(this.PlayButtonClicked);
    }

    private void PlayButtonClicked()
    {
        this.sceneLoader.LoadFirstLevel();
    }
}
