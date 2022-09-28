using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DinoController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float forwardMovementSpeed = 0.1f;
    private bool isGrounded;
    private Rigidbody2D playerRigidbody;
    public Transform groundCheckTransform;
    public LayerMask groundCheckLayerMask;
    private Animator dinoAnimator;
    private bool dead = false;
    private uint coins = 0;
    public Text coinsCollectedLabel;
    public Button restartButton, quitAfterDiedButton;
    public AudioSource jumpSound;
    public AudioSource collectCoinSound;
    public AudioSource dieSound;

    void Start(){
        playerRigidbody = GetComponent<Rigidbody2D>();
        dinoAnimator = GetComponent<Animator>();

    }

    public void RestartGame(){
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
    
    void FixedUpdate(){
        UpdateGroundedStatus();
        bool jumpActive = Input.GetKey("space");
        jumpActive = jumpActive && !dead;

        if (jumpActive){
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
        }

        if (!dead){
            Vector2 newVelocity = playerRigidbody.velocity;
            if (forwardMovementSpeed < 8.0){
                forwardMovementSpeed = forwardMovementSpeed + (float) 0.007;
                jumpForce = jumpForce + (float) 0.0005;
            } else{
                forwardMovementSpeed = forwardMovementSpeed + (float) 0.003;
            }

            newVelocity.x = forwardMovementSpeed;
            playerRigidbody.velocity = newVelocity;
        } else{
            Vector2 newVelocity = playerRigidbody.velocity;
            newVelocity.x = 0;
            playerRigidbody.velocity = newVelocity;
            restartButton.gameObject.SetActive(true);
            quitAfterDiedButton.gameObject.SetActive(true);
        }

        UpdateGroundedStatus();
    }

    void CollectCoin(Collider2D coinCollider){
        coins = coins + 1;
        coinsCollectedLabel.text = coins.ToString();
        Destroy(coinCollider.gameObject);
        collectCoinSound.Play();
    }

    void CollectEgg(Collider2D eggCollider){
        coins = coins + 5;
        coinsCollectedLabel.text = coins.ToString();
        eggCollider.gameObject.SetActive(false);
        collectCoinSound.Play();
    }

    void UpdateGroundedStatus(){
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);
        dinoAnimator.SetBool("isGrounded", isGrounded);
    }

    void Update(){
        Vector2 newVelocity = playerRigidbody.velocity;
        newVelocity.x = forwardMovementSpeed;
        playerRigidbody.velocity = newVelocity;
    
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.CompareTag("coins")){
            CollectCoin(collider);
        } else if (collider.gameObject.CompareTag("egg")){
            CollectEgg(collider);
        } else {
            dieSound.Play();
            HitByLog(collider);
        }
    }

    void HitByLog(Collider2D logCollider){
        dead = true;
        dinoAnimator.SetBool("dead", true);
    }
}
