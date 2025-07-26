using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotControl : MonoBehaviour
{
    public float volume;

    private Rigidbody2D rg;

    public float jumpForce = 500;
    public float maxSpeed = 5f;

    private float tempTime = 0;

    public Slider forwardLimitSlider;
    public Slider jumpLimitSlider;

    public static float ForwardLimit = 0.1f;
    public static float JumpLimit = 0.4f;

    public Animator animator;

    public bool isRush = false;
    public static float RushStopTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        forwardLimitSlider.value = ForwardLimit;
        jumpLimitSlider.value = JumpLimit;
    }

    // Update is called once per frame
    void Update()
    {
        volume = MicInput.volume;
        ForwardLimit = forwardLimitSlider.value;
        JumpLimit = jumpLimitSlider.value;

        if (RushStopTime > Time.time)
        {
            isRush = true;
            animator.SetBool("IsEatDrug", true);
        }
        else
        {
            isRush = false;
            animator.SetBool("IsEatDrug", false);
        }

        if (volume > ForwardLimit)
        {
            MoveForward();
            if (rg.velocity.x > maxSpeed)
            {
                if (isRush == false)
                {
                    rg.velocity = new Vector2(maxSpeed, rg.velocity.y);
                }
                else
                {
                    rg.velocity = new Vector2(maxSpeed * 2, rg.velocity.y);
                }
            }
        }

        if (rg.velocity.y <= 0.1f)
        {
            animator.SetBool("IsJump", false);
        }

        if (volume > jumpLimitSlider.value)
        {
            if (Time.time - tempTime > 2)
            {
                Jump();
                tempTime = Time.time;
            }
        }
    }

    void Jump()
    {
        animator.SetBool("IsJump", true);
        rg.AddForce(Vector2.up * jumpForce * volume);
    }

    void MoveForward()
    {
        rg.AddForce(Vector2.right * 10);
    }
}