using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plyerの入力をInputManagerで検知するクラス
/// </summary>
public class PlayerInput : MonoBehaviour
{
    public event Action<int> OnRightButtonClicked;
    public event Action<int> OnLeftButtonClicked;

    // Update is called once per frame
    void Update()
    {
        //HorizontalInput();
    }

    private void FixedUpdate()
    {
        HorizontalInput();
    }

    /// <summary>
    /// 左右方向を検知する関数
    /// </summary>
    private void HorizontalInput()
    {
        //右
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            OnRightButtonClicked(1);
        }
        //左
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            OnLeftButtonClicked(-1);
        }
    }
}
