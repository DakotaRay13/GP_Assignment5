using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInput : MonoBehaviour
{
    public WordManager wordManager;
    
    // Update is called once per frame
    void Update()
    {
        foreach(char letter in Input.inputString)
        {
            if(FindObjectOfType<GameManager>().pauseMenu.activeSelf == false)
                wordManager.TypeLetter(letter);
        }
    }
}
