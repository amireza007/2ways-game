using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArianWorkplace;

public class RoadManager : MonoBehaviour
{
    public enum Side
    {
        right,
        left
    }
    public enum Difficulty
    {
        low,
        medium,
        high
    }
    Queue<GameObject> trackingRightRoads = new Queue<GameObject>();
    Queue<GameObject> trackingLeftRoads = new Queue<GameObject>();
    public float elapsedTime = 0;
    GameObject dequeuedRightRoad;
    GameObject dequeuedLeftRoad;
    int fixedSeconds = 6; //for changing how quickly dequeued road need to disappear, according to player speed.
    float roadGap = 0; //length of a gap, depending on the speed of the player
    GameObject[] lastEnquedRoads = new GameObject[2]; //for placing the next enqueue with repsepct to last enqueued road
    int counter = 0;
    public GameObject player;
    public GameObject[] leftRoads = new GameObject[6];
    public GameObject[] rightRoads = new GameObject[6];
    //ObjectPool objectPool;
    //GameObject leftLane;
    PlayerMovement playerMovement;
    // Start is called before the first frame update

    void Awake()
    {
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

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (playerMovement.speed > 5) { fixedSeconds = 3; }
        if (counter == 0)
        {
            StartCoroutine(UpdateRightQueue());
            StartCoroutine(UpdateLeftQueue());
            counter++;
        }

    }
    IEnumerator UpdateRightQueue()
    {
        while (true)
        {
            while (player.transform.position.z <= dequeuedRightRoad.transform.position.z + 3.65f) 
            {
                yield return null;
            }
            yield return new WaitForSeconds(fixedSeconds/playerMovement.speed);
            dequeuedRightRoad.transform.position = lastEnquedRoads[0].transform.position + new Vector3(0, 0, 6.69f);
            lastEnquedRoads[0] = dequeuedRightRoad;
            trackingRightRoads.Enqueue(lastEnquedRoads[0]);
            dequeuedRightRoad = trackingRightRoads.Dequeue();
        }
    }
    IEnumerator UpdateLeftQueue()
    {
        while (true)
        {
            while (player.transform.position.z <= dequeuedLeftRoad.transform.position.z + 3.65f) 
            {
                yield return null;
            }
            yield return new WaitForSeconds(fixedSeconds / playerMovement.speed);
            dequeuedLeftRoad.transform.position = lastEnquedRoads[1].transform.position + new Vector3(0, 0, 6.69f);
            lastEnquedRoads[1] = dequeuedLeftRoad;
            trackingLeftRoads.Enqueue(lastEnquedRoads[1]);
            dequeuedLeftRoad = trackingLeftRoads.Dequeue();
        }
    }
    public void PlaceRoadsAhead(Side side,bool WantAGap)
    {
        //When you aim to place a gap, you should be careful about the last placed gap!
        //If it's not ok to place it (i.e. going falling into it when facing the last gap), DON'T DO IT! Or should you??
        //You could use the side road to scape... However you might not see your future because of the FOG, you have to choose to risk or not risk...
        if (side == Side.left) 
        {
            if (WantAGap)
            {
                
            }
            else //Thank God! pls, always do not want that! :)
            {

            }
        }
        else
        {

        }
    }
}
