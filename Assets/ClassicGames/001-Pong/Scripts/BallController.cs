using UnityEngine;

namespace ClassicGames.Pong
{
    public class BallController : MonoBehaviour
    {
        Rigidbody2D rb;
        Vector2 moveDirection;

        float[] angles = new float[] { -60f, -45f, -30f, 0f,0f, 30f, 45f, 60f };

        [SerializeField] float ballSpeed = 5f;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            ChooseDirection();
        }

        void FixedUpdate()
        {
            rb.linearVelocity = moveDirection * ballSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            // rb.linearVelocity = new Vector2(5f, 0f);

            if(Input.GetKeyDown(KeyCode.Space))
            {
                ResetBallPosition();
            }
        }

        void ResetBallPosition()
        {
            rb.linearVelocity = Vector2.zero;
            rb.position = Vector2.zero;
            ChooseDirection();
        }

        void ChooseDirection()
        {
            float side = Random.value < 0.5f ? -1f : 1f;
            float angle = Random.Range(-30f, 30f) * Mathf.Deg2Rad;
            moveDirection = new Vector2(Mathf.Cos(angle) * side, Mathf.Sin(angle));
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            // Collision with paddle
            if(collision.gameObject.CompareTag("Paddle"))
            {
                // calculate the new angle based on the collision point

                // 1. get the contact point of the collision
                ContactPoint2D contactPoint = collision.GetContact(0);
                Vector2 hitPoint = contactPoint.point;

                // 2. get paddle bottom and top positions
                float paddleBottom = collision.collider.bounds.min.y;
                float paddleTop = collision.collider.bounds.max.y;

                float normalizedHitPoint = Mathf.InverseLerp(paddleBottom, paddleTop, hitPoint.y);
                int index = Mathf.Clamp(Mathf.FloorToInt(normalizedHitPoint * 8), 0, 7);
                Debug.Log($"Hit point: {hitPoint.y}, Paddle bottom: {paddleBottom}, Paddle top: {paddleTop}, Normalized hit point: {normalizedHitPoint}, Index: {index}");

                // 3. get the angle in radians based on the index
                float rad = angles[index] * Mathf.Deg2Rad;

                // 4. calculate the new move direction based on the angle
                float horizontalDirection = collision.transform.position.x < transform.position.x ? 1f : -1f;
                moveDirection = new Vector2(horizontalDirection * Mathf.Cos(rad), Mathf.Sin(rad));
            }

            if(collision.gameObject.CompareTag("EdgeWall"))
            {
                // #4 reflect the ball's direction based on the collision normal
                Vector2 normal = collision.contacts[0].normal;
                moveDirection = Vector2.Reflect(moveDirection, normal);
            }

            if(collision.gameObject.CompareTag("BottomLine"))
            {
                // #5 reset the ball's position and choose a new direction
                ResetBallPosition();
            }
        }
    }
}