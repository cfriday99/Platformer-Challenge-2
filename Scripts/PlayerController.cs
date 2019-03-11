using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float speed;
    public float jumpForce;
    public Text countText;
    public Text livesText;
    public Text winText;
    public GameObject camera;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    private int count;
    private int lives;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        lives = 3;
        winText.text = "";
        SetCountText();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
    }        
    void Update()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);

        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            if (count == 4)
            {
                transform.position = new Vector2(23.0f, transform.position.y);
            }
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            
            lives = lives - 1;
            if (lives <= 0)
            {
                Destroy(gameObject, .1f);
            }
            SetCountText();
        }
        if (count == 4)
        {
            camera.transform.position = new Vector3(36.0f, 3f, -10f);
            lives = 3;
            SetCountText();
        }
        if (count == 8)
        {
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;
        }
    }
    void SetCountText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            winText.text = "You Lose!";
        }
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            winText.text = "You Win!";
        }
    }

}
