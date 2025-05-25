using TMPro;
using UnityEngine;

public class CollisionCounting : MonoBehaviour
{
    #region Private Serialized Variables
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] UIManager uiManager;
    #endregion

    #region Private Variables
    private float timeSinceLastCollision = -1f;
    private int collisionAmount;
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        collisionAmount = 0;
        uiManager.UpdateCollisionText(collisionAmount);
    }
    /// <summary>
    /// Updating Collison amount and txt on every collision of box1
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collisionMask & (1 << collision.gameObject.layer)) != 0)
        {
            collisionAmount++;
            uiManager.UpdateCollisionText(collisionAmount);
            timeSinceLastCollision = Time.time;
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Checking the time has passed since last collision
    /// </summary>
    /// <param name="threshold"></param>
    /// <returns></returns>
    public bool HasSimulationStopped(float threshold)
    {
        if (timeSinceLastCollision < 0) return false;
        return Time.time - timeSinceLastCollision > threshold;
    }
    /// <summary>
    /// Resetting collision amoutn and txt
    /// </summary>
    public void ResetCollisionCount() 
    {
        timeSinceLastCollision = -1f;
        collisionAmount = 0;
        uiManager.UpdateCollisionText(collisionAmount);
    }
    #endregion
}
