using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public bool farming;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (farming)
            return;
        if (other.CompareTag("StoneMineral"))
        {
            //transform.LookAt(other.transform);
            //TypeFarm(0);
            farming = true;
        }
        
        if (other.CompareTag("Tree"))
        {
            //transform.LookAt(other.transform);
            //TypeFarm(1);
            farming = true;
        }
        
        if (other.CompareTag("Cow"))
        {
            //transform.LookAt(other.transform);
            //TypeFarm(0);
            farming = true;
        }
        if (other.gameObject == null )
        {
            Debug.Log("no collider");
            //farming = false;
            GetComponent<SphereCollider>().enabled = false;
            Debug.Log("no collider");
        }
    }
    
    private void TypeFarm(int index)
    {
        if(index == 0)
        {
            anim.SetTrigger("Mining");
        }
        else if(index == 1)
        {
            anim.SetTrigger("Chopping");
        }
    }
}
