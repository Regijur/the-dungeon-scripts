using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [Header("Drop")]
    public GameObject ItemDrop;
    public ConsumableItem item;
    
    [Header("Enemy Stats")]
    public int health;
    public int damage;

    [Header("Enemy Follow")]
    public bool detected = false;
    public float speed;
    public float viewField;
    public Vector3 Position;
    public LayerMask playerLayer;

    private Transform player;
    private Rigidbody2D _rigidbody;
    private Animator animator;
    private Vector3 playerDistance;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(!detected)PlayerDetection();
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            if (player.position.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
            }
        }
    }
    
    void PlayerDetection()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position - Position, viewField,playerLayer);
        if(hit != null)
        {
            detected = true;
            animator.SetBool("running", true);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position - Position, viewField);
    }
    public void TakeDamage(int damage)
    {
        detected = true;
        health -= damage;
        if(health <= 0)
        {
            detected = false;
            if(item != null)
            {
                GameObject tempItem = Instantiate(ItemDrop, transform.position, transform.rotation);
                tempItem.GetComponent<ItemDrop>().item = item;
            }
            GetComponent<BoxCollider2D>().enabled = false;
            animator.SetTrigger("dead");
        }
        else
        {
            GetComponent<AudioSource>().Play();
            detected = false;
            _rigidbody.AddForce(Vector2.right * Direction() * 10, ForceMode2D.Impulse);
            StartCoroutine(DamageCoroutine());
        }
    }

    int Direction()
    {
        int direction;
        if (player.position.x < transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        return direction;
    }
    IEnumerator DamageCoroutine()
    {
        for(float i = 0; i < 0.2f; i+= 0.4f)
        {
            _renderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            _rigidbody.velocity = Vector2.zero;
            detected = true;
            _renderer.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public IEnumerator Death()
    {
        _renderer.color = Color.black;
        yield return new WaitForSeconds(0.5f); 
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(damage);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(damage);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }

}
