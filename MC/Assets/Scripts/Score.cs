using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform player;
    public Text scoreText;

    // Start is called before the first frame
    void Start()
    {
      //  Debug.Log(player.position);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = (player.position.z + 130.995).ToString("0");
    }
}
