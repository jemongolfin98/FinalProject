using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController4 : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float jumpForce1;
    public Text healthText;
    public Text winText;
    public Text countText;
    public Text loseText;
    public Text gameOverText;
    public Text restartText;
    public Text nextLevelText;

    private Rigidbody2D rb2d;
    private int count;
    private int health;
    private bool gameOver;
    private bool restart;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        health = 100;
        winText.text = "";
        loseText.text = "";
        restartText.text = "";
        gameOverText.text = "";
        nextLevelText.text = "";
        SetCountText();
        SetHealthText();
        gameOver = false;
        restart = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if(restart)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Level 5 - The Final Boss");
            }

        }

       
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce(movement * speed);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }

        if (collision.collider.tag == "Platform")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce1), ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();

        }

        if(other.gameObject.CompareTag("PickUp1"))
        {
            if(count >= 49)
            {
                other.gameObject.SetActive(false);
                count = count + 50;
                SetCountText();
            }
            else
            {
                other.gameObject.SetActive(true);
                count = count + 0;
                SetCountText();
            }
        }

        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            health = health - 1;
            SetHealthText();
        }

        if(other.gameObject.CompareTag("Enemy2"))
        {
            if (count >= 99)
            {
                Destroy(other.gameObject);
                count = count + 101;
                health = health - 0;
            }
            else if(health == 0)
            {
                health = health - 0;
            }
            else
            {
                other.gameObject.SetActive(true);
                health = health - 10;
                SetHealthText();
            }
        }
    }

    void SetCountText()
     {
          countText.text = "Cookies: " + count.ToString();

        if (count >= 200)
        {
            winText.text = "You Win!";
            FindObjectOfType<SoundEffects>().WinMusic();
            GameOver();
            if (gameOver)
            {
                gameOverText.text = "Game created by Jemon Golfin";

                restartText.text = "Press 'R' for Level Restart";
                restart = true;
                speed = 0;
                jumpForce = 0;
                jumpForce1 = 0;
            }
        }
     }

    void SetHealthText()
    {
        healthText.text = "Lives: " + health.ToString();

        if(health == 0)
        {
            loseText.text = "You Lose!";
            GameOver();
                if (gameOver)
                {
                    restartText.text = "Press 'R' for Level Restart";
                    restart = true;
                speed = 0;
                jumpForce = 0;
                jumpForce1 = 0;

                }
        }
    }

    public void GameOver()
    {
        if (health == 0)
        {
            gameOverText.text = "Game Over!";
            health = 0;
        }

        gameOver = true;
    }
}
