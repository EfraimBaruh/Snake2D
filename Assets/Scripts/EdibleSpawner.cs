using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject edible;
    [SerializeField] private float spawnTime;

    private Camera mainCamera;

    private ScoreHandler scoreHandler;
    
    void Start()
    {
        mainCamera = Camera.main;
        scoreHandler = FindObjectOfType<ScoreHandler>();
        StartCoroutine(SpawnEdible());
    }


    private IEnumerator SpawnEdible()
    {
        GameObject edible = Instantiate(this.edible, Utils.GetRandomPosition(mainCamera), Quaternion.identity);
        edible.GetComponent<EdibleLifeSpan>().EdibleLife = spawnTime;
        edible.tag = Utils.GetEdibleTag(scoreHandler.ScoreValue);

        yield return new WaitForSeconds(spawnTime);
        
        // if not eaten in time, then edible disappers.
        Destroy(edible);
        StartCoroutine(SpawnEdible());
    }

    public void Respawn()
    {
        StopAllCoroutines();
        StartCoroutine(SpawnEdible());
    }
    private void OnEnable()
    {
        Snake.Singleton.onSnakeEates += Respawn;
    }
    
    private void OnDisable()
    {
        Snake.Singleton.onSnakeEates -= Respawn;
    }
}
