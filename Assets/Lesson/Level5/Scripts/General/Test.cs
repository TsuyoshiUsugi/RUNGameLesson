using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform circle;      // 円のTransform
    public Transform rectangle;   // 矩形のTransform

    public float circleRadius;    // 円の半径
    public float rectangleWidth;  // 矩形の幅
    public float rectangleHeight; // 矩形の高さ

    private void Update()
    {
        // 円と矩形の中心座標を取得
        circleRadius = this.GetComponent<SpriteRenderer>().bounds.size.x;


        rectangleWidth = rectangle.GetComponent<SpriteRenderer>().bounds.size.x;
        rectangleHeight = rectangle.GetComponent<SpriteRenderer>().bounds.size.y;
        Vector3 circleCenter = circle.position;
        Vector3 rectangleCenter = rectangle.position;

        // 円の中心座標と矩形の中心座標の距離を計算
        float distance = Vector3.Distance(circleCenter, rectangleCenter);

        // 距離が円の半径よりも小さい場合、円と矩形は交差していると判定
        if (distance < circleRadius)
        {
            Debug.Log("Circle and rectangle intersect!");
        }
    }
}
