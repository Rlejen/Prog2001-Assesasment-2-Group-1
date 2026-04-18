using UnityEngine;
using DG.Tweening;
using TMPro;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float leftRightSpeed = 5f;
    [SerializeField] private Vector2 minMaxX;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    [SerializeField] private TextMeshProUGUI winScoreText;
    [SerializeField] private UnityEngine.UI.Slider healthBar;
    [SerializeField] private UnityEngine.UI.Image fillImage;

    [Header("UI & Audio")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private AudioSource pickupAudio;

    [Header("Game Settings")]
    [SerializeField] private float finishZ = 960f;
    [SerializeField] private float meltRate = 0.10f;

    private float h;
    private int score = 0;

    private float health = 1f;
    private bool isGameOver = false;
    private bool hasFinished = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Stop all logic if game ended
        if (isGameOver || hasFinished) return;

        // Input
        h = joystick.Horizontal + Input.GetAxis("Horizontal");
        h = Mathf.Clamp(h, -1f, 1f);

        // Clamp X position
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minMaxX.x, minMaxX.y),
            transform.position.y,
            transform.position.z
        );

        // Melting system
        health -= Time.deltaTime * meltRate;
        transform.localScale = Vector3.one * health;
        healthBar.value = Mathf.Lerp(healthBar.value, health, 5f * Time.deltaTime);
        fillImage.color = Color.Lerp(Color.red, Color.green, health);
        if (health < 0.3f)
        {
            fillImage.color = Color.Lerp(Color.red, Color.white, Mathf.PingPong(Time.time * 5f, 1));
        }

        // Game Over condition
        if (health <= 0.2f)
        {
            GameOver();
        }

        // Finish condition
        if (transform.position.z >= finishZ)
        {
            FinishSequence();
        }
    }

    private void FixedUpdate()
    {
        if (isGameOver || hasFinished) return;

        rb.velocity = new Vector3(
            h * leftRightSpeed,
            rb.velocity.y,
            forwardSpeed
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (isGameOver || hasFinished) return;

        if (other.CompareTag("snow"))
        {
            Destroy(other.gameObject);

            pickupAudio.Play();

            score += 10;
            scoreText.text = "Score: " + score;

            // Increase health
            health = Mathf.Clamp(health + 0.2f, 0.2f, 1f);
            healthBar.value = Mathf.Lerp(healthBar.value, health, 5f * Time.deltaTime);
            fillImage.color = Color.Lerp(Color.red, Color.green, health);

        }

        if (other.CompareTag("gift"))
        {
           
            Destroy(other.gameObject);

            pickupAudio.Play();

            score += 50;
            scoreText.text = "Score: " + score;

        }
    }

    private void GameOver()
    {
        if (hasFinished) return;

        isGameOver = true;

        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        Debug.Log("GAME OVER");

       
        gameOverScoreText.text = "Score: " + score;

        gameOverUI.SetActive(true);

        Time.timeScale = 0.5f;
    }

    private void FinishSequence()
    {
        if (isGameOver) return;

        hasFinished = true;

        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        transform.DORotate(new Vector3(0, 180f, 0), 0.5f);
        transform.DOScale(1.2f, 0.2f).SetLoops(2, LoopType.Yoyo);

        Debug.Log("YOU WIN!");

       
        winScoreText.text = "Score: " + score;

        winUI.SetActive(true);

        Time.timeScale = 0.5f;
    }
}

