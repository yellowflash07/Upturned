using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEaten : MonoBehaviour
{
    public bool fishEaten;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fish")
        {
            other.gameObject.SetActive(false);
            fishEaten = true;
        }
    }
}
