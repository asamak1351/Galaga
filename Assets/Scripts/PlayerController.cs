using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Starting states")]

    public float speed;
    Vector2 input;
    Rigidbody2D rigid;

    [Header("Shooting")]

    public GameObject bullet;
    public GameObject[] bulletSpawnPositions;
    private float cooldown;
    public float timeBetweenShots;
    public GameObject Flash;
    [Header("Health")]

    public GameObject healthImage;
    public GameObject healthParent;
    public int maxHealth = 10;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        cooldown = timeBetweenShots;
        health = maxHealth;
        for(int i = 0; i < health - 1; i++)
        {
            AddHeart();
        }
    }

    void AddHeart()
    {
        GameObject heart = Instantiate(healthImage);
        heart.transform.SetParent(healthParent.transform);
    }

    void RemoveHeart(int n)
    {
        if (healthParent.transform.childCount > 0)
        {
            if (healthParent.transform.childCount < n)
            {
                n = healthParent.transform.childCount;
            }
            for(int i = 0; i < n; i++)
            {
                Destroy(healthParent.transform.GetChild(0).gameObject);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            rigid.AddForce(input * speed);
        }

        if (Input.GetKey(KeyCode.Space) && cooldown<=0)
            Shoot();
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
    }

    void Shoot()
    {
        
        for(int i = 0; i < bulletSpawnPositions.Length; i++)
        {
            Instantiate(bullet, bulletSpawnPositions[i].transform.position, Quaternion.identity);
        }
        Instantiate(Flash, transform.position, Quaternion.identity);
        cooldown = timeBetweenShots;
    }

    public void TakeDamage(int damage)
    {
        RemoveHeart(damage);
        health = health - damage;
        if (health <= 0)
            GameOver();
    }

    void GameOver()
    {
        FindObjectOfType<GameController>().GameOver = true;
        FindObjectOfType<GameController>().GameOverUI.SetActive(true);
        gameObject.SetActive(false);
        Time.timeScale = 0f;
    }
}
