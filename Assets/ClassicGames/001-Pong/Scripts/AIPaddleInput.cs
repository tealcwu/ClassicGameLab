using UnityEngine;

namespace ClassicGames.Pong
{
    public class AIPaddleInput : MonoBehaviour
    {
        [SerializeField] float speed = 5f;

        Rigidbody2D rb;
        
        float moveInput = 0f;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            float difference = GameObject.FindGameObjectWithTag("Ball").transform.position.y - transform.position.y;


            if(difference > 0.2f)
            {
                moveInput = 1f;
            }
            else if(difference < -0.2f)
            {
                moveInput = -1f;
            }
            else
            {
                moveInput = 0f;
            }
        }

        void FixedUpdate()
        {
            float nextY = rb.position.y + moveInput * speed * Time.fixedDeltaTime;
            nextY = Mathf.Clamp(nextY, -3f, 3f);
            rb.MovePosition(new Vector2(rb.position.x, nextY));
        }
    }
}
