using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    public int PlayerCurrentHealth;
    public int PlayerMaxHealth = 100;

    public GameObject Player;
    private SpriteRenderer PlayerSprite;

    public Slider HPBar;
    // Start is called before the first frame update
    void Start()
    {
        PlayerSprite = Player.GetComponent<SpriteRenderer>();
        PlayerCurrentHealth = PlayerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float currHP = PlayerCurrentHealth;
        float maxHP = PlayerMaxHealth;
        HPBar.value = currHP/maxHP;
    }
    public void Damaged()
    {
        StartCoroutine(ChangeColor());
    }
    private IEnumerator ChangeColor()
    {
        PlayerSprite.color = new Color(255, 255, 255, 0);
        yield return new WaitForSeconds(0.15f);
        PlayerSprite.color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(0.1f);
        PlayerSprite.color = new Color(255, 255, 255, 0);
        yield return new WaitForSeconds(0.05f);
        PlayerSprite.color = new Color(255, 255, 255, 255);
    }
}
