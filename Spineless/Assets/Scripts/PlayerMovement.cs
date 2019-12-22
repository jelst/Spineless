using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{
    public bool playingSteps = false;
    public AudioClip walkSound;
    public AudioClip swordSound;
    public float volume;
    AudioSource audioSource;
    public PlayerState currentState;
    public float playerMoveSpeed;
    private Rigidbody2D playerRB;
    private Vector2 moveSpeedChange;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeedChange.x = Input.GetAxisRaw("Horizontal");
        moveSpeedChange.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack)
        {
            currentState = PlayerState.attack;
            StartCoroutine(AttackCo());
        }
        else if (moveSpeedChange != Vector2.zero && currentState != PlayerState.attack)
        {
            currentState = PlayerState.walk;
            MovePlayer();
            if(playingSteps == false)
            {
                playingSteps = true;
                StartCoroutine(StepsCo());
            }
            animator.SetFloat("moveX", moveSpeedChange.x);
            animator.SetFloat("moveY", moveSpeedChange.y);
            animator.SetBool("isMoving", true);
        }
        else if (currentState != PlayerState.attack)
        {
            currentState = PlayerState.idle;
            animator.SetBool("isMoving", false);
        }
    }
    
    private IEnumerator StepsCo()
    {
        audioSource.PlayOneShot(walkSound, volume);
        yield return new WaitForSeconds(0.5f);
        playingSteps = false;
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("isAttacking", true);
        audioSource.PlayOneShot(swordSound, volume);
        yield return null;
        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(0.45f);
        currentState = PlayerState.idle;
    }

    void MovePlayer()
    {
        playerRB.MovePosition(playerRB.position + moveSpeedChange.normalized * playerMoveSpeed * Time.deltaTime);
    }
}
