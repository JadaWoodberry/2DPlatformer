using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb2d;
    Transform transform;
    bool jump;
    bool hasWeapon = false;
    bool enemyMove = false;
    bool hit = false;
    bool equipt = false;
    private int lives = 4;
    private int xposition;
    private int yposition;
    private int XX;
    private int YY;
    

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        equipt = anim.GetBool("isEquipped");
        xposition = (int)transform.position.x;
        yposition = (int)transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        hit = false;
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 hMove = new Vector2(moveHorizontal, 0);
        rb2d.AddForce(hMove * 5);

        if (System.Math.Abs(moveHorizontal) > 0)
        {
            anim.SetBool("isWalking", true);
        }

        jump = Input.GetKeyDown(KeyCode.Space);
        bool walkLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        bool walkRight = Input.GetKeyDown(KeyCode.RightArrow);

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
      

    }

    // Update End

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            SceneManager.LoadScene("scene2", LoadSceneMode.Single);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isGrounded", true);
            anim.SetBool("isJumping", false);
        }
        if (other.gameObject.CompareTag("collectables"))
        {
            anim.SetBool("isEquipped", true);
            hasWeapon = true;
            Debug.Log("Has weapon: " + hasWeapon);
        }
        if (other.gameObject.CompareTag("enemy"))
        {

            if (xposition > 0)
            {
                XX = (int)-1.5f;

            }
            else if (xposition < -5)
            {
                XX = (int)1.5f;
            }

            if (yposition > 0)
            {
                YY = (int)-1.5f;
            }
            else if (yposition < 0)
            {
                YY = (int)1.5f;
            }

            hit = true;
            Debug.Log("Hit bool: " + hit);
            lives = lives - 1;
            Debug.Log(lives);
            float jumph = xposition;
            float jumpv = yposition;

            transform.position += new Vector3(jumph + XX, jumpv + YY, 0);
           

        }
        else
        {
            hit = false;
            Debug.Log("freeee");
        }
        if (other.gameObject.CompareTag("enemyZone"))
        {
            Debug.Log("In enemy Zone");
            enemyMove = true;
        }
    }

    public bool getEnemyMove()
    {
        return enemyMove;
    }

}


