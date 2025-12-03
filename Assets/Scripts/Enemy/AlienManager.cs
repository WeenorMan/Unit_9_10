using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class AlienManager : MonoBehaviour
{
    [Header("Alien Settings")]
    public Alien[] prefabs;
    public int rows = 5;
    public int columns = 6;
    public float speed = 0.0001f;
    public float intervalTimer = 0.0f;

    [Header("Attack Settings")]
    public float missileAttackRate = 1.0f;
    public ProjectileLogic missilePrefab;

    [Header("Game Stats")]
    public int aliensKilled { get; private set; }
    public int amountAlive => this.totalAliens - this.aliensKilled;
    public int waveNumber { get; private set; }
    public int totalAliens => rows * columns;
    public float percentKilled => (float)aliensKilled / (float)totalAliens;

    [Header("Movement Variables")]
    [SerializeField] float direction;
    float rightLimit = 1.9f;
    float leftLimit = -1.9f;
    bool requestDirectionChange;
    List<Alien> alienList = new List<Alien>();
    int alienToMove = 0;

    private void Awake()
    {
        SpawnAliens();
    }

    private void Start()
    {
        requestDirectionChange = false;
        alienToMove = 0;
        speed = 0.04f; 

        InvokeRepeating(nameof(MissleAttack), this.missileAttackRate, this.missileAttackRate);
    }

    private void Update()
    {
        DoMove();
    }
    void SpawnAliens()
    {
        for (int row = 0; row < this.rows; row++)
        {
            float width = 0.6f * (this.columns - 1);
            float height = 0.6f * (this.columns - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);

            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 0.6f), 0.0f);

            for (int col = 0; col < this.columns; col++)
            {
                Alien alien = Instantiate(this.prefabs[row], this.transform);
                alien.killed += AlienKilled;
                Vector3 position = rowPosition;
                position.x += col * 0.6f;
                alien.transform.localPosition = position;
                alienList.Add(alien);
            }
        }
    }

    void DoMove()
    {
        intervalTimer -= Time.deltaTime;
        if (intervalTimer < 0)
        {
            intervalTimer = speed;
        }
        else
        {
            return;
        }

        if (alienList.Count == 0) return;

        Alien alien;
        
        alien = alienList[alienToMove];

        alien.transform.localPosition = new Vector3(
            alien.transform.localPosition.x + direction,
            alien.transform.localPosition.y,
            0
        );

        if (alien.transform.localPosition.x >= rightLimit || alien.transform.localPosition.x <= leftLimit)
        {
            requestDirectionChange = true;
        }

        while(true)
        { 
            alienToMove++;
            if( alienToMove >= alienList.Count )
            {
                break;
            }
            alien = alienList[alienToMove];
            if (alien.gameObject.activeSelf)
            {
                break;
            }
        }

        if (alienToMove >= alienList.Count)
        {
            alienToMove = 0;

            if (requestDirectionChange)
            {
                requestDirectionChange = false;
                direction = -direction;

                foreach (Alien a in alienList)
                {
                    a.transform.localPosition += new Vector3(0, -0.25f, 0);
                }
            }
        }
    }

    private void MissleAttack()
    {
        foreach (Alien a in alienList)
        {
            if (!a.gameObject.activeInHierarchy )
            {
                continue;
            }

            if(Random.value < (1.0f / (float)this.amountAlive))
            {
                Instantiate(this.missilePrefab, a.transform.position, Quaternion.identity);
                break;
            }
        }
    }

    public void AlienKilled()
    {
        aliensKilled++;

        speed *= 0.9f;
        speed = Mathf.Max(0.01f, speed - 0.01f);

        if (aliensKilled >= totalAliens)
        {
            Debug.Log("All aliens killed!");

            foreach (Alien a in alienList)
            {
                if (a != null)
                {
                    Destroy(a.gameObject);
                }
            }
            alienList.Clear();

            waveNumber++;

            speed = 0.04f;
            direction = 0.1f;

            aliensKilled = 0;
            alienToMove = 0;

            SpawnAliens();
        }
    }

    private void OnGUI()
    {
        string text = " ";

        text += "\nFPS:" + 1/Time.deltaTime;

        // define debug text area
        GUI.contentColor = Color.white;
        GUILayout.BeginArea(new Rect(10f, 10f, 1600f, 1600f));
        GUILayout.Label($"<size=24>{text}</size>");
        GUILayout.EndArea();
    }
}
