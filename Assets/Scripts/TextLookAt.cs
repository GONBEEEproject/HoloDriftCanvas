using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLookAt : MonoBehaviour
{
    GameObject mycamera;

    // Start is called before the first frame update
    void Start()
    {
        mycamera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(mycamera.transform.position);
        transform.Rotate(new Vector3(0, -180, 0));
    }
}