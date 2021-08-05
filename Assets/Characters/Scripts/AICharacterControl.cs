using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }// the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        private Transform target;                                    // target to aim for
        public Transform target1, target2;
        private GameObject player;
        private bool playerCaught;
        private float playerCaughtDuration = 2.5f;
        private float playerCaughtTimer = 0.0f;


        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updateRotation = false;
            agent.updatePosition = true;

            if (target1 != null)
            {
                target = target1;
                agent.SetDestination(target.position);
                Debug.Log("target set");
            }
            else Debug.Log("No target");
        }


        private void Update()
        {
            if(playerCaught == false)
            {
                if (gameObject.transform.position.z == target.position.z && gameObject.transform.position.x == target.position.x)
                {
                    if (target == target1)
                        target = target2;

                    else if (target == target2)
                        target = target1;

                    agent.SetDestination(target.position);
                }
                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    character.Move(agent.desiredVelocity * 0.5f, false, false);
                }
                else
                {
                    character.Move(Vector3.zero, false, false);
                }
            }
            else
            {
                player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().Move(Vector3.zero, false, false);
                gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().Move(Vector3.zero, false, false);
                playerCaughtTimer += Time.deltaTime;

                if(playerCaughtTimer >= playerCaughtDuration)
                {
                    playerCaughtTimer = 0.0f;
                    playerCaught = false;
                    player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().enabled = true;
                    player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = true;
                    player = null;

                    target = target1;
                    agent.SetDestination(target.position);
                    EventBroadcaster.Instance.PostEvent(EventNames.ON_PLAYER_CAUGHT);
                    this.GetComponent<Collider>().enabled = true;
                }
            }
        }


        public void SetTarget(Transform target1, Transform target2)
        {
            this.target1 = target1;
            this.target2 = target2;
        }

        private void OnCollisionEnter(Collision collision)
        {
            GameObject collideObj;
            collideObj = collision.gameObject;
            while(collideObj.transform.parent != null)
            {
                collideObj = collideObj.transform.parent.gameObject;
            }

            if(collideObj.name == "Player")
            {
                gameObject.GetComponent<Collider>().enabled = false;
                player = collideObj;

                player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = false;
                player.transform.LookAt(new Vector3(gameObject.transform.position.x, player.transform.position.y, gameObject.transform.position.z));
                //player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = false;
                playerCaught = true;
                agent.ResetPath();
                gameObject.transform.LookAt(new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z));

                EventBroadcaster.Instance.PostEvent(EventNames.ON_PLAYER_WALK_STOP_SFX);
                EventBroadcaster.Instance.PostEvent(EventNames.ON_STOP_ALL_SFX);
            }
        }



    }
}
