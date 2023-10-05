using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{

    public float moveSpeed;

    private MonsterPath thePath.
    private int currentPoint;
    private bool reachedEnd;
    // Start is called before the first frame update
    void Start()
    {

        if(thePath == null)
        {
            thePath = FindObjectOfType<MonsterPath>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(reachedEnd == false)
        {
            transform.LookAt(thePath points[currentPoint]);

            transform.position = Vector3.MoveTowards(transform.position)
                ,thePath points[currentPoint],position, moveSpeed * Time.deltaTime);

            if(Vector3 Distance(transform.position,thePath.points[currentPoint].position) < 0.01f)
            {
                currentPoint += 1;

                 (currentPoint += 1;

                if(current)



            }
        }
        
    }
}
