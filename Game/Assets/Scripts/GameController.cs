using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public Vector3 spawnValue;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public static int scoreValue = 0;
    public static int playerHealth = 1;

    public string score = "";
    public string gameOver = "";
    public string reset = "";

    private GUIStyle guiStyle = new GUIStyle();

    

    void Start()
    {
        StartCoroutine(SpawnWaves());
        playerHealth = 1;
        scoreValue = 0;
    }

    void Update()
    {
        score = "Score: " + scoreValue;
        if(GameController.playerHealth == 0)
        {
            gameOver = "Game Over!";
            reset = "Press 'R' to RESET";
        }

        if(GameController.playerHealth == 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }

    }


    void OnGUI()
    {
        guiStyle.fontSize = 26;
        GUI.color = Color.yellow;
        GUI.Label(new Rect(0, 0, 200, 50), score, guiStyle);
        GUI.Label(new Rect(350, 0, 200, 50), reset, guiStyle);
        GUI.Label(new Rect(250, 450, 200, 50), gameOver, guiStyle);

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
