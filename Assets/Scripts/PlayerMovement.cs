using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    public float jumpForce = 3f;

    private Rigidbody2D rb;

    public PolygonCollider2D Collider;

    private SpriteRenderer spriteRenderer;

    private bool isWalk = false;
    private bool isIdle = true;

    public GameObject AttackCollider;

    private PauseState pause;

    public AudioSource slash;
    public AudioSource jump;

    void Start()
    {
        pause = GameObject.FindGameObjectWithTag("PauseState").GetComponent<PauseState>();

        // Ambil komponen rigidbody dari objek player
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    #region AnimationHandler
    private Animator animator;
    private void PlayWalk()
    {
        animator.SetTrigger("GoWalk");
    }
    private void PlayJump()
    {
        animator.SetTrigger("GoJump");
    }
    private void PlayIdle()
    {
        animator.SetTrigger("GoIdle");
    }
    private void PlayAttack()
    {
        animator.SetTrigger("GoAttack");
    }
    #endregion

    //sprite flip ini berguna untuk mengubah hadapan player
    private void SpriteFlip(float horizontalInput)
    {
        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
            AttackCollider.transform.localScale = new Vector3(1, AttackCollider.transform.localScale.y);
        }
        else if(horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
            AttackCollider.transform.localScale = new Vector3(-1, AttackCollider.transform.localScale.y);
        }
    }

    float horizontalInput;

    bool isAttacking = false;
    bool isAnimAttack = false;

    public void OnAttack()
    {
        Collider.enabled = true;
    }
    public void OffAttack()
    {
        Collider.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isAttacking)
        {
            slash.Play();
            isAttacking = true;
            
            StartCoroutine(CDAttack());
        }

        
        horizontalInput = Input.GetAxis("Horizontal");

        if (isAttacking)
        {
            horizontalInput = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.001f && !isAttacking)
        {
            jump.Play();
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            
            PlayJump();
            isIdle = false;
            isWalk = false;
        }

        if(horizontalInput == 0)
        {
            if (!isIdle && Mathf.Abs(rb.velocity.y) < 0.001f && !isAttacking)
            {
                PlayIdle();
                isIdle = true;
                isWalk = false;
            }
        }

        if(horizontalInput != 0 && !isWalk && Mathf.Abs(rb.velocity.y) < 0.001f && !isAttacking)
        {
            PlayWalk();
            isWalk = true;
            isIdle = false;
        }
    }

    public IEnumerator CDAttack()
    {
        Invoke("OnAttack", 0.1f);
        Invoke("OffAttack", 0.15f);
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        isAnimAttack = false;
        PlayIdle();
        isIdle = true;
        isWalk = false;
    }

    void FixedUpdate()
    {
        // Menggerakan player ke kanan atau kiri menggunakan transform.translate
        
        transform.Translate(new Vector3(horizontalInput * speed * Time.deltaTime * pause.isPause, 0f, 0f));
        SpriteFlip(horizontalInput);

        if (isAttacking && !isAnimAttack)
        {
            PlayAttack();
            isAnimAttack = true;
            isIdle = false;
        }
        // Mengaktifkan lompatan player jika player menyentuh tanah
        
    }
}
