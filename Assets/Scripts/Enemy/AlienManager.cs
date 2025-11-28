using UnityEngine;
using System.Collections.Generic;

public class AlienManager : MonoBehaviour
{
    public Alien[] prefabs;
    public int rows = 5;
    public int columns = 6;
    public AnimationCurve speed;
    private float currentInterval = 0.1f;
    public int aliensKilled { get; private set; }
    public int totalAliens => rows * columns;
    public float percentKilled => (float)aliensKilled / (float)totalAliens;

    [SerializeField] float direction;
    float rightLimit = 1.9f;
    float leftLimit = -1.9f;
    bool requestDirectionChange;
    List<Alien> alienList = new List<Alien>();
    int alienToMove = 0;

    private void Awake()
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

    private void Start()
    {
        requestDirectionChange = false;
        alienToMove = 0;
        currentInterval = speed.Evaluate(percentKilled);
       // InvokeRepeating("DoMove", currentInterval, currentInterval);
    }

    private void Update()
    {
        DoMove();
    }

    void DoMove()
    {
        if (alienList.Count == 0) return;

        Alien alien = alienList[alienToMove];
        // Move by fixed amount each step
        alien.transform.localPosition = new Vector3(
            alien.transform.localPosition.x + direction,
            alien.transform.localPosition.y,
            0
        );

        if (alien.transform.localPosition.x >= rightLimit || alien.transform.localPosition.x <= leftLimit)
        {
            requestDirectionChange = true;
        }

        alienToMove++;
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

    public void AlienKilled()
    {
        aliensKilled++;

        float newInterval = Mathf.Clamp(speed.Evaluate(percentKilled), 0.02f, 10f);
        if (Mathf.Abs(newInterval - currentInterval) > 0.001f)
        {
            currentInterval = newInterval;
            CancelInvoke("DoMove");
            InvokeRepeating("DoMove", currentInterval, currentInterval);
        }
    }
}
