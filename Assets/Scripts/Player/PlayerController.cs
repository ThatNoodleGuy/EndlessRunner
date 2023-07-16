using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private float playerSpeed;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private ParticleSystem damageEffectPrefab;

    private float inputX;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (GameManager.instance.GetIsGameEnden())
        {
            return;
        }

        HandleMovement();
    }

    private void HandleMovement()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        transform.position += Vector3.right * inputX * playerSpeed * Time.deltaTime;

        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }

        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Spike>())
        {
            GameManager.instance.TakeDamage();
            Destroy(other.gameObject);
            ParticleSystem damageEffectPaticleSystem = Instantiate(damageEffectPrefab, transform.position, Quaternion.identity);
        }
    }
}