using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region Private Serialized Variables
    [SerializeField] private int gameScene = 1;

    [Space(5)]
    [Header("UI")]
    [SerializeField] private Button playBtn;
    [SerializeField] private Button quitBtn;
    #endregion

    #region MonoBehaviour

    private void OnEnable()
    {
        playBtn.onClick.AddListener(OnPlay);
        quitBtn.onClick.AddListener(OnQuit);
    }

    private void OnDisable()
    {
        playBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.RemoveAllListeners();
    }
    #endregion

    #region Private Methods
    private void OnPlay() 
    {
        SceneManager.LoadScene(gameScene); 
    }

    private void OnQuit() 
    {
        Application.Quit();
    }
    #endregion
}
