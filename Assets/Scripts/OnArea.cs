using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnArea : MonoBehaviour
{
    public bool isInAreaofAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isInAreaofAttack=true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isInAreaofAttack = false;
        }
    }
}
