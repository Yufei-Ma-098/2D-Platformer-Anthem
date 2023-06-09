using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float chaseRange;
    public int damage = 1;
    public float speed = 6f;
    public float rotationSpeed = 400f; 
    public float fixedYPosition;  

    private Rigidbody2D rb;
    private float distanceToPlayer = Mathf.Infinity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= chaseRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * speed, 0);

            // 让敌人以自己的中心为轴心旋转
            rb.MoveRotation(rb.rotation + rotationSpeed * Time.deltaTime);

            if (distanceToPlayer <= rb.GetComponent<Collider2D>().bounds.size.magnitude)
            {
                AttackPlayer();
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        transform.position = new Vector3(transform.position.x, fixedYPosition, transform.position.z);
    }

    private void AttackPlayer()
    {
        player.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes") || collision.gameObject.CompareTag("Traps"))
        {
            Destroy(gameObject);
        }
    }
}
