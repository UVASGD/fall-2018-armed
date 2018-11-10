using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public readonly int BULLET_LAYER = 13;
    public int bulletDamage;
    public int maxHealth;
    public int currentHealth;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == BULLET_LAYER)
        {
            TakeBulletDamage();
        }
    }

    private void TakeBulletDamage() {
        if (currentHealth - bulletDamage < 0) {
            currentHealth = 0;
            foreach (Transform child in gameObject.transform) {
                child.transform.parent = null;
            }
            Destroy(gameObject);
        } else {
            currentHealth -= bulletDamage;
        }
    }
}
