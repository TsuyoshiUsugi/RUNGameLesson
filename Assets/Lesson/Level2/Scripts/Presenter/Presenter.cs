using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presenter : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] MovePlayer _movePlayer;

    // Start is called before the first frame update
    void Start()
    {
        _playerInput.OnRightButtonClicked += num => _movePlayer.HorizontalControl(num);
        _playerInput.OnLeftButtonClicked += num => _movePlayer.HorizontalControl(num);
    }
}
