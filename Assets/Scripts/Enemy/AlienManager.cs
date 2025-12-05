using System.Collections.Generic;
using UnityEngine;

public class AlienManager : MonoBehaviour
{
    // ---------------------------------------------------------
    // Inspector Settings
    // ---------------------------------------------------------

    [Header("Alien Settings")]
    [Tooltip("Alien prefabs, one per row.")]
    public Alien[] prefabs;

    [Range(1, 10)] public int rows = 5;
    [Range(1, 12)] public int columns = 6;

    [Tooltip("Movement interval, lower = faster.")]
    public float speed = 0.04f;

    private float intervalTimer = 0f;

    [Header("Attack Settings")]
    [Tooltip("Seconds between alien missile attack attempts.")]
    public float missileAttackRate = 1f;

    public ProjectileLogic missilePrefab;

    [Header("Movement Variables")]
    [SerializeField] private float direction = 0.1f;
    private readonly float rightLimit = 1.9f;
    private readonly float leftLimit = -1.9f;

    private bool requestDirectionChange = false;

    // ---------------------------------------------------------
    // Game State
    // ---------------------------------------------------------

    public int aliensKilled { get; private set; }
    public int amountAlive => totalAliens - aliensKilled;
    public int waveNumber; //{ get; private set; }
    public int totalAliens => rows * columns;

    public float percentKilled =>
        totalAliens > 0 ? (float)aliensKilled / totalAliens : 0;

    private List<Alien> alienList = new List<Alien>();
    private int alienToMove = 0;

    // ---------------------------------------------------------
    // Unity Events
    // ---------------------------------------------------------

    private void Awake()
    {
        SpawnAliens();
    }

    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), missileAttackRate, missileAttackRate);
    }

    private void Update()
    {
        DoMove();
    }

    // ---------------------------------------------------------
    // Spawning
    // ---------------------------------------------------------

    private void SpawnAliens()
    {
        alienList.Clear();

        float xSpacing = 0.6f;
        float ySpacing = 0.6f;

        float width = xSpacing * (columns - 1);
        float height = ySpacing * (rows - 1);

        Vector2 offset = new Vector2(-width * 0.5f, -height * 0.5f);

        for (int row = 0; row < rows; row++)
        {
            Vector3 rowPosition = new Vector3(offset.x, offset.y + (row * ySpacing), 0f);

            for (int col = 0; col < columns; col++)
            {
                Alien alien = Instantiate(prefabs[row], transform);
                alien.killed += AlienKilled;

                Vector3 position = rowPosition;
                position.x += col * xSpacing;

                alien.transform.localPosition = position;
                alienList.Add(alien);
            }
        }
    }

    // ---------------------------------------------------------
    // Movement
    // ---------------------------------------------------------

    private void DoMove()
    {
        intervalTimer -= Time.deltaTime;
        if (intervalTimer >= 0) return;

        intervalTimer = speed;
        if (alienList.Count == 0) return;

        // Move currently selected alien
        Alien alien = alienList[alienToMove];

        alien.transform.localPosition += new Vector3(direction, 0, 0);

        // Check for limits
        if (alien.transform.localPosition.x >= rightLimit ||
            alien.transform.localPosition.x <= leftLimit)
        {
            requestDirectionChange = true;
        }

        // Find next active alien
        while (++alienToMove < alienList.Count)
        {
            if (alienList[alienToMove].gameObject.activeSelf)
                break;
        }

        // End of list → reset and possibly descend
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

    // ---------------------------------------------------------
    // Attacks
    // ---------------------------------------------------------

    private void MissileAttack()
    {
        foreach (Alien a in alienList)
        {
            if (!a.gameObject.activeInHierarchy)
                continue;

            if (Random.value < (1f / amountAlive))
            {
                Instantiate(missilePrefab, a.transform.position, Quaternion.identity);
                break;
            }
        }
    }

    // ---------------------------------------------------------
    // Kill & Wave Handling
    // ---------------------------------------------------------

    private void AlienKilled()
    {
        aliensKilled++;

        // Speed ramp-up
        float difficultyFactor = percentKilled * (1f + waveNumber * 0.9f);
        speed = Mathf.Lerp(0.04f, 0.001f, difficultyFactor);


        Debug.Log($"DF: {difficultyFactor} Speed: {speed}");


        if (aliensKilled >= totalAliens)
        {
            Debug.Log("All aliens killed!");

            foreach (Alien a in alienList)
            {
                if (a != null) Destroy(a.gameObject);
            }

            waveNumber++;
            Debug.Log("wave number = " + waveNumber);
            aliensKilled = 0;
            alienToMove = 0;
            missileAttackRate *= 0.9f;
            missileAttackRate = Mathf.Max(0.1f, missileAttackRate);

            speed = 0.04f;
            direction = 0.1f;

            SpawnAliens();
        }
    }
}
