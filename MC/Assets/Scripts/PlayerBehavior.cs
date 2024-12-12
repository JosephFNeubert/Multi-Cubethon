using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerBehavior : MonoBehaviourPun
{
    // Game Manager
    public GameBehavior gameManager;

    // Declare the Rigidbody component for the physics system
    public Rigidbody _rb;

    // Player variables
    private bool movement = true;
    public float forwardForce = 6000f;
    public float sidewayForce = 50f;
    public int id;
    public Player photonPlayer;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
        _rb = this.GetComponent<Rigidbody>();
    }

    [PunRPC]
    public void Initialize(Player player)
    {
        id = player.ActorNumber;
        photonPlayer = player;

        GameBehavior.instance.players[id - 1] = this;

        if (!photonView.IsMine)
        {
            GetComponentInChildren<Camera>().gameObject.SetActive(false);
            _rb.isKinematic = true;
        }
    }

    // Update for physics system
    void FixedUpdate()
    {
        if(movement)
        {
            _rb.AddForce(0, 0, forwardForce * Time.deltaTime);

            if (Input.GetKey("d"))
            {
                _rb.AddForce(sidewayForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
            else if (Input.GetKey("a"))
            {
                _rb.AddForce(-sidewayForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
        }

        if(_rb.position.y < -1f)
        {
            gameManager.EndGame();
        }
    }

    // Check for player collision
    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "Obstacle")
        {
            // Debug.Log("Hit");
            movement = false;
            gameManager.EndGame();
        }
    }
}
