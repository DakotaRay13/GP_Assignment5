using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordDisplay : MonoBehaviour
{
    public Text text;

    public float fallSpeed = 1f;

    public void Start()
    {
        switch (FindObjectOfType<GameManager>().Speed)
        {
            default: fallSpeed = 1f; break;
            case 1: fallSpeed = 1f; break;
            case 2: fallSpeed = 3f; break;
            case 3: fallSpeed = 5f; break;
        }
    }

    private void Update()
    {
        transform.Translate(0f, -fallSpeed * Time.deltaTime, 0f);
    }

    public void SetWord(string word)
    {
        text.text = word;
    }

    public void RemoveLetter()
    {
        text.text = text.text.Remove(0, 1);
        text.color = Color.red;
    }

    public void RemoveWord()
    {
        Destroy(gameObject);
    }
}
