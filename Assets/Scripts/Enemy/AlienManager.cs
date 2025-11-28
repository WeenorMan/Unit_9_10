using UnityEngine;
using System.Collections.Generic;

public class AlienManager : MonoBehaviour
{
    public Alien[] prefabs;
    public int rows = 5;
    public int columns = 6;

    [SerializeField] float direction = 0.1f;
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
        InvokeRepeating("DoMove", 0.1f, 0.1f);
    }

    void DoMove()
    {

        if (alienList.Count == 0) return;


        Alien alien = alienList[alienToMove];
        alien.transform.localPosition = new Vector3(alien.transform.localPosition.x + direction, alien.transform.localPosition.y, 0);

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
}
