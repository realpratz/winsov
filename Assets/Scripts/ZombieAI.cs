using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public float attackRange=2f;
    public float attackCooldown=1.5f;
    public int zombieMoney;

    [SerializeField] private AudioClip[] sfx;
    private AudioSource sfxPlayer;

    private float currentHealth;
    public float maxHealth=100f;

    private NavMeshAgent agent;
    private Animator animator;
    private Transform player;
    private PlayerController pc;
    private float lastAttacktime;
    private bool isDead;

    public Gun gunScript;
    public ZombieSpawner zs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isDead=false;
        currentHealth=maxHealth;
        agent=GetComponent<NavMeshAgent>();
        animator=GetComponent<Animator>();
        sfxPlayer=GetComponent<AudioSource>();
        GameObject playerObj=GameObject.FindGameObjectWithTag("Player");
        player=playerObj.transform;
        pc=player.GetComponent<PlayerController>();
        zs=GameObject.FindWithTag("Spawner").GetComponent<ZombieSpawner>();
        gunScript = playerObj.GetComponentInChildren<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        float distance=Vector3.Distance(transform.position,player.position);

        if (distance > attackRange)
        {
            agent.isStopped=false;
            agent.SetDestination(player.position);

            animator.SetBool("isAttack",false);
        }
        else
        {
            FacePlayer();
            agent.isStopped=true;

            animator.SetBool("isAttack",true);

            if(Time.time > lastAttacktime + attackCooldown)
            {
                sfxPlayer.PlayOneShot(sfx[0]);
                pc.TakeDamage(Random.Range(5f, 15f));
                lastAttacktime=Time.time;
            }
        }
    }

    void FacePlayer()
    {
        Vector3 direction=(player.position-transform.position).normalized;
        direction.y=0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation=Quaternion.LookRotation(direction);
            transform.rotation=Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime*5f);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;
        
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        
        int reward = Random.Range(30, 71);
        gunScript.AddMoney(reward);

        sfxPlayer.PlayOneShot(sfx[1]);

        zs.score++;

        agent.isStopped = true;
        animator.SetBool("isDead", true);
        animator.SetBool("isAttack", false);

        GetComponent<Collider>().enabled = false; 
        
        Destroy(gameObject, 5f);
    }
}
