using System.Collections;
using TMPro;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;

    public int maxZombies=10;
    public float spawnInterval=3f;

    public TextMeshProUGUI countdownText;
    public Gun gunScript;
    public GameObject gamePanel;
    public TextMeshProUGUI scoreText;
    public GameObject countdownPanel;

    public int score=0;

    [SerializeField] private AudioClip[] sfx;
    [SerializeField] private AudioSource sfxPlayer;

    private float nextSpawnTime;
    private bool gameStarted=false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gamePanel.SetActive(false);
        countdownPanel.SetActive(true);
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        gunScript.enabled=false;

        countdownText.text="3";
        yield return new WaitForSeconds(1f);
        sfxPlayer.PlayOneShot(sfx[0]);

        countdownText.text="2";
        yield return new WaitForSeconds(1f);
        sfxPlayer.PlayOneShot(sfx[0]);

        countdownText.text="1";
        yield return new WaitForSeconds(1f);
        sfxPlayer.PlayOneShot(sfx[0]);

        countdownText.text="BEGIN!";
        yield return new WaitForSeconds(1f);
        sfxPlayer.PlayOneShot(sfx[1]);

        gameStarted=true;

        countdownText.text="";

        gamePanel.SetActive(true);
        countdownPanel.SetActive(false);

        gunScript.enabled=true;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text="Score: " + score;
        
        if(!gameStarted) return;

        if (Time.time >= nextSpawnTime)
        {
            int currentZombies=GameObject.FindGameObjectsWithTag("Enemy").Length;

            if(currentZombies< maxZombies)
            {
                SpawnEnemy();
            }

            nextSpawnTime=Time.time+spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        int randomIndex=Random.Range(0,spawnPoints.Length);
        Transform spawnPoint=spawnPoints[randomIndex];

        Instantiate(zombiePrefab,spawnPoint.position,spawnPoint.rotation);
    }

    public void StopGame()
    {
        gameStarted=false;

        GameObject[] zombies=GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject zom in zombies)
        {
            Destroy(zom);
        }
    }
}
