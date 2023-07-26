using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private SplineFollower splineFollower;
    [SerializeField] private Image currentHp;
    public int hp;
    private int baseHp;
    
    public void Init(SplineComputer splineComputer)
    {
        splineFollower = GetComponent<SplineFollower>();
        splineFollower.spline = splineComputer;
        splineFollower.enabled = true;
    }

    private void Start()
    {
        baseHp = hp;
    }
    void Update()
    {
        if(hp > 0)
            currentHp.fillAmount = (float) hp / baseHp;
        else
        {
            Destroy(gameObject);
        }
    }
}
