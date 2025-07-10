using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool locked;

    public GameObject gameWinUI;
    // Start is called before the first frame update
    void Start()
    {
        locked = true;

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            locked = false;

            gameWinUI.SetActive(true);
        }
    }
}
