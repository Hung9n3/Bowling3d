using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int Player1Turn;
    private int Player2Turn;
    public Text Name1;
    public Text Name2;
    private int playChances;
    public Text VoteText;
    private List<string> VoteList;
    public GameObject roundOverCanvas;
    public Text roundOverText;
    public GameObject gameOverCanvas;
    public Text totalScoreText;
    private int currentScore;
    public GameObject ball;
    public GameObject ScoreUI;
    public int score = 0;
    public Vector3 offset;
    public Transform player;
    public Text[] PlayerScore1;
    public Text[] PlayerScore2;
    // Start is called before the first frame update
    void Start()
    {
        Name1.text = PlayerPrefs.GetString("Name1");
        Name2.text = PlayerPrefs.GetString("Name2");
        VoteList = new List<string>(new string[] { "OH NO :(","GOOD ^.^", "GREAT:D", "STRIKE!!!!" });
        ScoreTable();
    }

    
    public void Score()
    {
        currentScore++;
        Debug.Log(currentScore);
    }
    private void ScoreTable()
    {
        for(int i = 0; i < 3; i++)
        {
            string turn = (i + 1).ToString();
            PlayerScore1[i].text = PlayerPrefs.GetInt("Player1Turn" + turn).ToString();
            PlayerScore2[i].text = PlayerPrefs.GetInt("Player2Turn" + turn).ToString();
        }
    }
    void ScoreEachTurn()
    {
        playChances = PlayerPrefs.GetInt("Chances");
        if (playChances % 2 == 0)
        {
            Player1Turn = playChances / 2;
            PlayerPrefs.SetInt("Player1Turn" + Player1Turn.ToString(), currentScore);
            PlayerPrefs.SetInt("Score1", PlayerPrefs.GetInt("Score1") + currentScore);
            PlayerScore1[(playChances / 2) - 1].text = currentScore.ToString();
        }
        else
        {
            Player2Turn = (playChances + 1) / 2;
            PlayerPrefs.SetInt("Player2Turn" + Player2Turn.ToString(), currentScore);
            Debug.Log("Player2Turn" + Player2Turn.ToString() + ":" + PlayerPrefs.GetInt("Player2Turn" + Player2Turn.ToString()));
            PlayerPrefs.SetInt("Score2", PlayerPrefs.GetInt("Score2") + currentScore);
            PlayerScore2[((playChances + 1) / 2) - 1].text = currentScore.ToString();
        }
    }
    //Render Vote Sentence
    void VoteSentence()
    {
        if (currentScore <= 2) VoteText.text = VoteList[0];
        if (currentScore >= 3 && currentScore <= 5) VoteText.text = VoteList[1];
        if (currentScore >= 6 && currentScore <= 9) VoteText.text = VoteList[2];
        if (currentScore == 10) VoteText.text = VoteList[3];
    }
    public void roundFinish()
    {
        playChances = PlayerPrefs.GetInt("Chances");
        ScoreEachTurn();
        playChances -= 1;
        VoteSentence();
        roundOverCanvas.SetActive(true);
        roundOverText.text = currentScore.ToString();
        Invoke("loadLevel", 2f);

    }
    //reset player info
    void ResetPlayerInfo()
    {
        PlayerPrefs.SetInt("Score1", 0);
        PlayerPrefs.SetInt("Player1Turn1", 0);
        PlayerPrefs.SetInt("Player1Turn2", 0);
        PlayerPrefs.SetInt("Player1Turn3", 0);
        PlayerPrefs.SetInt("Score2", 0);
        PlayerPrefs.SetInt("Player2Turn1", 0);
        PlayerPrefs.SetInt("Player2Turn2", 0);
        PlayerPrefs.SetInt("Player2Turn3", 0);
        PlayerPrefs.SetInt("Chances", 6);
    }
    //Detect the winner
    void WhoWon()
    {
        if (PlayerPrefs.GetInt("Score1") > PlayerPrefs.GetInt("Score2"))
        {
            totalScoreText.text = PlayerPrefs.GetString("Name1") + "won";
        }
        else if (PlayerPrefs.GetInt("Score1") == PlayerPrefs.GetInt("Score2"))
        {
            totalScoreText.text = "Draw";
        }
        else totalScoreText.text = PlayerPrefs.GetString("Name2") + "won";
    }
    //Restart Game or next turn
    void loadLevel()
    {
        PlayerPrefs.SetInt("Chances", playChances);
        if (playChances <= 0)
        {
            WhoWon();
            roundOverCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);
            ResetPlayerInfo();
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.T) && turn < 2 ||ball.transform.position.y <= -2.49 && turn < 2)
    //    {
    //        turn++;
    //        CountPinsDown();
    //        ResetPin();
    //    }
    //}
    //public void CountPinsDown()
    //{
    //    for(int i =0; i < pins.Length; i++)
    //    {
    //        if(pins[i].transform.eulerAngles.z > 5 && pins[i].transform.eulerAngles.z < 355 && pins[i].activeSelf)
    //        {
    //            score++;
    //            pins[i].SetActive(false);   
    //        }
    //    }
    //    ScoreUI.SetActive(true);
    //    ScoreValue.text = score.ToString();
    //}
    //void pinReset()
    //{
    //    for (int i = 0; i < pins.Length; i++)
    //    {
    //        pins[i].SetActive(true);
    //        pins[i].transform.position = positions[i];
    //        pins[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
    //        pins[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    //        pins[i].transform.rotation = Quaternion.identity;
    //    }
    //}

    //void ResetBall()
    //{
    //    ball.transform.position = Pos;
    //    ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    //    ball.transform.rotation = Quaternion.identity;
    //    cam.transform.position = player.position + offset;
    //    cam.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    cam.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    //    cam.transform.rotation = Quaternion.identity;
    //}
    //private void OnGUI()
    //{
    //    GUI.skin = scoreSkin;
    //    GUI.Label(new Rect(20, 20, 200, 100), "Score: " + currentScore.ToString());
    //}
    //calculate score of each player after each turn
}
