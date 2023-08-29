using UnityEngine;

    public class FollowPlayer : MonoBehaviour
    {
        public bool shouldFollow = true;
        public Transform player;
        
        private Vector3 initialPos;
        
        void Start()
        {
            initialPos = transform.position - player.position;
            //initialPos = new Vector3(0,1.71f)
        }
        
        void Update()
        {
            if (shouldFollow)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, initialPos.z + player.position.z);
            }
        }
    }