using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int CharacterDamage = 5;
    public PolygonCollider2D Collider;
    // Start is called before the first frame update
    void Start()
    {
        Collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("attacked");
        if(collision.gameObject.tag == "Enemy")
        {
            EnemyHealth ThisEnemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            ThisEnemyHealth.Damaged = true;
            ThisEnemyHealth.CurrentHealth = ThisEnemyHealth.CurrentHealth - CharacterDamage;
        }
    }

    
}
