using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;

    public bool Damaged = false;
    public GameObject HealthBar;
    public Slider HealthSlider;

    public GameObject EnemyParent;
    // Start is called before the first frame update
    void Start()
    {
        HealthBar.SetActive(false);
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Damaged)
        {
            HealthBar.SetActive(true);
        }

        float CurHP = CurrentHealth;
        float MaxHP = MaxHealth;

        HealthSlider.value = CurHP / MaxHP;

        if(CurrentHealth <= 0)
        {
            Destroy(EnemyParent);
        }
    }
}
