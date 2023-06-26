using System.Collections.Generic;
using UnityEngine;

public class MyCollision
{
    /// <summary>
    /// お互いが矩形の時の接触検知関数
    /// </summary>
    /// <param name="otherObjects"></param>
    public static List<GameObject> CollisionEnter(GameObject myObject , List<GameObject> otherObjects)
    {
        if (otherObjects == null) return null;

        var hitObj = new List<GameObject>();

        foreach (var obj in otherObjects)
        {
            if (obj == null) continue;

            var otherObjPos = obj.transform.position;

            var myObjWidth = myObject.GetComponent<Transform>().localScale.x;
            var myObjHeight = myObject.GetComponent<Transform>().localScale.y;
            var width = obj.GetComponent<Transform>().localScale.x;
            var height = obj.GetComponent<Transform>().localScale.y;

            var xDir = Mathf.Abs(myObject.transform.position.x - otherObjPos.x);
            var yDir = Mathf.Abs(myObject.transform.position.y - otherObjPos.y);

            var widthSum = Mathf.Abs(myObjWidth - width);
            var heightSum = Mathf.Abs(myObjHeight - height);

            if (xDir <= widthSum && yDir <= heightSum)
            {
                   hitObj.Add(obj);
            }
        }

        return hitObj;
    }

    /// <summary>
    /// 自オジェクトが丸で当たるオブジェクトが矩形の時の接触判定
    /// ここでは円の半径のなかに矩形の円に一番近い座標があるかどうかで判定している
    /// </summary>
    /// <param name="otherObjects"></param>
    public static List<GameObject> CircleCollision(GameObject myObject, List<GameObject> otherObjects)
    {
        if (otherObjects == null) return null;

        var hitObj = new List<GameObject>();

        foreach (var obj in otherObjects)
        {
            if (obj == null) continue;

            var otherObjPos = obj.transform.position;

            //ここで円の座標から矩形の座標を引いているのは、矩形からの相対位置を求めるため
            var circleLocalPos = myObject.transform.position - otherObjPos;

            var myObjWidth = myObject.GetComponent<SpriteRenderer>().bounds.size.x;
            var width = obj.GetComponent<SpriteRenderer>().bounds.size.x;
            var height = obj.GetComponent<SpriteRenderer>().bounds.size.y;

            var halfWidth = width / 2;
            var halfheight = height / 2;

            //矩形のXの最短座標
            var nearestX = Mathf.Clamp(circleLocalPos.x, -halfWidth, halfWidth);
            //矩形のYの最短座標
            var nearestY = Mathf.Clamp(circleLocalPos.y, -halfheight, halfheight);

            float distanceX = circleLocalPos.x - nearestX;
            float distanceY = circleLocalPos.y - nearestY;
            float distance = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);

            if (distance < myObjWidth / 2)
            {
                hitObj.Add(obj);
            }

        }
        return hitObj;
    }

    /// <summary>
    /// レーザー銃用の当たり判定クラス
    /// 線分と線分の各辺の交差判定を行う
    /// </summary>
    /// <param name="myObject"></param>
    /// <param name="otherObjects"></param>
    /// <returns></returns>
    public static bool CrossLineCollision(Vector2 lineStart, Vector2 lineEnd, Vector2 line2Start, Vector2 line2End)
    {  
        Vector2 line = lineEnd - lineStart;
        Vector2 line2 = line2End - line2Start;

        return LineAndLine(line, line2Start - lineStart) * LineAndLine(line, line2End - lineStart) < 0 &&
               LineAndLine(line2, lineStart - line2Start) * LineAndLine(line2, lineEnd - line2Start) < 0;
    }

    /// <summary>
    /// 二次元ベクトルの外積を返す(本当は外積は二次元ベクトルでは定義されない)
    /// ここではZ成分の値を交差しているか判定するのに使うためにZの値を返す
    /// </summary>
    /// <param name="line1"></param>
    /// <param name="line2"></param>
    /// <returns></returns>
    static float LineAndLine(Vector2 line1, Vector2 line2)
    {
        return line1.x * line2.y - line1.y * line2.x;
    }
}
