using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{

    private Rigidbody targetRb;
    private float minSpeed = 13;
    private float maxSpeed = 18;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -6;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;
    public List<ParticleSystem> particleSystems;


    // Start is called before the first frame update

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        targetRb = GetComponent<Rigidbody>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (!gameManager.isGameActive) { return; }
        else if (gameObject.CompareTag("Bad")) { gameManager.GameOver(); }
        Destroy(gameObject);
        changeParticleRandomly();
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        gameManager.UpdateScore(5);
    }

    private void changeParticleRandomly()
    {
        int index = Random.Range(0, particleSystems.Count);
        explosionParticle = particleSystems[index];
    }

    private void OnTriggerEnter(Collider collider)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad")) { gameManager.GameOver(); }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
