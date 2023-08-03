using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArianWorkplace;

public class RoadManager : MonoBehaviour
{
    Queue<GameObject> untrackedRightRoads = new Queue<GameObject>();
    Queue<GameObject> trackingRightRoads = new Queue<GameObject>();
    Queue<GameObject> untrackedLeftRoads = new Queue<GameObject>();
    Queue<GameObject> trackingLeftRoads = new Queue<GameObject>();
    GameObject contactingRoad;
    GameObject[] lastEnquedRoads = new GameObject[2];
    int counter = 0;
    public GameObject player;
    public GameObject[] leftRoads = new GameObject[10];
    public GameObject[] rightRoads = new GameObject[10];
    ObjectPool objectPool;
    GameObject leftLane;
    PlayerMovement playerMovement;
    double testTime = 0;
    // Start is called before the first frame update

    void Awake()
    {
        contactingRoad = GameObject.FindGameObjectWithTag("RoadC");
        contactingRoad = rightRoads[0];
        //untrackedRoads.
        rightRoads[0] = Instantiate(GameObject.FindGameObjectWithTag("RoadC"));
        leftRoads[0] = Instantiate(GameObject.FindGameObjectWithTag("RoadC"));
        rightRoads[0].transform.position = new Vector3(0.6f, 0.03f, 4.4f);
        leftRoads[0].transform.position = new Vector3(-0.6f, 0.03f, 4.4f);
        trackingLeftRoads.Enqueue(leftRoads[0]);
        trackingRightRoads.Enqueue(rightRoads[0]);

        for (int i = 1; i < 10; i++)
        {
            rightRoads[i] = Instantiate(GameObject.FindGameObjectWithTag("RoadC"));
            leftRoads[i] = Instantiate(GameObject.FindGameObjectWithTag("RoadC"));
            rightRoads[i].transform.position = rightRoads[i - 1].transform.position + new Vector3(0, 0, 6.7f);
            leftRoads[i].transform.position = leftRoads[i - 1].transform.position + new Vector3(0, 0, 6.7f);
            trackingLeftRoads.Enqueue(leftRoads[i]);
            trackingRightRoads.Enqueue(rightRoads[i]);
            if(i == 9) {
                lastEnquedRoads[0] = rightRoads[i];
                lastEnquedRoads[1] = leftRoads[i];
            }
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 0)
        {
            StartCoroutine(UpdateQueues());
            counter++;
        }
        //timer += Time.deltaTime;
        //if(timer)
        //testTime += Time.deltaTime;
        //Debug.Log(Time.deltaTime);
    }
    IEnumerator UpdateQueues()
    {
        while (true)
        {
            //Debug.Log("hi");
            contactingRoad = (player.transform.position.x == -0.6f) ? trackingLeftRoads.Dequeue() : trackingRightRoads.Dequeue();
            //Debug.Log(contactingRoad.transform.position.z);

            while (player.transform.position.z <= contactingRoad.transform.position.z + 3.65f)
            {
                Debug.Log("you are in this Lane:" + contactingRoad.transform.position.z);
                yield return null;
            }
            Debug.Log("hellooooo000000000000000!");
            yield return new WaitForSeconds(3/playerMovement.speed);
            if (contactingRoad.transform.position.x == 0.6f) {
                contactingRoad.transform.position = lastEnquedRoads[0].transform.position + new Vector3(0, 0, 6.7f);
                lastEnquedRoads[0] = contactingRoad;
                trackingRightRoads.Enqueue(lastEnquedRoads[0]);
            }
            else {
                contactingRoad.transform.position = lastEnquedRoads[1].transform.position + new Vector3(0, 0, 6.7f);
                lastEnquedRoads[1] = contactingRoad;
                trackingLeftRoads.Enqueue(lastEnquedRoads[1]);
            }
        }
    }
    ////////////////////////
    ///forget the code below
    ///simply have a list of 10 lanes and transfer them ahead once the ball leaves them
    IEnumerator ManageLeftRoads()       
    {
        //This coroutine both pool(deactivate) and unpool (activate the following) road
        //Bear in mind that deactivation depends on the speed of the ball
        
        /////////////////////////////
        //when ball's contact point is $offset number AHEAD, deactivate the ball
        //we need to compute how many seconds, with respect to speed of the ball, it takes for the ball
        // to stop being in contact with the road. (which is complicated! & not Arian Pasand!:))
        while (true)
        {
            
            while(objectPool.pooledObjects.Count > 8) {
                playerMovement.speed = 3;
            }
          
            yield return null;
        }
    }
}
