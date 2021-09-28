using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BotController : MonoBehaviour
{
    private BotSensor sensor;
    private Rigidbody2D rb;
    public UnityEvent DamageTakenEvent;
    [SerializeField] private float HP = 1;
    //class used to locate and change slots and the botparts which are on each slot
    public Slots slots;

    //Get this bot's current HP
    public float GetGetHP()
    { 
        return HP; 
    }

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        sensor = GetComponent<BotSensor>();
        rb = GetComponent<Rigidbody2D>();
        if (DamageTakenEvent == null)
            DamageTakenEvent = new UnityEvent();



    }

    public void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Combat" || currentScene == "General Testing Scene")
        {
            FaceEnemy();
        }
    }

    private void FaceEnemy()
    {
        foreach (Transform childtransform in transform)
        {
            childtransform.localScale = new Vector3(sensor.GetNearestSensedBotDirection(), 1, 1);
        }
    }

    public void SetPosition(Vector3 newPosition)
    {
        //The desired new position is sent by the attacking bot, but may be countered by certain effects
        rb.position = newPosition;
    }

    public void ApplyForce(Vector3 force)
    {
        //The desired force is sent by the attacking bot, but may be countered by certain effects
        rb.AddRelativeForce(force, ForceMode2D.Impulse);
    }

    public void PlayAudio(string audioName)
    {
        AudioManager.instance.Play(audioName);
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        DamageTakenEvent.Invoke();
        if (HP <= 0.0f)
        {
            Destroy(sensor.GetNearestSensedBot());
            Destroy(gameObject);
            SceneManager.LoadScene(0);
            //audioManager.Play("Death");
            //animator.Play("death");
            //Make a new gameObject for dead hull, or disable scripts?
            //Instantiate(deathFX, transform.position, Quaternion.identity);
        }
        else
        {
            AudioManager.instance.Play("Hit");
            //Instantiate(damageFX, transform.position, Quaternion.identity);
        }
    }
}
