using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform circle;      // �~��Transform
    public Transform rectangle;   // ��`��Transform

    public float circleRadius;    // �~�̔��a
    public float rectangleWidth;  // ��`�̕�
    public float rectangleHeight; // ��`�̍���

    private void Update()
    {
        // �~�Ƌ�`�̒��S���W���擾
        circleRadius = this.GetComponent<SpriteRenderer>().bounds.size.x;


        rectangleWidth = rectangle.GetComponent<SpriteRenderer>().bounds.size.x;
        rectangleHeight = rectangle.GetComponent<SpriteRenderer>().bounds.size.y;
        Vector3 circleCenter = circle.position;
        Vector3 rectangleCenter = rectangle.position;

        // �~�̒��S���W�Ƌ�`�̒��S���W�̋������v�Z
        float distance = Vector3.Distance(circleCenter, rectangleCenter);

        // �������~�̔��a�����������ꍇ�A�~�Ƌ�`�͌������Ă���Ɣ���
        if (distance < circleRadius)
        {
            Debug.Log("Circle and rectangle intersect!");
        }
    }
}
