using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    Rigidbody2D playerRB;
    Animator playerAnimator;
    public float moveSpeed = 1f, groundCheckRadius, health, bulletSpeed;
    public float jumpSpeed = 1f, jumpFrequency = 1f, nextJumpTime;
    Transform muzzle;
    public Transform groundCheckPosition, bullet, floatingText, bloodParticle;
    public LayerMask groundCheckLayer;
    public Slider slider;
    public bool isGrounded = false;
    bool facingRight = true, mouseIsNotOverUI;

    void Awake()
    {

    }
    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        muzzle = transform.GetChild(1);
        slider.maxValue = health;
        slider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMove();
        OnGroundCheck();

        if(playerRB.velocity.x < 0 && facingRight)
        {
            FlipFace();
        }
        else if (playerRB.velocity.x > 0 && !facingRight)
        {
            FlipFace();
        }

        if(Input.GetAxis("Vertical") > 0 && isGrounded && nextJumpTime < Time.timeSinceLevelLoad)
        {
            nextJumpTime = Time.timeSinceLevelLoad + jumpFrequency;
            Jump();   
        }

        mouseIsNotOverUI = EventSystem.current.currentSelectedGameObject == null;

        if (Input.GetMouseButtonDown(0) && mouseIsNotOverUI)
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {

    }

    void HorizontalMove()
    {  
        playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, playerRB.velocity.y);
        playerAnimator.SetFloat("playerSpeed", Mathf.Abs(playerRB.velocity.x));
    }

    void FlipFace()
    {
        facingRight = !facingRight;
        Vector3 tempLocalScale = transform.localScale;
        tempLocalScale.x *= -1;
        transform.localScale = tempLocalScale;
    }

    void Jump()
    {
        playerRB.AddForce(new Vector2(0f, jumpSpeed));
    }

    void OnGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer);
        playerAnimator.SetBool("isGroundedAnim", isGrounded);
    }

    public void GetDamage(float damage)
    {
        Instantiate(floatingText, transform.position, Quaternion.identity).GetComponent<TextMesh>().text = damage.ToString();

        if (health - damage > 0)
        {
            health -= damage;
        }
        else
        {
            health = 0;
        }

        slider.value = health;
        AmIDead();
    }

    void AmIDead()
    {
        if(health <= 0)
        {
            Destroy(Instantiate(bloodParticle, transform.position, Quaternion.identity), 2f);
            Destroy(gameObject);
            DataManager.Instance.LoseProcess();
        }
    }

    void Shoot()
    {
        Transform tempBullet;
        tempBullet = Instantiate(bullet, muzzle.position, Quaternion.identity);
        tempBullet.GetComponent<Rigidbody2D>().AddForce(muzzle.forward * bulletSpeed);
        DataManager.Instance.ShootBullet++;
    }
 
}
