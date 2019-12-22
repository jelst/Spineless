using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public GameObject initialCam;
    public GameObject newCam;
    public Rigidbody2D playerRB;
    public Vector3 positionAdjustment;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position += positionAdjustment;
            newCam.SetActive(true);
            initialCam.SetActive(false);
        }
    }
}
