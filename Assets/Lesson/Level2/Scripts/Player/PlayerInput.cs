using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plyer�̓��͂�InputManager�Ō��m����N���X
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
    /// ���E���������m����֐�
    /// </summary>
    private void HorizontalInput()
    {
        //�E
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            OnRightButtonClicked(1);
        }
        //��
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            OnLeftButtonClicked(-1);
        }
    }
}
