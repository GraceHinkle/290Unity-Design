using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    private GameObject enemy;
    void Update()
    {
        if (enemy == null)
        {
            enemy = Instantiate(enemyPrefab) as GameObject;
            enemy.transform.position = new Vector3(7, 0, -10);

            // Randomly set the height of the enemy
            float randomHeight = Random.Range(1.0f, 3.0f);
            enemy.transform.localScale = new Vector3(1, randomHeight, 1);

            enemy.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);

            float angle = Random.Range(0, 360);
            enemy.transform.Rotate(0, angle, 0);
        }
    }
}