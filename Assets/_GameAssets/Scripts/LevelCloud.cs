using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCloud : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float JumpForce = 300f;
    [SerializeField] private GameObject _cloud;
    private Rigidbody2D rb;
    private float timer = 0f;
    private float checkInterval = 1f;
    private float lasty;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (_cloud != null)
        {
            float lastcx = transform.position.x;
            float lastcy = transform.position.y;
            for (int i = 0; i < 15; i++)
            {
                lastcy += 8f;
                float minX = Mathf.Max(-6.6f, lastcx - 6.6f);
                float maxX = Mathf.Min(6.6f, lastcx + 6.6f);
                float newX = Random.Range(minX, maxX);
                Vector3 _spawnCloudPos = new Vector3(newX, lastcy, 0);
                Instantiate(_cloud, _spawnCloudPos, Quaternion.identity);
                lastcx = newX;
            }
        }
    }
    void Update()
    {
        float inputx = Input.GetAxis("Horizontal");
        transform.Translate(inputx * speed * Time.deltaTime, 0, 0);
        timer += Time.deltaTime;

        if (timer >= checkInterval)
        {
            if (Mathf.Abs(transform.position.y - lasty) < 0.01f)
            {
                rb.AddForce(Vector2.up * 1f, ForceMode2D.Impulse);
            }

            lasty = transform.position.y;
            timer = 0f;
        }
    }
    public void PlayerCloudJump()
    {

        rb.AddForce(Vector2.up * JumpForce * 2f, ForceMode2D.Impulse);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Win"))
        {
            Time.timeScale = 0;
            //Next Ep;
        }
    }
}
