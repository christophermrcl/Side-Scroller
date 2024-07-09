using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public int Number;
    public bool Activated = false;

    public AudioSource sfx;

    private Animator animator;
    private CheckPointMng mng;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        mng = gameObject.GetComponentInParent<CheckPointMng>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Activated == true)
        {
            animator.SetFloat("on", 0.5f);
        }
        else
        {
            animator.SetFloat("on", 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Activated == false)
        {
            sfx.Play();
            Activated = true;
            mng.gameData.SavedPos = collision.gameObject.transform.position;
            mng.gameData.CheckPointNo = Number;
        }
    }

}
