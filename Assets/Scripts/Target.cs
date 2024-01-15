using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float maxSpeed = 16;
    private float minSpeed = 12;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;
    
    public int pointValue;
    public ParticleSystem explosionParticle;
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse); 
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
            

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver(); 
        }
    }
    Vector3 RandomForce() 
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque ()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomSpawnPos () {
        return new Vector3(Random.Range(-xRange,xRange),ySpawnPos);
    }
}
