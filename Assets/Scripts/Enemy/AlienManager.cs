using UnityEngine;

public class AlienManager : MonoBehaviour
{
    public Alien[] prefabs;

    public int rows = 5;
    public int columns = 6;

    private void Awake()
    {
        for (int row = 0; row < this.rows; row++)
        {
            Vector3 rowPosition = new Vector3(-1.21f, row * 0.6f, 0.0f);

            for (int col = 0; col < this.columns; col++)
            {
                Alien alien = Instantiate(this.prefabs[row], this.transform);
                Vector3 position = rowPosition;
                position.x += col * 0.6f;
                alien.transform.localPosition = position;
            }
        }
    }
}
