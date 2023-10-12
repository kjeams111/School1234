using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTimeTower : MonoBehaviour
{

    private Tower thisTower;

    public GameObject projectTile;
    public Transform firePoin;
    public float timeBetweenShorts = 1f;
    private float shotCounter;

    private Transform target;

    public Transform launcherModel;
    // Start is called before the first frame update
    void Start()
    {
        thisTower = GetComponent<Tower>();
    }

    // Update is called once per frame
    void Update()
    {
        launcherModel.rotation = Quaternion.Slerp(launcherModel.rotation, Quaternion.LookRotation(target.position - transform.position), 5f * Time.deltaTime);

        launcherModel.rotation = Quaternion.Euler(0f, launcherModel.rotation.eulerAngles.y, 0f);

        shotCounter -= Time.deltaTime;

        if(shotCounter <= 0 && target !=null)
        {
            shotCounter = thisTower.fireRate;
            firePoint.LookAt(target);
            Instantiate(projectTile, firePoint.position, firePoint.rotation);
        }

        if(thisTower enemiesUpdate)
       {
            if(thisTower.enemiesinRange.Count > 0)
            {
                float minDistance = thisTower.range + 1;

                foreach (EnemyController enemy in thisTower.enemiesinRange)
                {
                    if (enemny != null)
                    {
                        float distance = Vector3.Distance(transform.position, enemy.transforn.position);

                    if(Distance< minDistance)
                    {
                            minDistance = distance;
                            target = enemy.transform;
                    }
                    
                }
            }
        }
            else
            {
                target = null;
            }

    }


}
