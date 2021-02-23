using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { Idle, Playing, Ended, Ready };

public class GameController : MonoBehaviour {

    public float parallaxSpeed = 0.02f;
    public RawImage bakcground;
    public RawImage platform;
    public GameObject uiIdle;
    public GameObject uiScore;
    public Text pointsText;
    public Text recordText;

    public GameState gameState = GameState.Idle;

    public GameObject player;

    public GameObject enemyGenerator;

    public float scaleTime = 6f;
    public float scaleInc = .25f;

    private int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        recordText.text = "RÉCORD: " + GetMaxScore().ToString();
    }

    // Update is called once per frame
    void Update()    {

        bool userAction = Input.GetKeyDown("up") || Input.GetMouseButtonDown(0);

        //Empieza el juego
        if (gameState == GameState.Idle && userAction) {
            gameState = GameState.Playing;
            uiIdle.SetActive(false);
            uiScore.SetActive(true);
            player.SendMessage("UpdateState", "PlayerRun");
            enemyGenerator.SendMessage("StartGenerator");
            InvokeRepeating("GameTimeScale", scaleTime, scaleTime);

        }
        //Juego en marcha
        else if (gameState == GameState.Playing) {
            Parallax();
        }

        //Reiniciar juego
        else if (gameState == GameState.Ready)
        {
           if (userAction) {
                RestartGame();
            }
        }

    }

    void Parallax()
    {
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        bakcground.uvRect = new Rect(bakcground.uvRect.x + finalSpeed, 0f, 1f, 1f);
        platform.uvRect = new Rect(platform.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
    }

    public void RestartGame()
    {
        ResetTimeScale();
        SceneManager.LoadScene("SampleScene");
    }

    void GameTimeScale()
    {
        Time.timeScale += scaleInc;
        Debug.Log("Ritmo incrementado a " + Time.timeScale.ToString());
    }

    public void ResetTimeScale(float newTimeScale = 1f)
    {
        CancelInvoke("GameTimeScale");
        Time.timeScale = newTimeScale;
        Debug.Log("Ritmo reiniciado");
    }

    public void IncreasePoints()
    {
        points++;
        pointsText.text = points.ToString();
        if (points >= GetMaxScore())
        {
            recordText.text = "RÉCORD: " + points.ToString();
            SaveScore(points);
        }
    }

    public int GetMaxScore()
    {
        return PlayerPrefs.GetInt("Max Points", 0);
    }

    public void SaveScore(int currentPoints)
    {
        PlayerPrefs.SetInt("Max Points", currentPoints);
    }
}
