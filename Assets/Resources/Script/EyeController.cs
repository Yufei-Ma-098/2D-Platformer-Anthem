using UnityEngine;

public class EyeController : MonoBehaviour
{
    public Transform player;
    public Transform eyeball;
    public Animator animator;

    private Vector3 initialPosition;
    private float minX = -0.64f;
    private float maxX = 0f;
    private float minY = -0.33f;
    private float maxY = 0f;

    void Start()
    {
        initialPosition = eyeball.localPosition;
    }

    void Update()
    {
        float playerX = Mathf.InverseLerp(-8f, 8f, player.position.x);

        float targetX = Mathf.Lerp(minX, maxX, playerX);
        float targetY = Mathf.Lerp(minY, maxY, 1 - Mathf.Cos(playerX * Mathf.PI));

        Vector3 targetPosition = new Vector3(targetX, targetY, initialPosition.z);
        eyeball.localPosition = initialPosition + targetPosition;

        Vector3 direction = player.position - eyeball.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        eyeball.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("PlayerNearby");
        }
    }
}
