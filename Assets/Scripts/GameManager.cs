using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PlayerLoop;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float waveNumber;
    public GameObject enemyWave;

    public TMP_Text waveText;
    void Start()
    {
        waveText.text = "wssave: " + waveNumber.ToString();
        SpawnNewWave();
    }

    public void SpawnNewWave() {
        waveNumber++;
        waveText.text = "wave: " + waveNumber.ToString();
        Instantiate(enemyWave, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void GameOver() {
        SceneManager.LoadScene("MainMenu");
    }
}
