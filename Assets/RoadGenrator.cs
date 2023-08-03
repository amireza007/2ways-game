using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenrator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator generateRoad()
    {
        //when ball's contact point is $offset number AHEAD, deactivate the ball
        //we need to compute how many seconds, with respect to speed of the ball, it takes for the ball
        // to stop being in contact with the road. (which is complicated! & not Arian Pasand!:))
        while (true)
        {
            yield return new WaitForEndOfFrame();
        }
    }
}
