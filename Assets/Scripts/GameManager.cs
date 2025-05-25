using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Private Serialized variables
    [Space(5)]
    [Header("Script Ref")]
    [SerializeField] private CollisionCounting collisionCounting;
    [SerializeField] private UIManager uiManager;

    [Space(5)]
    [Header("Boxes")]
    [SerializeField] private GameObject box2;
    [SerializeField] private GameObject box1;
    [SerializeField] private float forceMultiplier;
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
    private void Awake()
    {
        uiManager.CanInteractReplayBtn(false);
        box2RB = box2.GetComponent<Rigidbody2D>();
        box1RB = box1.GetComponent<Rigidbody2D>();

        box2InitialAnchoredPos = box2.GetComponent<RectTransform>().anchoredPosition;
        box1InitialAnchoredPos = box1.GetComponent<RectTransform>().anchoredPosition;

        ChangeBox2Mass(uiManager.GetKgDropdown().value);
        box2RB.AddForce(forceAmount, ForceMode2D.Impulse);
    }
    private void Update()
    {
        if (simulationRunning)
        {
            // checking if either velocity is 0 or last collision was 20sec ago.  
            if (Mathf.Abs(box1RB.linearVelocity.x) < 0.01f && Mathf.Abs(box2RB.linearVelocity.x) < 0.01f || collisionCounting.HasSimulationStopped(20f))
            {
                simulationRunning = false;
                uiManager.CanInteractReplayBtn(true);
            }
        }
    }
    #endregion

    #region Private Methods 
    /// <summary>
    /// Changing box2 mass, Updating text on box2 and also the force applied on it
    /// </summary>
    /// <param name="value"></param>
    private void ChangeBox2Mass(int value)
    {
        uiManager.UpdateBox2Text(uiManager.GetKgDropdown().options[value].text);

        Debug.Log("Trying to Converert " + uiManager.GetKgDropdown().options[value].text + " from string to int");
        if (int.TryParse(uiManager.GetKgDropdown().options[value].text, out int mass))
        {
            Debug.Log("Conversion from string to int successful");
            box2RB.mass = mass;
            UpdateForceAmount();
        }
    }

    /// <summary>
    /// Updating Force amount in only x.
    /// </summary>
    private void UpdateForceAmount() 
    {    
        forceAmount.x = -(forceMultiplier * box2RB.mass); // - cause we need it to move in -x direction
    }

    #endregion

    #region Public Methods
    /// <summary>
    /// Reseting positions, velocities, and states, collision amount, bxo2 mass
    /// </summary>
    public void Replay()
    {
        ChangeBox2Mass(uiManager.GetKgDropdown().value);
        collisionCounting.ResetCollisionCount();
        simulationRunning = true;
        uiManager.CanInteractReplayBtn(false);
        box1RB.linearVelocity = Vector2.zero;
        box2RB.linearVelocity = Vector2.zero;

        box2.GetComponent<RectTransform>().anchoredPosition = box2InitialAnchoredPos;
        box1.GetComponent<RectTransform>().anchoredPosition = box1InitialAnchoredPos;

        box2RB.AddForce(forceAmount, ForceMode2D.Impulse);
    }
    #endregion
}
