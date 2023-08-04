using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArianWorkplace;

public class RoadManager : MonoBehaviour
{
    Queue<GameObject> trackingRightRoads = new Queue<GameObject>();
    Queue<GameObject> trackingLeftRoads = new Queue<GameObject>();
    GameObject notcontactingRoad;
    GameObject contactingRoad;
    GameObject dequeuedRightRoad;
    GameObject dequeuedLeftRoad;
    int fixedSeconds = 6;
    float roadGap = 0;
    GameObject[] lastEnquedRoads = new GameObject[2]; //for placing the next enqueue with repsepct to last enqueued road
    int counter = 0;
    public GameObject player;
    public GameObject[] leftRoads = new GameObject[6];
    public GameObject[] rightRoads = new GameObject[6];
    ObjectPool objectPool;
    GameObject leftLane;
    PlayerMovement playerMovement;
    double testTime = 0;
    // Start is called before the first frame update

    void Awake()
    {
        contactingRoad = GameObject.FindGameObjectWithTag("RoadC");
        contactingRoad = rightRoads[0];
        //untrackedRoads
        rightRoads[0] = Instantiate(GameObject.FindGameObjectWithTag("RoadC"));
        leftRoads[0] = Instantiate(GameObject.FindGameObjectWithTag("RoadC"));
        rightRoads[0].transform.position = new Vector3(0.6f, 0.03f, 4.4f);
        leftRoads[0].transform.position = new Vector3(-0.6f, 0.03f, 4.4f);
        trackingLeftRoads.Enqueue(leftRoads[0]);
        trackingRightRoads.Enqueue(rightRoads[0]);

        for (int i = 1; i < 6; i++)
        {
            rightRoads[i] = Instantiate(GameObject.FindGameObjectWithTag("RoadC"));
            leftRoads[i] = Instantiate(GameObject.FindGameObjectWithTag("RoadC"));
            rightRoads[i].transform.position = rightRoads[i - 1].transform.position + new Vector3(0, 0, 6.69f);
            leftRoads[i].transform.position = leftRoads[i - 1].transform.position + new Vector3(0, 0, 6.69f);
            trackingLeftRoads.Enqueue(leftRoads[i]);
            trackingRightRoads.Enqueue(rightRoads[i]);
            if(i == 5) {
                lastEnquedRoads[0] = rightRoads[i];
                lastEnquedRoads[1] = leftRoads[i];
            }
        }
        dequeuedRightRoad = trackingRightRoads.Dequeue();
        dequeuedLeftRoad = trackingLeftRoads.Dequeue();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.speed > 5) { fixedSeconds = 3; }
        if (counter == 0)
        {
            StartCoroutine(UpdateRightQueue());
            StartCoroutine(UpdateLeftQueue());
            counter++;
        }
        //timer += Time.deltaTime;
        //if(timer)
        //testTime += Time.deltaTime;
        //Debug.Log(Time.deltaTime);
    }
    IEnumerator UpdateRightQueue()
    {
        while (true)
        {
            //Debug.Log("hi");
            
            //notcontactingRoad = trackingRightRoads.Dequeue();
            //else {
            //    contactingRoad = trackingRightRoads.Dequeue();
            //    notcontactingRoad = trackingLeftRoads.Dequeue();
            //}
            
            //Debug.Log(contactingRoad.transform.position.z);

            while (player.transform.position.z <= dequeuedRightRoad.transform.position.z + 3.65f) //player.transform.position.x >= contactingRoad.transform.position.x - 0.2f
            {

                //Debug.Log("The field of view contains this road: " + dequeuedRightRoad.transform.position.z);
                yield return null;
            }
            yield return new WaitForSeconds(fixedSeconds/playerMovement.speed);
            //Debug.Log("We've exited the right road: Enqueue process of this road and the next dequeue begins!");
            dequeuedRightRoad.transform.position = lastEnquedRoads[0].transform.position + new Vector3(0, 0, 6.69f);
            lastEnquedRoads[0] = dequeuedRightRoad;
            trackingRightRoads.Enqueue(lastEnquedRoads[0]);
            dequeuedRightRoad = trackingRightRoads.Dequeue();

            //else {
            //    contactingRoad.transform.position = lastEnquedRoads[1].transform.position + new Vector3(0, 0, 69f);
            //    notcontactingRoad.transform.position = lastEnquedRoads[0].transform.position + new Vector3(0, 0, 6.69f);
            //    lastEnquedRoads[1] = contactingRoad;
            //    trackingLeftRoads.Enqueue(lastEnquedRoads[1]);
            //    lastEnquedRoads[0] = notcontactingRoad;
            //    trackingRightRoads.Enqueue(lastEnquedRoads[0]);
            //}
        }
    }
    IEnumerator UpdateLeftQueue()
    {
        while (true)
        {
            //Debug.Log("hi");

            //notcontactingRoad = trackingRightRoads.Dequeue();
            //else {
            //    contactingRoad = trackingRightRoads.Dequeue();
            //    notcontactingRoad = trackingLeftRoads.Dequeue();
            //}

            //Debug.Log(contactingRoad.transform.position.z);

            while (player.transform.position.z <= dequeuedLeftRoad.transform.position.z + 3.65f) //player.transform.position.x >= contactingRoad.transform.position.x - 0.2f
            {

                Debug.Log("The field of view contains this road: " + dequeuedLeftRoad.transform.position.z);
                yield return null;
            }
            yield return new WaitForSeconds(fixedSeconds / playerMovement.speed);
            Debug.Log("We've exited the left road: Enqueue process of this road and the next dequeue begins!");
            dequeuedLeftRoad.transform.position = lastEnquedRoads[1].transform.position + new Vector3(0, 0, 6.69f);
            lastEnquedRoads[1] = dequeuedLeftRoad;
            trackingLeftRoads.Enqueue(lastEnquedRoads[1]);
            dequeuedLeftRoad = trackingLeftRoads.Dequeue();

            //else {
            //    contactingRoad.transform.position = lastEnquedRoads[1].transform.position + new Vector3(0, 0, 69f);
            //    notcontactingRoad.transform.position = lastEnquedRoads[0].transform.position + new Vector3(0, 0, 6.69f);
            //    lastEnquedRoads[1] = contactingRoad;
            //    trackingLeftRoads.Enqueue(lastEnquedRoads[1]);
            //    lastEnquedRoads[0] = notcontactingRoad;
            //    trackingRightRoads.Enqueue(lastEnquedRoads[0]);
            //}
        }
    }
    ////////////////////////
    ///forget the code below
    ///simply have a list of 10 lanes and transfer them ahead once the ball leaves them
}
