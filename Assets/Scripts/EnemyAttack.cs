using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int EnemyDmg = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealthSystem PlayerHP = collision.gameObject.GetComponent<PlayerHealthSystem>();
            PlayerHP.Damaged();
            PlayerHP.PlayerCurrentHealth = PlayerHP.PlayerCurrentHealth - EnemyDmg;
        }
    }
}
