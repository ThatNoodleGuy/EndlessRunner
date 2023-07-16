using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] private float speedEasy;
    [SerializeField] private float speedHard;

    private float speed;

    private void Start()
    {
        speed = Mathf.Lerp(speedEasy, speedHard, Spawner.instance.GetDifficultyPercent());
    }

    private void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;
    }
}