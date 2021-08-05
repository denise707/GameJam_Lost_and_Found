using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        [SerializeField] float speed = 6.0f;
        [SerializeField] float turnSmoothTime = 0.5f;
        [SerializeField] Transform camera;

        float turnSmoothVelocity;

        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.



        private void Start()
        {
            EventBroadcaster.Instance.AddObserver(EventNames.ON_PLAYER_CAUGHT, this.terminate);
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }

        private void OnDestroy()
        {
            EventBroadcaster.Instance.RemoveObserver(EventNames.ON_PLAYER_CAUGHT);
        }
        private void Update()
        {
            
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                //  controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }

            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v * m_CamForward + h * m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v * Vector3.forward + h * Vector3.right;
            }
#if !MOBILE_INPUT
            // walk speed multiplier
            if ((h != 0.0f || v != 0.0f) && m_Jump ==false)
            {
                Parameters parameters = new Parameters();


                if (Input.GetKey(KeyCode.LeftShift))
                {
                    m_Move *= 0.5f;
                    parameters.PutExtra("isWalking", true);
                }

                else
                    parameters.PutExtra("isWalking", false);

                EventBroadcaster.Instance.PostEvent(EventNames.ON_PLAYER_WALK_SFX, parameters);
            }

            else
            {
                EventBroadcaster.Instance.PostEvent(EventNames.ON_PLAYER_WALK_STOP_SFX);
            }
            
            
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }

        private void terminate()
        {
            Time.timeScale = 0;
            //this.enabled = false;
            //SceneManager.LoadScene("Game Proper");
        }
    }


}
