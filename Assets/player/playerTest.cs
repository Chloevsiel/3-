using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTest : MonoBehaviour
{
    Rigidbody2D rd;
    public float y;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(rd.gameObject.name);
            Vector2 force = new Vector2(0, y * 10);
            rd.AddForce(force);
        }
        {
            if (Input.GetKey(KeyCode.D)) ;
        }
    }
}
