using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public int maxHeath = 5;
    public float timeInvincible = 2f;

    private bool isInvincible;
    private float invincibleTimer;

    private int heath;
    private Rigidbody2D rigidbody2d;
    private int speed = 3;

    private float horizontal;
    private float vertical;

    private Animator animator;
    private Vector2 lookDir = new Vector2(1, 0);

    public GameObject projectilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        heath = maxHeath;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0) || !Mathf.Approximately(move.y, 0))
        {
            lookDir.Set(move.x, move.y);
            lookDir.Normalize();
        }
        
        animator.SetFloat("Look X", lookDir.x);
        animator.SetFloat("Look Y", lookDir.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
                isInvincible = false;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x += horizontal * speed * Time.deltaTime;
        position.y += vertical * speed * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDir, 300);

        animator.SetTrigger("Launch");
    }

    public void ChangeHeath(int amount)
    {
        heath = Mathf.Clamp(heath + amount, 0, maxHeath);
        Debug.Log("heath:" + heath + "/" + maxHeath);
    }

    public void TakeDamage(int amount)
    {
        Debug.Log("take dame");
        if (!isInvincible)
        {
            heath -= amount;
            activeInvincible();
            animator.SetTrigger("Hit");
            Debug.Log("heath:" + heath + "/" + maxHeath);
        }
    }

    public void activeInvincible()
    {
        isInvincible = true;
        invincibleTimer = timeInvincible;
    }

    public bool CheckNeedHeath()
    {
        return (heath < maxHeath);
    }
}