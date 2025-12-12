using System.Collections.Generic;
using NUnit.Framework;
using PGGE;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public CharacterController mCharacterController;
    public Animator mAnimator;

    public float mWalkSpeed = 1.5f;
    public float mRotationSpeed = 50.0f;
    public bool mFollowCameraForward = false;
    public float mTurnRate = 10.0f;

#if UNITY_ANDROID
    public FixedJoystick mJoystick;
#endif

    private float hInput;
    private float vInput;
    private float speed;
    private bool jump = false;
    private bool crouch = false;
    private bool run = false; // NEW ANIMATIOM

    public List<string> mAnimations = new List<string>(); // TO GET ANIMATIONS NAME

    public float mGravity = -30.0f;
    public float mJumpHeight = 1.0f;
    public float mRunningSpeed = 2.0f; // NEW ANIMATION

    private Vector3 mVelocity = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {
        mCharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //HandleInputs();
        //Move();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
    }

    public void HandleInputs()
    {
        // We shall handle our inputs here.
#if UNITY_STANDALONE
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
#endif

#if UNITY_ANDROID
        hInput = 2.0f * mJoystick.Horizontal;
        vInput = 2.0f * mJoystick.Vertical;
#endif

        speed = mWalkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = mWalkSpeed * mRunningSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jump = false;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            crouch = !crouch;
            Crouch();
        }

        // NEW ANIMATION - Running
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            run = true;
            Run();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            run = false;
            Run();
        }

        // NEW ANIMATION - ComboPunch
        if (Input.GetKeyDown(KeyCode.C))
        {
            ComboPunch();
        }


        // NEW ANIMATION - Martelo
        if (Input.GetKeyDown(KeyCode.V))
        {
            Martelo();
        }

        // NEW ANIMATION - Macaco
        if (Input.GetKeyDown(KeyCode.B))
        {
            Macaco();
        }
    }

    public void Move()
    {
        if (mAnimator == crouch) return;

        // See if animator is running any animation
        if (mAnimator != null)
        {
            // Loop through all animations and see if it matches with the animator state
            for (int i = 0; i < mAnimations.Count; i++)
            {
                if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName(mAnimations[i]))
                {
                    return;
                }
            }
        }

        // We shall apply movement to the game object here.
        if (mAnimator == null) return;
        if (mFollowCameraForward)
        {
            // rotate Player towards the camera forward.
            Vector3 eu = Camera.main.transform.rotation.eulerAngles;
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.Euler(0.0f, eu.y, 0.0f),
                mTurnRate * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0.0f, hInput * mRotationSpeed * Time.deltaTime, 0.0f);
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward).normalized;
        forward.y = 0.0f;

        mCharacterController.Move(forward * vInput * speed * Time.deltaTime);
        mAnimator.SetFloat("PosX", 0);
        mAnimator.SetFloat("PosZ", vInput * speed / (2.0f * mWalkSpeed));

        if (jump)
        {
            Jump();
            jump = false;
        }
    }

    void Jump()
    {
        mAnimator.SetTrigger("Jump");
        mVelocity.y += Mathf.Sqrt(mJumpHeight * -2f * mGravity);
    }

    private Vector3 HalfHeight;
    private Vector3 tempHeight;
    void Crouch()
    {
        mAnimator.SetBool("Crouch", crouch);
        if (crouch)
        {
            tempHeight = CameraConstants.CameraPositionOffset;
            HalfHeight = tempHeight;
            HalfHeight.y *= 0.5f;
            CameraConstants.CameraPositionOffset = HalfHeight;
        }
        else
        {
            CameraConstants.CameraPositionOffset = tempHeight;
        }
    }

    // NEW ANIMATION - Run
    void Run()
    {
        mAnimator.SetBool("Run", run);
    }

    // NEW ANIMATION - ComboPunch
    void ComboPunch()
    {
        mAnimator.SetTrigger("ComboPunch");
    }

    //// NEW ANIMATION - Martelo
    void Martelo()
    {
        mAnimator.SetTrigger("Martelo");
    }

    // NEW ANIMATION - Macaco
    void Macaco()
    {
        mAnimator.SetTrigger("Macaco");
    }

    void ApplyGravity()
    {
        // apply gravity.
        mVelocity.y += mGravity * Time.deltaTime;
        if (mCharacterController.isGrounded && mVelocity.y < 0)
            mVelocity.y = 0f;
    }
}
