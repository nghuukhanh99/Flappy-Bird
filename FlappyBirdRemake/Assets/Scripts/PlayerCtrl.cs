using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    public float velocity;

    private Rigidbody2D rb;

    SpawnManager spawnManager;

    private float yRange = 4.50f;

    private int score = 0;

    private int highScore;

    public Text nowScore;

    public Text highScored;

    public TextMeshProUGUI scoreText;

    int angle;

    int maxAngle = 20;

    int minAngle = -90;

    AudioSource playerAudio;

    public AudioClip wingSound;

    public AudioClip deadSound;

    public AudioClip deadSound2;

    public AudioClip deadSound3;

    public AudioClip checkPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        score = 0;

        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
   

    private void Update()
    {
        OnMouseDown();
        cantMoveOutOfYRange();

        if (spawnManager.isGameActive == true)
        {
            scoreText.gameObject.SetActive(true);
        }

        rotateBird();
        
        //updateScore();
    }


    public void OnMouseDown()
    {

        if (spawnManager.canTouch == true && Input.GetMouseButtonDown(0))
            {
                rb.velocity = Vector2.up * velocity;
                
                rb.constraints = RigidbodyConstraints2D.None;

                playerAudio.PlayOneShot(wingSound, 0.5f);
            }
    }


    private void OnTriggerEnter2D(Collider2D birdCollision)
    {
        if (birdCollision.gameObject.CompareTag("Pipe") || birdCollision.CompareTag("Ground"))
        {
            spawnManager.GameOver();

            playerAudio.PlayOneShot(deadSound, 0.5f);

            playerAudio.PlayOneShot(deadSound2, 0.5f);
            
            playerAudio.PlayOneShot(deadSound3, 0.5f);
            
        }

        if (birdCollision.gameObject.CompareTag("PointTrigger"))
        {
            score++;

            scoreText.text = score.ToString();

            nowScore.text = score.ToString();

            playerAudio.PlayOneShot(checkPoint, 0.5f);
        }
    }

   

    private void cantMoveOutOfYRange()
    {
        if(transform.position.y > yRange)
        {
            transform.position = new Vector3(transform.position.x, yRange, transform.position.z);
        }
    }


    private void rotateBird()
    {
        if(rb.velocity.y > 0)
        {
            if(angle <= maxAngle)
            {
                angle = angle + 4;
            }
        }
        else if(rb.velocity.y < -1.2)
        {
            if (angle >= minAngle)
            {
                angle = angle - 3;
            }
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }




    public void updateScore()
    {

        if (score > highScore)
        {
            highScore = score;
        }

        highScored.text = "" + score;

        if (score < highScore)
        {
            nowScore.text = score.ToString();
        }
    }
}
