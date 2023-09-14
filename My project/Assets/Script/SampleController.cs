using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Singletone.instance.inscreaseSocre(10);
        GameManager.Instance.inscreaseSocre(15);
    }

   
}
