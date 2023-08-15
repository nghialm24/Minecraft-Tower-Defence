using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ObstacleAds : MonoBehaviour
{
    [SerializeField] private RectTransform rectTfAds;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image img;
    void Start()
    {
        //var p = CanvasPositioningExtensions.WorldToCanvasPosition(canvas, transform.position);
        //rectTfAds.localPosition = new Vector2(p.x,p.y+200);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        var tempColor = img.color;
        tempColor.a = 1f;
        img.color = tempColor;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        var tempColor = img.color;
        tempColor.a = 0f;
        img.color = tempColor;
    }
}
