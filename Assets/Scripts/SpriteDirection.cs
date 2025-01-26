using UnityEngine;

public class SpriteDirection : MonoBehaviour
{
    [SerializeField] Transform spriteTransform;
    private Rigidbody2D rb;

    void Start()
    {
        if (spriteTransform == null)
        {
            Debug.LogError("Sprite Transform is not assigned in the Inspector.");
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No RigidBody on GameObject");
        }
    }

    void Update()
    {
        UpdateSpriteDirection(rb.velocity);
    }

    private void UpdateSpriteDirection(Vector3 velocity)
    {
        if (velocity.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(-velocity.y, -velocity.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            spriteTransform.rotation = Quaternion.Slerp(spriteTransform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}