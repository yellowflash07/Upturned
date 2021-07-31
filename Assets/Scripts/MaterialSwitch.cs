using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwitch : MonoBehaviour
{
    bool flip = true;

    public Material upMat;
    public Material downMat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            flip = !flip;
        }

        if (flip)
        {
            GetComponent<MeshRenderer>().material = upMat;
        }
        if (!flip)
        {
            GetComponent<MeshRenderer>().material = downMat;
        }
    }
}
