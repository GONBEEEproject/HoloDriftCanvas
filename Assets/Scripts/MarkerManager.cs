using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour
{
    private static MarkerManager instance;
    public static MarkerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MarkerManager>();
            }
            return instance;
        }
    }


    private List<Node> nodes = new List<Node>();
    private int flagNum;

    private TextMesh timeMesh;
    private TextMesh[] rankingMesh;

    private float scoreTime;
    private float distance = 0;

    private List<float> rankingTime = new List<float>();

    private bool isAttack = false;
    private bool cleared = false;

    public AudioClip StartSound, EndSound,NodeSound,NodePut;
    private AudioSource source;

    public Material touchedFlag, waitFlag;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isAttack)
        {
            scoreTime += Time.deltaTime;

            string s = scoreTime.ToString() + "sec\n" + distance.ToString("0.00") + "m";
            timeMesh.text = s;
        }
    }

    public void MarkerTouched(Node node)
    {
        if (GazeGestureManager.Instance.state == GazeGestureManager.NowState.TimeAttack && isAttack)
        {
            if (nodes.Count == flagNum) return;

            if (nodes[flagNum] == node)
            {
                flagNum++;

                node.gameObject.GetComponent<MeshRenderer>().material = touchedFlag;

                source.PlayOneShot(NodeSound);
            }
        }
    }

    public void FlagTouched()
    {
        if (cleared) return;

        if (GazeGestureManager.Instance.state == GazeGestureManager.NowState.TimeAttack)
        {

            if (nodes.Count == flagNum)
            {
                isAttack = false;
                cleared = true;
                source.PlayOneShot(EndSound);
                RankingUpdate();
            }
            else if (isAttack == false)
            {
                isAttack = true;
                source.PlayOneShot(StartSound);
            }
        }
    }

    private void RankingUpdate()
    {
        rankingTime.Add(scoreTime);
        rankingTime.RemoveAll(num => num == 0);
        rankingTime.Sort();

        for(int i = rankingTime.Count; i < rankingMesh.Length; i++)
        {
            rankingTime.Add(0);
        }

        rankingTime.RemoveRange(rankingMesh.Length, rankingTime.Count - rankingMesh.Length);

        for(int i = 0; i < rankingMesh.Length; i++)
        {
            rankingMesh[i].text = rankingTime[i].ToString("0.000000");
        }
    }

    public void StartMarker(GameObject startFlag)
    {
        nodes = new List<Node>();
        rankingTime = new List<float>();
        scoreTime = 0;
        distance = 0;
        flagNum = 0;

        isAttack = false;

        timeMesh = startFlag.GetComponent<StartFlag>().timeMesh;
        rankingMesh = startFlag.GetComponent<StartFlag>().rankingMesh;
        source.PlayOneShot(NodePut);
    }

    public void DestroyMarker()
    {
        isAttack = false;
    }


    public void ResetMarker()
    {
        scoreTime = 0;
        flagNum = 0;

        isAttack = false;
        cleared = false;

        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].gameObject.GetComponent<MeshRenderer>().material = waitFlag;
        }

        string s = scoreTime.ToString("0.000000") + "sec\n" + distance.ToString("0.00") + "m";
        timeMesh.text = s;

        source.PlayOneShot(EndSound);
    }

    public void SetNewMarker(Node newNode)
    {
        newNode.SetUp(nodes.Count + 1);
        nodes.Add(newNode);
        source.PlayOneShot(NodePut);
    }

    public void EndMarker(float dist)
    {
        distance = dist;
        source.PlayOneShot(EndSound);

        string s = "Start/End\n" + distance.ToString("0.00") + "m";
        timeMesh.text = s;
    }
}
