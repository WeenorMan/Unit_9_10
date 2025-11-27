using UnityEngine;

public class MoverScript : MonoBehaviour
{

    public GameObject[] enemyList;
    public int enemyCount;
    int enemyToMove;
    [SerializeField] float direction;
    float rightLimit = 1.9f;
    float leftLimit = -1.9f;
    bool requestDirectionChange;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyCount = enemyList.Length;
        enemyToMove = 0;
        requestDirectionChange = false;
        InvokeRepeating("DoMove", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        //DoMove();
        
    }


    void DoMove()
    {
        print("moving enemy " + enemyToMove);

        GameObject obj = enemyList[enemyToMove];

        obj.transform.position = new Vector3(obj.transform.position.x + direction, obj.transform.position.y, 0);

        //check for enemy reaching far left or far right

        if (obj.transform.position.x >= rightLimit || obj.transform.position.x <= leftLimit)
        {
            requestDirectionChange = true;
        }

        enemyToMove++;
        if( enemyToMove >= enemyCount )
        {
            enemyToMove = 0;

            if (requestDirectionChange == true)
            {
                requestDirectionChange = false;
                direction = -direction;

                //jump down one line
                foreach (GameObject enemy in enemyList)
                {
                    enemy.transform.position += new Vector3(0, -0.25f, 0);
                }
            }
        }

       

        

    }

    

    

}
