using UnityEngine;

namespace ClassicGames.Pong
{
    public class PaddleController : MonoBehaviour
    {
        [SerializeField] KeyCode upKey;
        [SerializeField] KeyCode downKey;
        [SerializeField] float speed = 5f;

        Rigidbody2D rb;
        float input;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            input = 0f;

            if(Input.GetKey(upKey))
            {
                input += 1f;
            }

            if(Input.GetKey(downKey))
            {
                input -= 1f;
            }
        }

        void FixedUpdate()
        {
            float nextY = rb.position.y + input * speed * Time.fixedDeltaTime;
            nextY = Mathf.Clamp(nextY, -3f, 3f);
            rb.MovePosition(new Vector2(rb.position.x, nextY));
        }
    }
}