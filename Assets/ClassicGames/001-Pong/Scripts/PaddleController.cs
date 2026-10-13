using UnityEngine;

namespace ClassicGames.Pong
{
    public class PaddleController : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKey(KeyCode.W))
            {
                if(transform.position.y < 3f)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * 5f);
                }
            }

            if(Input.GetKey(KeyCode.S))
            {
                if(transform.position.y > -3f)
                {
                    transform.Translate(Vector3.down * Time.deltaTime * 5f);
                }
            }
        }
    }
}