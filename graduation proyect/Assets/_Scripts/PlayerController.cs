using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private const string MOVE_HORIZONTAL = "Move_Horizontal";
    private const string MOVE_VERTICAL = "Move_Vertical";

    public float speed = 6;
    public float forceJump = 5;
    public float gravityMultiplier;
    public float moveForward;

    private Rigidbody playerRb;
    private float moveHorizontal;
    private float moveVertical;
    private bool isOnGround = true;
    private Animator _animator;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        Physics.gravity *= gravityMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        moveForward = (moveHorizontal * moveHorizontal + moveVertical * moveVertical);
        if(moveForward > 1)
        {
            moveForward /= 2;
        }

        transform.Translate(Vector3.forward * speed * moveForward * Time.deltaTime);
        
         if(moveForward > 0.01)
        {
                _animator.SetFloat(MOVE_HORIZONTAL, moveHorizontal * 5);
                _animator.SetFloat(MOVE_VERTICAL, moveVertical * 5);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * forceJump, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
            isOnGround = true;
        }
    }
}
