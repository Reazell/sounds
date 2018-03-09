using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameController : MonoBehaviour {

    #region Public Variables
    public GameObject hazard;
    public Vector3 spawnValue;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public AudioMixer audioMixer;
    public AudioMixerSnapshot GameSnap;
    public AudioMixerSnapshot GameOverSnap;
    public AudioMixerSnapshot MenuSnap;


    public static int scoreValue = 0;
    public static int playerHealth = 1;
    #endregion

    #region Private Variables
    private string score = "";
    private string gameOver = "";
    private string reset = "";
    private string author = "";


    private bool isGame;
    private bool isGameOver;
    private bool isMenu;
    #endregion

    #region GUI Variables
    private GUIStyle guiStyle = new GUIStyle();
    private GUIStyle gameOverStyle = new GUIStyle();
    private GUIStyle authorStyle = new GUIStyle();
    
    public Font myFont;

    #endregion



    void Start()
    {
        StartCoroutine(SpawnWaves());
        playerHealth = 1;
        scoreValue = 0;

        isGame = true;
        isGameOver = false;
        isMenu = false;
        #region GeneralGUI Settings
        guiStyle.normal.textColor = Color.white;
        guiStyle.fontSize = 32;
        guiStyle.font = myFont;
        #endregion


        #region gameOverGUI Settings
        gameOverStyle.normal.textColor = Color.red;
        gameOverStyle.fontSize = 64;
        gameOverStyle.font = myFont;
        #endregion
    }

    void Update()
    {
        score = "Score: " + scoreValue;
        if(GameController.playerHealth == 0)
        {
            isGame = false;
            isGameOver = true;
            isMenu = false;
            gameOver = "Game Over!";
            reset = "Press 'R' to RESET";
            author = "By Szymon Przybyszewski";
        }

        if(GameController.playerHealth == 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }

        if(isGame == true)
        {
            GameSnap.TransitionTo(.1f);
        }

        if(isGameOver == true)
        {
            GameOverSnap.TransitionTo(.1f);
        }

        if (isMenu == true)
        {
            MenuSnap.TransitionTo(.1f);
        }

    }


    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 50), score, guiStyle);
        GUI.Label(new Rect(320, 0, 200, 50), reset, guiStyle);
        GUI.Label(new Rect(140, 450, 200, 50), gameOver, gameOverStyle);
        GUI.Label(new Rect(120, 860, 200, 50), author, guiStyle);

    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {

            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
}
