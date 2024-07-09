using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StdEnemy : MonoBehaviour
{
    public bool isPatrol;
    public bool isAggro;
    public bool isInAttack;

    public bool isIdle;
    public bool isAttack;
    public bool isWalk;
    public bool isAttacking;

    public float speed = 1f;

    public int MovementLimit;
    public bool movingLeft;

    public GameObject ParentObj;
    public SpriteRenderer EnemySprite;

    private Vector3 OriginalPosition;
    private float horizontalDirection = -1f;

    public Animator SpriteAnimator;

    private GameObject Player;
    private Transform PlayerPosition;

    public GameObject AreaCollider;
    private OnArea AttackArea;

    public GameObject AttackCollider;

    private PauseState pause;

    // Start is called before the first frame update
    void Start()
    {
        pause = GameObject.FindGameObjectWithTag("PauseState").GetComponent<PauseState>();

        AttackArea = AreaCollider.GetComponent<OnArea>();
        AttackCollider.SetActive(false);
        PlayWalk();
        isPatrol = true;
        isAggro = false;
        isInAttack = false;

        isIdle = false;
        isAttack = false;
        isWalk = true;

        movingLeft = true;
        OriginalPosition = gameObject.transform.position;

        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerPosition = Player.GetComponent<Transform>();
    }


    private void PlayWalk()
    {
        SpriteAnimator.SetTrigger("GoWalk");
    }
    private void PlayIdle()
    {
        SpriteAnimator.SetTrigger("GoIdle");
    }
    private void PlayAttack()
    {
        SpriteAnimator.SetTrigger("GoAttack");
    }

    private IEnumerator AttackingPlayer()
    {
        PlayAttack();
        yield return new WaitForSeconds(0.5f);
        AttackCollider.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        PlayWalk();
        AttackCollider.SetActive(false);
        yield return new WaitForSeconds(1f);
        isAttacking = false;
        isAttack = false;
        PlayWalk();
        isIdle = true;
    }

    // Update is called once per frame
    void Update()
    {
        AggroDetect();

        if (AttackArea.isInAreaofAttack)
        {
            isInAttack = true;
        }
        else
        {
            isInAttack = false;
        }

        if (isInAttack)
        {
            isAggro = false;
            horizontalDirection = 0;

            isAttacking = true;

            if (!isAttack && (isWalk || isIdle))
            {
                isWalk = false;
                isIdle = false;
                isAttack = true;
                
                StartCoroutine(AttackingPlayer());
            }
            
        }

        if (!isAttacking && isPatrol && !isWalk && isIdle)
        {
            isWalk = true;
            isIdle = false;
            isAttack = false;

            PlayWalk();
        }
        
        if (!isAttacking && isInAttack && !isIdle)
        {
            isWalk = false;
            isIdle = true;
            isAttack=false;

            PlayIdle();
        }
        if (movingLeft)
        {
            AreaCollider.transform.localScale = new Vector3(-1,1);
            AttackCollider.transform.localScale = new Vector3(-1,1);
            EnemySprite.flipX = true;
        }
        else
        {
            AreaCollider.transform.localScale = new Vector3(1, 1);
            AttackCollider.transform.localScale = new Vector3(1, 1);
            EnemySprite.flipX = false;
        }
        if (movingLeft && gameObject.transform.position.x <= OriginalPosition.x - MovementLimit)
        {
            horizontalDirection = 1f;
            movingLeft = false;
        }else if(!movingLeft && gameObject.transform.position.x >= OriginalPosition.x + MovementLimit)
        {
            horizontalDirection = -1f;
            movingLeft = true;
        }

        if (isPatrol)
        {
            transform.Translate(new Vector3(horizontalDirection * speed * Time.deltaTime * pause.isPause, 0f, 0f));
        }

        ToPlayerOnAggro();
    }

    private void AggroDetect()
    {
        if(!isInAttack && PlayerPosition.position.x >= OriginalPosition.x - MovementLimit && PlayerPosition.position.x <= OriginalPosition.x + MovementLimit)
        {
            isAggro = true;
        }
        else
        {
            isAggro= false;
        }
    }

    private void ToPlayerOnAggro()
    {
        if (isAggro)
        {
            isPatrol = false;
        }
        else
        {
            isPatrol= true;
        }

        if (isAggro)
        {
            if (PlayerPosition.position.x >= gameObject.transform.position.x)
            {
                horizontalDirection = 1f;
                EnemySprite.flipX = false;
                movingLeft = false;
            }
            else if (PlayerPosition.position.x <= gameObject.transform.position.x)
            {
                horizontalDirection = -1f;
                EnemySprite.flipX = true;
                movingLeft = true;
            }

            transform.Translate(new Vector3(horizontalDirection * speed * Time.deltaTime * pause.isPause, 0f, 0f));
        }
    }
    
}
