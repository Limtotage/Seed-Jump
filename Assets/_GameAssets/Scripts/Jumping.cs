using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jumping : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float JumpForce = 300f;
    [SerializeField] private GameObject _floor;
    [SerializeField] private GameObject _cloud;
    [SerializeField] private GameObject _end;
    [SerializeField] private bool isTwoGround = false;
    private Rigidbody2D rb;
    private float timer = 0f;
    private float checkInterval = 1f;
    private float lasty;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (isTwoGround)
        {
            float lastx = transform.position.x;
            float lasty = transform.position.y;
            for (int i = 0; i < 30; i++)
            {
                lasty += 4f;
                float minX = Mathf.Max(-6.6f, lastx - 6.6f);
                float maxX = Mathf.Min(6.6f, lastx + 6.6f);
                float newX = Random.Range(minX, maxX);
                Vector3 _spawnPos = new Vector3(newX, lasty, 0);
                Instantiate(_floor, _spawnPos, Quaternion.identity);
                lastx = newX;
            }
        }
        if (_cloud != null)
        {
            float lastcx = transform.position.x;
            float lastcy = transform.position.y;
            lastcy += 4f;
            for (int i = 0; i < 15; i++)
            {
                float minX = Mathf.Max(-6.6f, lastcx - 6.6f);
                float maxX = Mathf.Min(6.6f, lastcx + 6.6f);
                float newX = Random.Range(minX, maxX);
                Vector3 _spawnCloudPos = new Vector3(newX, lastcy, 0);
                Instantiate(_cloud, _spawnCloudPos, Quaternion.identity);
                lastcy += 8f;
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
    public void PlayerJump()
    {

        rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }
    public void PlayerCloudJump()
    {

        rb.AddForce(Vector2.up * JumpForce * 1.5f, ForceMode2D.Impulse);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Win"))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            string currSceneName = SceneManager.GetActiveScene().name;
            if (currSceneName != "Level5")
            {
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
            else
            {
                Time.timeScale = 0f;
                _end.SetActive(true);
            }

        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Danger")
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}
