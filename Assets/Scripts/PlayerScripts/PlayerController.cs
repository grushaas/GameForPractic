using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stat Player")]
    public float speed = 5f;
    public float health = 100f;
    public int level = 1;

    public int experience = 0;
    public int experienceToLevelUp = 100;

    private PlayerInput playerInput;
    private Rigidbody2D rb;
    private Animator anim;

    private UIStats uiStats;

    void Awake()
    {
        playerInput = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        uiStats = FindFirstObjectByType<UIStats>();

        uiStats.UpdateHealthLabel(health);
        uiStats.UpdateLevelLabel(level);
    }

    void FixedUpdate()
    {
        Vector2 inputVector = playerInput.Player.Move.ReadValue<Vector2>();

        Vector2 direction = new Vector2(inputVector.x, inputVector.y).normalized;
        RotateTowardsMouse();

        rb.linearVelocity = direction * speed;

        anim.SetBool("isWalking", direction.magnitude > 0);
    }

    private void RotateTowardsMouse()
    { 
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }

    public Vector2 getPosition()
    {
        return transform.position;
    } 
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Health: " + health);

        if(uiStats != null)
        {
            uiStats.UpdateHealthLabel(health);
        }

        if (health <= 0)
        {
            Debug.Log("==DEAD==");
            //GameOver();
        }
    }

    private void OpenSkillInterface()
    {
        SkillUI skillUI = FindFirstObjectByType<SkillUI>();
        if(skillUI != null)
        {
            skillUI.OpenSkillInterface();
        }
    }

    public void levelUp()
    {
        level++;
        if(uiStats != null)
        {
            uiStats.UpdateLevelLabel(level);
        }

        PauseGame();
        OpenSkillInterface();
    }

    public void GainExperience(int amount)
    {
        experience += amount;

        if(experience >= experienceToLevelUp)
        {
            experience = 0;
            levelUp();
        }
    }

    private void GameOver()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // Завершить приложение в сборке
#endif
    }
}
