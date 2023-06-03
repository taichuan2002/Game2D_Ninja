using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextMapBoss : MonoBehaviour
{
    public float delaySecond = 2;
    public string nameScene = "MapBoss";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.SetActive(false);  // an? player

            ModeSelect();
        }
    }

    private void ModeSelect()
    {
        StartCoroutine(LoadAfterDelay());
    }

    IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(delaySecond);

        SceneManager.LoadScene(nameScene);
    }
}
