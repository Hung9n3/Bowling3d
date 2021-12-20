using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public string ScenetoLoad;
    public GameObject Menu;
    public GameObject Guide;
    public GameObject Credit;
    public GameObject CreatePlayer;
    public Text Player1;
    public Text Player2;
    public GameObject PauseScene;
    // Use this for initialization
    public void loadLevel()
    {
        Debug.Log("Load" + ScenetoLoad);
        SceneManager.LoadScene(ScenetoLoad);
    }
    public void StartGame()
    {
        PlayerPrefs.SetString("Name1", Player1.text);
        PlayerPrefs.SetString("Name2", Player2.text);
        PlayerPrefs.SetInt("Score1", 0);
        PlayerPrefs.SetInt("Player1Turn1", 0);
        PlayerPrefs.SetInt("Player1Turn2", 0);
        PlayerPrefs.SetInt("Player1Turn3", 0);
        PlayerPrefs.SetInt("Score2", 0);
        PlayerPrefs.SetInt("Player2Turn1", 0);
        PlayerPrefs.SetInt("Player2Turn2", 0);
        PlayerPrefs.SetInt("Player2Turn3", 0);
        PlayerPrefs.SetInt("Chances", 6);
        loadLevel();
    }
    void ActiveFalse(GameObject O)
    {
        O.SetActive(false);
    }
    void ActiveTrue(GameObject O)
    {
        O.SetActive(true);
    }
    public void LoadCreatePlayer()
    {
        ActiveTrue(CreatePlayer);
        ActiveFalse(Menu);
    }
    public void LoadCreditCanvas()
    {
        ActiveTrue(Credit);
        ActiveFalse(Menu);
    }
    public void LoadGuideCanvas()
    {
        ActiveTrue(Guide);
        ActiveFalse(Menu);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Back()
    {
        ActiveTrue(Menu);
        ActiveFalse(Credit);
        ActiveFalse(CreatePlayer);
        ActiveFalse(Guide);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void PauseGame()
    {
        PauseScene.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        PauseScene.SetActive(false);
        Time.timeScale = 1;
    }
}
