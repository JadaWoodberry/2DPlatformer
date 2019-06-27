using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb2d;
    bool jump;
    bool hasWeapon = false;
    bool enemyMove = false;
    bool hit = false;
    bool equipt = false;
    int lives = 4;
    private int xposition;
    private int yposition;
    private int XX;
    private int YY;
    public GameObject blowdryer;
    bool call = false;
    private int ppx;
    private int ppy;
    


    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        equipt = anim.GetBool("isEquipped");
        xposition = (int)transform.position.x;
        yposition = (int)transform.position.y;
        ppx = (int)gameObject.transform.position.x;
        ppy = (int)gameObject.transform.position.y;


    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 targetVelocity = new Vector2(moveHorizontal * 10f, moveVertical * 10f);
        rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

        bool walkLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        bool walkRight = Input.GetKeyDown(KeyCode.RightArrow);

        hit = false;

        if (equipt == true)
        {
            Debug.Log("Equipped: " + equipt );
        }
        if (System.Math.Abs(moveHorizontal) > 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        jump = Input.GetKeyDown(KeyCode.Space);


        if (jump == true)
        {
            anim.SetBool("isJumping", true);
            transform.position += new Vector3(0, 3, 0);

        }

        // you have the position. If your x value is below 0, and you press the right key, flip.
        // if your value is above 0, and you press the right key, flip. 

        if ((transform.localScale.x < 0) && (walkRight == true))
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            anim.SetBool("isEquipped", false);

        }
        if ((transform.localScale.x > 0) && (walkLeft == true))
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            anim.SetBool("isEquipped", false);

        }
        if ((equipt = false) && (hit == false))
        {
            Debug.Log("DROP WEAPON");
        }
        if (lives <= 0)
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene(2);

        }


    }


    // Update End

    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("trigger"))
        {
            gameObject.SetActive(false);

        } else if (other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isGrounded", true);
            anim.SetBool("isJumping", false);
        } else if (other.gameObject.CompareTag("collectables"))
        {
            anim.SetBool("isEquipped", true);
            hasWeapon = true;
            Debug.Log("IS EQUIPPED NOW");
            Debug.Log(anim.GetBool("isEquipped"));

        } else if (other.gameObject.CompareTag("enemy"))
        {
            if ( hasWeapon == true)
            {
                Instantiate(blowdryer, new Vector3(ppx, ppy + 4f, -3f), Quaternion.identity);
                hasWeapon = false;
                
            }

            if (xposition > 0)
            {
                XX = (int)-2;

            }
            else if (xposition < 0)
            {
                XX = (int)2;
            }

            hit = true;
            lives = lives - 1;
            float jumph = xposition;
            float jumpv = yposition;

            transform.position += new Vector3(jumph + XX, jumpv + 1.5f, 0);
        } else if (other.gameObject.CompareTag("enemyZone"))
        {
            Debug.Log("In enemy Zone");
            enemyMove = true;
        }else
        {
            hit = false;
        }
        if (other.gameObject.CompareTag("collectables"))
        {
            anim.SetBool("isEquipped", true);
        }

    }

    public bool getEnemyMove()
    {
        return enemyMove;
    }

    public int getLives()
    {
        return lives;
    }

}


