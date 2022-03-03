using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rigid;
    PlayerController Player;
    public float xSpeed, ySpeed;
    GameController cont;
    public int amount;
    Vector2 bounds;

    [Header("Bullet Config")]
    public GameObject bullet;
    public GameObject bulletspawn;
    public float timeBetweenAttackLow = .5f;
    public float timeBetweenAttackHigh = 2f;

    float attackCools;

    [Header("Enemy Health")]
    public int maxHealth = 2;
    public int enemyHealth;
    public GameObject Explosion;



    // Start is called before the first frame update
    void Start()
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        rigid = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<PlayerController>();
        attackCools = Random.Range(timeBetweenAttackLow, timeBetweenAttackHigh);
        enemyHealth = maxHealth;
        cont = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0f;
        if (Player != null)
        {
            if (Player.transform.position.x > transform.position.x) //enemy is on the left
            {
                x = xSpeed;
            }
            else if (Player.transform.position.x < transform.position.x)
            {
                x = -xSpeed;
            }
        }
        rigid.AddForce(new Vector2(x, -ySpeed) * Time.deltaTime);

        if (attackCools > 0)
            attackCools -= Time.deltaTime;
        else
            Attack();
    
        if(transform.position.y< -bounds.y)
        {
            cont.AddScore(-amount);
            Destroy(gameObject);
        }
    }

    void Attack()
    {
        Instantiate(bullet, bulletspawn.transform.position, transform.rotation);
        attackCools = Random.Range(timeBetweenAttackLow, timeBetweenAttackHigh);

    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
        cont.AddScore(amount);
        Instantiate(Explosion, transform.position, Quaternion.Euler(0, 0, 0));
    }

}
