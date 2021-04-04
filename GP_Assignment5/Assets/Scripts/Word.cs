using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word
{
    public string word;
    private int TypeIndex;

    private WordDisplay display;

    public Word (string _word, WordDisplay _display)
    {
        word = _word;
        TypeIndex = 0;

        display = _display;
        display.SetWord(word);
    }

    public char GetNextLetter()
    {
        return word[TypeIndex];
    }

    public void TypeLetter()
    {
        TypeIndex++;
        display.RemoveLetter();
    }

    public bool WordTyped()
    {
        bool wordTyped = (TypeIndex >= word.Length);
        if(wordTyped)
        {
            display.RemoveWord();
        }
        return wordTyped;
    }
}
