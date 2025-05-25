using TMPro;
using UnityEngine;

public class CollisionCounting : MonoBehaviour
{
    #region Private Serialized Variables
    [SerializeField] private TextMeshProUGUI collisionAmountText;
    #endregion

    #region Private Variables
    private float timeSinceLastCollision = -1f;
    private int collisionAmount;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        collisionAmount = 0;
        UpdateCollisionText();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionAmount++;
        UpdateCollisionText();
        timeSinceLastCollision = Time.time;
    }
    #endregion

    #region Private Methods
    private void UpdateCollisionText() 
    {
        collisionAmountText.text = "Collision amount: " + collisionAmount.ToString();
    }
    #endregion

    #region Public Methods
    public bool HasSimulationStopped(float threshold)
    {
        if (timeSinceLastCollision < 0) return false;
        return Time.time - timeSinceLastCollision > threshold;
    }
    public void ResetCollisionCount() 
    {
        timeSinceLastCollision = -1f;
        collisionAmount = 0;
        UpdateCollisionText();
    }
    #endregion
}
