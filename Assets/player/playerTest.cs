using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.VFX;

public class playerTest : MonoBehaviour
{

    [SerializeField]
    private int moveSpeed;
    public Rigidbody2D rd;
    public float y;

    private GamePlayerInput inputActions;

    private bool isGround = false;

    // Start is called before the first frame update
    void Start()
    {
        inputActions = new GamePlayerInput();
        inputActions.Enable();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Debug.Log("rd.gameObject.name");
            Vector2 force = new Vector2(0, y * 10);
            rd.velocity = force;
            isGround = false;
        }
    }

    private void FixedUpdate()
    {
        var direction = inputActions.Player.Move.ReadValue<Vector2>();
        direction.x *= moveSpeed;
        direction.y = rd.velocity.y;
        //moveSpeed
        rd.velocity = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
}
