using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{

    [Header("Health UI")]
    [SerializeField] private Image[] heartSlots;
    [SerializeField] private Sprite[] hearts;
    private float healthPerSection;

    [Header("Health Stats")]
    public float currentHealth;
    public float maxHealth;
    public float minHealth;

    [Header("Heal Particle System")]
    public ParticleSystem healPS;
    public Animator anim;

    private void Start()
    {
        healPS.Stop();
        anim.enabled = false;
    }

    private void Update()
    {
        UpdateHearts(currentHealth, maxHealth);
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        healPS.Stop();
        anim.enabled = false;

        if (currentHealth <= 0)
        {
            currentHealth = minHealth;
        }
    }

    public void HealSelf(float hp)
    {
        currentHealth += hp;
        healPS.Play();
        anim.enabled = true;
        anim.SetInteger("HealSelf", 1);

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void UpdateHearts(float currentHealth, float maxHealth)
    {
        int heartSlotIndex = 0;

        healthPerSection = maxHealth / (heartSlots.Length * 4); //calculate how much health each heart slot quarter is worth

        foreach (Image heart in heartSlots)
        {
            if (currentHealth >= healthPerSection * 4 * (heartSlotIndex + 1)) //if current health fills this heart
            {
                heartSlots[heartSlotIndex].sprite = hearts[4]; //display full heart
            }
            else if (currentHealth >= healthPerSection * (4 * heartSlotIndex + 3)) //if current health reaches at least a 3/4 of the current heart
            {
                heartSlots[heartSlotIndex].sprite = hearts[3]; //display 3/4 heart
            }
            else if (currentHealth >= healthPerSection * (4 * heartSlotIndex + 2))
            {
                heartSlots[heartSlotIndex].sprite = hearts[2];
            }
            else if (currentHealth >= healthPerSection * (4 * heartSlotIndex + 1))
            {
                heartSlots[heartSlotIndex].sprite = hearts[1];
            }
            else
            {
                heartSlots[heartSlotIndex].sprite = hearts[0];
            }
            heartSlotIndex++;
        }
    }
}
