using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpeedChecker : MonoBehaviour {

    private Vector3 latest;
    private float speed;
    private GameObject cam;

    private float[] speedList = new float[10];
    private int listNum = 0;

    [SerializeField]
    private TextMesh speedText;

    


	// Use this for initialization
	void Start () {
        cam = Camera.main.gameObject;
        latest = cam.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        speed = ((cam.transform.position - latest) / Time.deltaTime).magnitude;

        latest = cam.transform.position;

        speedList[listNum] = speed;

        listNum++;
        listNum = listNum % speedList.Length;

        speedText.text = ((speedList.Average()) * 3.6f).ToString("0.00") + "km/h";
	}
}
