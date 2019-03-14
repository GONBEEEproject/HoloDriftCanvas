using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFlag : MonoBehaviour {

    public TextMesh timeMesh;

    public TextMesh[] rankingMesh;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            MarkerManager.Instance.FlagTouched();
        }
    }

}
