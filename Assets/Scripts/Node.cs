using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public TextMesh myNum;

    public void SetUp(int num)
    {
        myNum.text = num.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            MarkerManager.Instance.MarkerTouched(this);
        }
    }
}
