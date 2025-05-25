using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    #region Private Serialized Variables
    [Space(5)]
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI box2_Kg_Text;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private TextMeshProUGUI collisionAmountText;
    [SerializeField] private int homeScene;
    [SerializeField] private TMP_Dropdown kg_Dropdown;

    [Space(5)]
    [Header("Script ref")]
    [SerializeField] private GameManager gameManager;
    #endregion

    #region MonoBehaviour

    private void OnEnable()
    {
        replayButton.onClick.AddListener(gameManager.Replay);
        homeButton.onClick.AddListener(Home);
    }

    private void OnDisable()
    {
        replayButton.onClick.RemoveAllListeners();
        homeButton.onClick.RemoveAllListeners();
    }
    #endregion

    #region Public Methods

    public void CanInteractReplayBtn(bool interact) 
    {
        replayButton.interactable = interact;
    }

    /// <summary>
    /// Updating Kg text on Box2
    /// </summary>
    /// <param name="text"></param>
    public void UpdateBox2Text(string text)
    {
        box2_Kg_Text.text = text + " Kg";
    }

    /// <summary>
    /// Updating collision txt based on collison amount
    /// </summary>
    public void UpdateCollisionText(int amount)
    {
        collisionAmountText.text = "Collision amount: " + amount.ToString();
    }
    #endregion

    #region Private Methods
    private void Home()
    {
        SceneManager.LoadScene(homeScene);
    }
    #endregion

    #region Getters
    public TMP_Dropdown GetKgDropdown() => kg_Dropdown;
    #endregion
}
