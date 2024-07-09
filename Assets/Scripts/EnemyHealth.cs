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

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public float floatUpSpeed = 0.01f;
    public float fadeOutSpeed = 1f;

    public AudioSource deathsfx;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

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
            deathsfx.Play();
            StartCoroutine(DeathAnimation());
        }
    }

    private IEnumerator DeathAnimation()
    {
        float fadeOutProgress = 0f;

        while (fadeOutProgress < 1f)
        {
            transform.position += Vector3.up * floatUpSpeed * Time.deltaTime;
            fadeOutProgress += fadeOutSpeed * Time.deltaTime;
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1 - fadeOutProgress);
            yield return null;
        }

        Destroy(EnemyParent);
    }
}
