using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Private Serialized variables
    [Header("UI")]
    [SerializeField] private TMP_Dropdown kg_Dropdown;
    [SerializeField] private TextMeshProUGUI box2_Kg_Text;
    [SerializeField] private Button replayButton;

    [Space(5)]
    [Header("Script Ref")]
    [SerializeField] private CollisionCounting collisionCounting;

    [Space(5)]
    [Header("Boxes")]
    [SerializeField] private GameObject box2;
    [SerializeField] private GameObject box1;
    [SerializeField] private Vector2 forceAmount;
    #endregion

    #region Private Variables 
    private Rigidbody2D box2RB;
    private Rigidbody2D box1RB;
    private bool simulationRunning = true;
    private Vector2 box2InitialAnchoredPos;
    private Vector2 box1InitialAnchoredPos;
    #endregion

    #region MonoBehaviour

    private void OnEnable()
    {
        replayButton.onClick.AddListener(Replay);
    }
    private void Awake()
    {
        replayButton.interactable = false;
        box2RB = box2.GetComponent<Rigidbody2D>();
        box1RB = box1.GetComponent<Rigidbody2D>();

        box2InitialAnchoredPos = box2.GetComponent<RectTransform>().anchoredPosition;
        box1InitialAnchoredPos = box1.GetComponent<RectTransform>().anchoredPosition;

        ChangeBox2Mass(kg_Dropdown.value);
        box2RB.AddForce(forceAmount, ForceMode2D.Impulse);
    }
    private void Update()
    {
        if (simulationRunning)
        {
            if (Mathf.Abs(box1RB.linearVelocity.x) < 0.01f && Mathf.Abs(box2RB.linearVelocity.x) < 0.01f || collisionCounting.HasSimulationStopped(20f))
            {
                simulationRunning = false;
                replayButton.interactable = true;
            }
        }
    }
    private void OnDisable()
    {
        replayButton.onClick.RemoveAllListeners();
    }
    #endregion

    #region Private Methods 
    private void ChangeBox2Mass(int value)
    {
        UpdateBox2Text(kg_Dropdown.options[value].text);

        Debug.Log("Trying to Converert " + kg_Dropdown.options[value].text + " from string to int");
        if (int.TryParse(kg_Dropdown.options[value].text, out int mass))
        {
            Debug.Log("Conversion from string to int successful");
            box2RB.mass = mass;
            UpdateForceAmount();
        }
    }

    private void UpdateBox2Text(string text)
    {
        box2_Kg_Text.text = text + " Kg";
    }

    private void UpdateForceAmount() 
    {    
        forceAmount.x = -100 * box2RB.mass;
    }
    private void Replay()
    {
        // Reset positions, velocities, and states
        ChangeBox2Mass(kg_Dropdown.value);
        collisionCounting.ResetCollisionCount();
        simulationRunning = true;
        replayButton.interactable = false;
        box1RB.linearVelocity = Vector2.zero;
        box2RB.linearVelocity = Vector2.zero;

        box2.GetComponent<RectTransform>().anchoredPosition = box2InitialAnchoredPos;
        box1.GetComponent<RectTransform>().anchoredPosition = box1InitialAnchoredPos;

        box2RB.AddForce(forceAmount, ForceMode2D.Impulse);
    }
    #endregion
}
