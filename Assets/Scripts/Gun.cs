using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using TMPro;

public class Gun : MonoBehaviour
{
    public float range=10f;
    public float damage;

    public int maxAmmo=10;
    public int totalAmmo=30;
    private int currentAmmo;
    public float reloadTime=1f;
    private bool isReloading=false;

    [SerializeField] private AudioClip[] sfx;
    private AudioSource sfxPlayer;

    public int money=0;
    public int ammoCost=100;
    public int ammoPack=30;

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI moneyText;

    public Camera cam;
    public Image crosshairImage;

    public Color defaultColor=Color.white;
    public Color enemyDetectedColor=Color.red;

    public GameObject trail;

    private ZombieAI zom;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim=GetComponent<Animator>();
        trail.SetActive(false);
        sfxPlayer=GetComponent<AudioSource>();

        currentAmmo=maxAmmo;
        UpdateAmmoUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(isReloading) return;

        ColorCursor();

        if(Input.GetKeyDown(KeyCode.R))
        {
            if(currentAmmo<maxAmmo && totalAmmo > 0)
            {
                StartCoroutine(Reload());
                return;                
            }
            else if (totalAmmo <= 0)
            {
                BuyAmmo();
            }
        }

        if(currentAmmo<=0 && Input.GetButtonDown("Fire1") && totalAmmo>0)
        {
            StartCoroutine(Reload());
            return;                
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (currentAmmo > 0)
            {
                StartCoroutine(showTrail());
                Shoot();                
            }
            else
            {
                StartCoroutine(emptyFire());
            }
        }
    }

    public void ColorCursor()
    {
        Ray ray=cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, range))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                crosshairImage.color=enemyDetectedColor;
            }
            else
            {
                crosshairImage.color=defaultColor;                
            }
        }
        else
        {
            crosshairImage.color=defaultColor;
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateAmmoUI();
    }

    public void Shoot()
    {
        if (currentAmmo <= 0) return;

        currentAmmo--;
        UpdateAmmoUI();

        sfxPlayer.PlayOneShot(sfx[0]);

        Ray ray=cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, range))
        {
            zom=hit.transform.GetComponent<ZombieAI>();
            if (zom!=null)
            {
                zom.TakeDamage(Random.Range(10f, 40f));
            }
        }
    }

    public void BuyAmmo()
    {
        if (money >= ammoCost)
        {
            money-=ammoCost;
            totalAmmo+=ammoPack;

            sfxPlayer.PlayOneShot(sfx[3]);

            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        sfxPlayer.PlayOneShot(sfx[2]);
        isReloading=true;
        anim.SetBool("isReload",true);
        trail.SetActive(true);
        yield return new WaitForSeconds(reloadTime);
        int bulletsToLoad=Mathf.Min(maxAmmo-currentAmmo,totalAmmo);
        currentAmmo+=bulletsToLoad;
        totalAmmo-=bulletsToLoad;
        UpdateAmmoUI();
        trail.SetActive(false);
        anim.SetBool("isReload",false);      
        isReloading=false;  
    }

    IEnumerator emptyFire()
    {
        sfxPlayer.PlayOneShot(sfx[1]);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator showTrail()
    {
        anim.SetBool("isShoot",true);
        trail.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        trail.SetActive(false);
        anim.SetBool("isShoot",false);
    }

    public void UpdateAmmoUI()
    {
        ammoText.text=currentAmmo+"/"+totalAmmo;
        moneyText.text="SUR " + money;
    }
}
