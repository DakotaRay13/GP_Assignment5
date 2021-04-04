using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordDespawner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Word")
        {
            collision.GetComponent<WordDisplay>().RemoveWord();
            FindObjectOfType<GameManager>().GameOver();
        }
    }
}
