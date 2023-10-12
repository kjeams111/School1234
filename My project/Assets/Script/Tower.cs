using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update


    public float range = 3.0f;
    public float fireRate = 1.0f;

    public LayerMask isEnemy;

    public Collider[] colliderinRange;

    public List<EnemyController> enemiesinRange = new List<EnemyController>();

    public float checkCounter;
    public float checkTime = 0.2f;

       public bool enemiesUpdate;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        enemiesUpdate = false;

        checkCounter -= Time.deltaTime;

        if(checkCounter <= 0)
        {
            checkCounter = checkTime;

            colliderinRange = Physics.OverlapSphere(transform.position, range, isEnemy);

            enemiesinRange.Clear();
            foreach(Collider col in colliderinRange)
            {
                enemiesinRange.Add(col.GetComponent<EnemyController>());
            }
            enemiesUpdate = true;

        }
        
    }
}
