using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct MyCollision
{
    /// <summary>
    /// ���݂�����`�̎��̐ڐG���m�֐�
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

            Debug.Log($"xdir = {xDir} widthSum = {widthSum} ydir = {yDir} heithtSum = {heightSum}");
            if (xDir <= widthSum && yDir <= heightSum)
            {
                   hitObj.Add(obj);
            }
        }

        return hitObj;
    }

    /// <summary>
    /// ���I�W�F�N�g���ۂœ�����I�u�W�F�N�g����`�̎��̐ڐG����
    /// �����ł͉~�̔��a�̂Ȃ��ɋ�`�̉~�Ɉ�ԋ߂����W�����邩�ǂ����Ŕ��肵�Ă���
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

            //�����ŉ~�̍��W�����`�̍��W�������Ă���̂́A��`����̑��Έʒu�����߂邽��
            var circleLocalPos = myObject.transform.position - otherObjPos;

            var myObjWidth = myObject.GetComponent<SpriteRenderer>().bounds.size.x;
            var width = obj.GetComponent<SpriteRenderer>().bounds.size.x;
            var height = obj.GetComponent<SpriteRenderer>().bounds.size.y;

            var halfWidth = width / 2;
            var halfheight = height / 2;

            //��`��X�̍ŒZ���W
            var nearestX = Mathf.Clamp(circleLocalPos.x, -halfWidth, halfWidth);
            //��`��Y�̍ŒZ���W
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
}
