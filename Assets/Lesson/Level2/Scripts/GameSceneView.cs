using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneView : MonoBehaviour
{
    [SerializeField] Text _scoreText;
    // Start is called before the first frame update
    public void ShowScore(int num)
    {
        _scoreText.text = num.ToString();
    }
        
}
