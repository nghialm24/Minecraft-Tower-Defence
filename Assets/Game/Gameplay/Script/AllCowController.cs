using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCowController : MonoBehaviour
{
    [SerializeField] private GameObject cow;
    [SerializeField] private int count;
    [SerializeField] private int maxX;
    [SerializeField] private int maxZ;
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            var c = Instantiate(cow);
            c.transform.parent = transform;
            c.transform.position = transform.position + new Vector3(Random.Range(-maxX, maxX), 0, Random.Range(-maxZ, maxZ));
            c.transform.eulerAngles = new Vector3(0,Random.Range(0,360),0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
