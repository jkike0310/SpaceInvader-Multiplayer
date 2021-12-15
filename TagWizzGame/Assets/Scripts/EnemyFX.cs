using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyFX : MonoBehaviour
{
    public static EnemyFX instance;    
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private GameObject content;    
    [SerializeField] private int poolSize = 10;
    private Queue<GameObject> poolQueue;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        InitializePool();        
    }

    public void InitializePool()
    {
        poolQueue = new Queue<GameObject>(); 
        for (int i = 0; i < poolSize; i++) 
        {             
            GameObject newParticle = PhotonNetwork.Instantiate("FX_DieEnemy", Vector3.zero, Quaternion.identity);
            poolQueue.Enqueue(newParticle);   
            newParticle.transform.SetParent(content.transform);
            newParticle .SetActive(false);    
        }
    }

    public void ShowParticle(Transform enemyTransform)
    {
        GameObject particleSelected = poolQueue.Dequeue();
        particleSelected.transform.position = enemyTransform.position - new Vector3(0f, 0.2f , 0f);
        particleSelected.SetActive(true);
        particleSelected.GetComponent<ParticleSystem>().Play();
        StartCoroutine(ReturnParticleToPool(particleSelected));
    }

    public IEnumerator ReturnParticleToPool(GameObject particleTarget)
    {
        yield return new WaitForSeconds(2f);
        particleTarget.SetActive(false);
        poolQueue.Enqueue(particleTarget);
    }
    
}
