using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    public string PlayerName = "";
    public int Score = 0;
    public int Speed = 1;

    public GameObject pauseMenu;
    public Text playerNameText;


    private void Start()
    {
        UnpauseGame();

        if(GameData.isLoadGame)
        {
            LoadGame();
        }
        else
        {
            PlayerName = GameData.PlayerName;
            Speed = GameData.Speed;
            SetUI_Text();
        }

        Debug.Log("Speed: " + Speed.ToString());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseMenu.activeSelf == true)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void NewGame()
    {
        GameData.PlayerName = PlayerName;
        GameData.isLoadGame = false;
        GameData.Speed = Speed;
        SceneManager.LoadScene("GameScene");
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");
        GameData.Score = Score;
        SceneManager.LoadScene("EndScene");
    }

    public void SetUI_Text()
    {
        playerNameText.text = PlayerName + ": " + Score.ToString();
    }

    public void IncreaseScore()
    {
        Score += 10 * Speed;
        SetUI_Text();
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        save.PlayerName = PlayerName;
        save.Score = Score;
        save.Speed = Speed;

        return save;
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            PlayerName = save.PlayerName;
            Score = save.Score;
            Speed = save.Speed;
            SetUI_Text();

            Debug.Log("Game Loaded");

            FindObjectOfType<WordManager>().ClearScreen();
            UnpauseGame();
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

    public void SaveAsJSON()
    {
        Save save = CreateSaveGameObject();
        string json = JsonUtility.ToJson(save);

        Debug.Log("Saving as JSON: " + json);
    }
}
