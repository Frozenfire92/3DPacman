using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Text scoreText;
    public GameObject pelletPrefab;
    public Vector3 minPellet;
    public Vector3 maxPellet;
    public float spawnChance = 0.5f;

    int score;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            score = 0;
            UpdateUI();
            CreatePellets();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ResetScore()
    {
        score = 0;
        UpdateUI();
    }

    public void AddScore()
    {
        score++;
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = score.ToString();
    }

    void CreatePellets()
    {
        for (int y = (int)minPellet.y; y <= (int)maxPellet.y; y++)
        {
            for (int x = (int)minPellet.x; x < (int)maxPellet.x; x++)
            {
                for (int z = (int)minPellet.z; z < (int)maxPellet.z; z++)
                {
                    if (Random.Range(0f, 1f) >= spawnChance)
                    {
                        Instantiate(pelletPrefab, new Vector3(x, y, z), Quaternion.identity);
                    }
                }
            }
        }
    }
}
