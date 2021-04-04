using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreTable : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    
    private List<Transform> highScoreEntryTransformList;

    private void Awake()
    {
        entryTemplate.gameObject.SetActive(false);

        AddHighScoreEntry(GameData.Score, GameData.PlayerName);

        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

        for (int i = 0; i < highScores.highScoreEntryList.Count; i++)
        {
            for(int j = i+1; j < highScores.highScoreEntryList.Count; j++)
            {
                if(highScores.highScoreEntryList[j].score > highScores.highScoreEntryList[i].score)
                {
                    HighScoreEntry temp = highScores.highScoreEntryList[i];
                    highScores.highScoreEntryList[i] = highScores.highScoreEntryList[j];
                    highScores.highScoreEntryList[j] = temp;
                }
            }
        }

        highScoreEntryTransformList = new List<Transform>();
        foreach(HighScoreEntry highScoreEntry in highScores.highScoreEntryList)
        {
            CreateHighScoreEntry(highScoreEntry, entryContainer, highScoreEntryTransformList);
        }
    }

    private void CreateHighScoreEntry(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -95f * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int Rank = transformList.Count + 1;
        string rankString;
        switch (Rank)
        {
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
            default: rankString = Rank.ToString() + "TH"; break;
        }

        entryTransform.Find("posPlace").GetComponent<Text>().text = rankString;
        entryTransform.Find("posPlayer").GetComponent<Text>().text = highScoreEntry.name;
        entryTransform.Find("posScore").GetComponent<Text>().text = highScoreEntry.score.ToString();

        transformList.Add(entryTransform);
    }

    private void AddHighScoreEntry(int score, string name)
    {
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = score, name = name };

        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores;
        if (jsonString != "") highScores = JsonUtility.FromJson<HighScores>(jsonString);
        else highScores = new HighScores();

        highScores.highScoreEntryList.Add(highScoreEntry);

        for (int i = 0; i < highScores.highScoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highScores.highScoreEntryList.Count; j++)
            {
                if (highScores.highScoreEntryList[j].score > highScores.highScoreEntryList[i].score)
                {
                    HighScoreEntry temp = highScores.highScoreEntryList[i];
                    highScores.highScoreEntryList[i] = highScores.highScoreEntryList[j];
                    highScores.highScoreEntryList[j] = temp;
                }
            }
        }

        if(highScores.highScoreEntryList.Count > 10)
            highScores.highScoreEntryList.RemoveAt(10);

        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highScoreTable"));
    }

    public void ClearHighScores()
    {
        PlayerPrefs.SetString("highScoreTable", "");
        PlayerPrefs.Save();
        SceneManager.LoadScene("EndScene");
    }

    private class HighScores
    {
        public List<HighScoreEntry> highScoreEntryList = new List<HighScoreEntry>();
    }

    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
        public string name;
    }
}
