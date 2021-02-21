using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { Idle, Playing, Ended };

public class GameController : MonoBehaviour {

    public float parallaxSpeed = 0.02f;
    public RawImage bakcground;
    public RawImage platform;
    public GameObject uiIdle;

    public GameState gameState = GameState.Idle;

    public GameObject player;

    public GameObject enemyGenerator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()    {

        //Empieza el juego
        if (gameState == GameState.Idle && (Input.GetKeyDown("up") || Input.GetMouseButtonDown(0))) {
            gameState = GameState.Playing;
            uiIdle.SetActive(false);
            player.SendMessage("UpdateState", "PlayerRun");
            enemyGenerator.SendMessage("StartGenerator");

        }
        //Juego en marcha
        else if (gameState == GameState.Playing) {
            Parallax();
        }

        //Juego en marcha
        else if (gameState == GameState.Ended)
        {
            //TOODO
        }

        void Parallax()
        {
            float finalSpeed = parallaxSpeed * Time.deltaTime;
            bakcground.uvRect = new Rect(bakcground.uvRect.x + finalSpeed, 0f, 1f, 1f);
            platform.uvRect = new Rect(platform.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
        }
    }
}
