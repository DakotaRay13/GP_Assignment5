using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public InputField nameBox;
    public Slider speedSlider;

    private void Start()
    {
        if(GameData.PlayerName != "")
        {
            nameBox.text = GameData.PlayerName;
        }

        speedSlider.value = GameData.Speed;
    }

    public void NewGame()
    {
        GameData.isLoadGame = false;
        SceneManager.LoadScene("GameScene");
    }

    public void LoadGame()
    {
        GameData.isLoadGame = true;
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NewPlayerName()
    {
        GameData.PlayerName = nameBox.text;
    }

    public void NewSpeed()
    {
        GameData.Speed = (int)speedSlider.value;
    }
}
