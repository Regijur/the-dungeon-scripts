using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int stamina = 2;
    public int health = 3;
    public int strength = 1;

    public readonly int maxHealth = 3;
    private bool canDamage = true;

    public void TakeDamage(int Damage)
    {
        if (canDamage)
        {
            canDamage = false;
            health -= Damage;
            if (health <= 0)
            {
                Inventary.inventary.RemoveAllItens();
                StartCoroutine(Death());
            }
            else
            {
                GetComponent<AudioSource>().Play();
                StartCoroutine(DamageCoroutine());
            }
            FindObjectOfType<HealthManager>().SetLife();
        }
    }

    IEnumerator DamageCoroutine()
    {
        for (float i = 0; i < 0.2f; i += 0.4f)
        {
            SpriteRenderer _renderer = GetComponent<SpriteRenderer>();
            _renderer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            _renderer.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }

        canDamage = true;
    }
    IEnumerator Death()
    {
        GetComponent<SpriteRenderer>().color = Color.black;
        GetComponentInChildren<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
