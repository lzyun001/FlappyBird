using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public Vector2 position;
    public float jumpHeight;
    public int Score;
    public bool isStart;
    public bool isDead;

    public Player(float jumpHeight)
    {
        this.jumpHeight = jumpHeight;
    }
}

public class PlayerControl : MonoBehaviour
{
    public UIManager uiManager;
    public Player Instance;
    public AudioSource Audio_PassBlock;
    public Animator ani;
    public GameManager gm;
    private Rigidbody2D rb;
    private Vector2 jumpForce;
    private Vector2 playerInitPosition;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Init();
        playerInitPosition = gameObject.transform.position;
        jumpForce = new Vector2(0, Instance.jumpHeight);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Init()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Instance = new Player(300);
    }
    // Update is called once per frame
    void Update()
    {
        print(Instance.isStart);
        if (Input.anyKeyDown && !Instance.isStart)
        {
            GameStart();
        }

        if (Instance.isStart && !Instance.isDead)
        {
            gameObject.transform.Translate(new Vector2(0.08f, 0));
            if (Input.anyKeyDown)
            {
                rb.AddForce(jumpForce);
                ani.SetTrigger("Jump");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            PlayerScoreAdd(1);
            Audio_PassBlock.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D obstacle)
    {
        if (obstacle.gameObject.tag == "Obstacle")
        {
            Dead();
        }
    }

    private void GameStart()
    {
        uiManager.StopAllCoroutines();
        uiManager.Text_Start.enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        Instance.isStart = true;
    }

    public void PlayerScoreAdd(int score)
    {
        Instance.Score += score;
        uiManager.Text_PlayerScore.text = "Score: " + Instance.Score;
    }

    public void Dead()
    {
        ShowScoreBoard();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Instance.isDead = true;
        gm.StopAllCoroutines();
    }

    private void ShowScoreBoard()
    {
        uiManager.ScoreBoard.SetActive(true);
        uiManager.Text_ScoreEnd.text = "Your Score is :" + Instance.Score;
    }

    public void Restart()
    {
        Init();
        gameObject.transform.position = playerInitPosition;
        uiManager.Text_PlayerScore.text = "Score: " + Instance.Score;
    }
}
