using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    static Spawner instance;

    void Awake() { instance = this; }

    public static Spawner GetInstance() { return instance; }

    public CamControl panControl;

    public float spawnGap;

    public Transform spawnPoint;

    public Text waveCountdown;

    public SceneFader fader;
    
    public GameObject panelGameWon;

    public Wave[] waves;

    float countDown;

    int countWaves;

    int repelWaves;

    int enemiesAlive;

    public int getRepelWaves()
    {
        return repelWaves;
    }

    void Start()
    {
        countDown = 1.0f;

        waveCountdown.text = "[ " + countDown.ToString("00.00") + " ]";
    }

    void Update()
    {
        if (enemiesAlive > 0) return;

        repelWaves = countWaves;

        if (repelWaves == waves.Length) {

            panControl.freeze();

            panelGameWon.SetActive(true);

            this.enabled = false;

            return;
        }
        countDown -= Time.deltaTime;

        countDown = Mathf.Max(countDown, 0.0f);

        waveCountdown.text = "[ " + countDown.ToString("00.00") + " ]";

        if (countDown <= 0.0f) {

            enemiesAlive = waves[countWaves].count;

            countWaves ++;

        	StartCoroutine(spawnWave());

        	countDown = spawnGap;
        }
    }

    IEnumerator spawnWave()
    {
    	for (int i = 0; i < waves[countWaves - 1].count; i++) {

    		GameObject instance = (GameObject)Instantiate(waves[countWaves - 1].enemy, spawnPoint.position, spawnPoint.rotation);

            instance.GetComponent<Enemy>().death += decrement;

    		yield return new WaitForSeconds(waves[countWaves - 1].gap);
    	}
    }

    public void decrement()
    {
        enemiesAlive --;
    }
}
