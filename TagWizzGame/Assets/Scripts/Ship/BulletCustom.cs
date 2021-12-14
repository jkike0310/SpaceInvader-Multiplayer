using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCustom : MonoBehaviour
{
    [SerializeField] 
    private float speed = 200f;

    [SerializeField] 
    private float lifeTime = 5f;
    private int damage;

    internal void DestroySelf()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void Initialize(int damageAmount){
        damage = damageAmount;
    }

    private void Awake()
    {
        Invoke("DestroySelf", lifeTime);
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.up);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy")){
            other.GetComponent<Enemy>().TakeDamage(damage);            
            DestroySelf();
        }   
    }

}
