using UnityEngine;

public class MoverScript : MonoBehaviour
{

    public GameObject[] enemyList;

    int enemyCount;
    int enemyToMove;
    float direction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyCount = enemyList.Length;
        enemyToMove = 0;
        direction = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        DoMove();
        
    }


    void DoMove()
    {
        //X axis = 1.98 and -1.98

        GameObject obj = enemyList[enemyToMove];

        obj.transform.position = new Vector3(obj.transform.position.x + direction, 0, 0);

        //check for enemy reaching far left or far right

        //jump down one line

        enemyToMove++;
        if( enemyToMove > enemyCount )
        {
            enemyToMove = 0;
        }

    }

}
