using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Vector2 vel;
    public float smoothTime;

    public bool isPickedUp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPickedUp)
        {
            transform.position = Vector2.SmoothDamp(transform.position, player.transform.position, ref vel, smoothTime);
        }
    }

    private void OTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag ("Player")&& !isPickedUp)
        {
            isPickedUp = true;
        }
    }
}
