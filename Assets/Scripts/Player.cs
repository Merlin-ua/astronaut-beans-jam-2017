using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    public Animator playerAnimator;
    public SpriteRenderer playerSpriteRenderer;
    public VoiceControl voiceControl;
    public float maxVelocity = 10.0f;

    public float maxFuel = 100.0f;
    public float fuelConsumption = 1.0f;
    public float fuelRegen = 10.0f;

    public float maxY = 1.0f;

    public int lives = 3;
    float fuelCapacity;

    float previousValue = 0.0f;
    float jumpTimer = 0.0f;

    private Rigidbody2D rigidbody2D;

    private bool isGrounded = false;
    private int playerLayerMask = 0;

    private float invincibleTimer = 0f;
    private float invincibleTimerMax = 3.0f;

    private float score = 0f;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerLayerMask |= (1 << LayerMask.NameToLayer("Player"));
        playerLayerMask = ~playerLayerMask;
        fuelCapacity = maxFuel;
    }

    void Start () {

    }

    private void LateUpdate() {
        rigidbody2D.velocity = new Vector2(0.0f, Mathf.Clamp(rigidbody2D.velocity.y, -maxVelocity * 10, maxVelocity));
    }

    void Update() {
        bool isGrounded = IsGrounded();
        playerAnimator.SetBool("Grounded", isGrounded);
        // controls 
        if (GetLives() == 0 )
        {
            if(Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
            if (voiceControl.GetJumpValue() > 0.0f)
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(1);
            }
                
        }

        // regen fuel
        if (isGrounded) {
            fuelCapacity += Time.deltaTime * fuelRegen;
        } 
        fuelCapacity = Mathf.Clamp(fuelCapacity, 0f, maxFuel);

        // jetpack and jump logic
        float newValue = Mathf.Clamp(Mathf.Log(voiceControl.GetJumpValue()) * 10000.0f, 0.0f, 10000.0f);

        if (newValue > 0.0f) {
            playerAnimator.SetBool("Fly", true);
            fuelCapacity -= newValue * fuelConsumption * Time.deltaTime;

            if(isGrounded && jumpTimer == 0.0f) {
                newValue *= 5f;
                jumpTimer = 1.0f;
            }
        } else {
            playerAnimator.SetBool("Fly", false);
        }
        
        if (fuelCapacity <= 0.0f) {
            newValue = 0.0f;
        }

        rigidbody2D.AddForce(new Vector2(0.0f, newValue), ForceMode2D.Force);

        jumpTimer = Mathf.MoveTowards(jumpTimer, 0.0f, 1.0f);
        if(invincibleTimer > 0.0f) {
            invincibleTimer -= Time.deltaTime;
            playerSpriteRenderer.enabled = Mathf.FloorToInt(invincibleTimer * 10f) % 2 == 0;
        } else {
            playerSpriteRenderer.enabled = true;
        }

        score += Time.deltaTime * 10.0f;
    }

    public bool IsGrounded() {
        return Physics2D.Raycast(transform.position - new Vector3(0.0f, 0.0f, 0.0f), -Vector2.up, 0.6f, playerLayerMask);
    }

    public float GetFuelRatio() {
        return fuelCapacity / maxFuel;
    }

    public int GetLives() {
        return lives;
    }

    public void OnCollisionEnter2D(Collision2D col) {
        if((col.transform.tag == "Projectile" || col.transform.tag == "Enemy") && invincibleTimer <= 0f)
        {
            if(col.transform.tag == "Projectile")
                Destroy(col.gameObject);
            invincibleTimer = invincibleTimerMax;
            Hit();
        }
    }

    public void Hit() {
        lives -= 1;
    }

    public void Kill() {
        lives = 0;
    }

    public Vector2 GetVelocity() {
        return rigidbody2D.velocity;
    }

    public int GetScore() {
        return Mathf.FloorToInt(score);
    }
}
