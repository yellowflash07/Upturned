using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerScript : MonoBehaviour
{
    [HideInInspector]
    public int health = 0;
    public Slider healthBar;

    public float range;

    bool seamineTriggered = false, doOnce = false, dead = false;
    bool pickedupfish = false;
    bool pressF = false;

    [HideInInspector]
    public static bool impact, fishcap, fishclear, exploded, sink,fishEat, levelComp;

    Animator animator;
    RuntimeAnimatorController animatorController;

    GameObject shark;
    GameObject fish;

    public Image warning;
    public GameObject nextLevel,gameOver;

    int explosionPower = 800;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animatorController = animator.runtimeAnimatorController;
        animator.runtimeAnimatorController = null;

        shark = GameObject.FindGameObjectWithTag("Sharks");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        healthBar.value = health;
        Debug.Log(seamineTriggered);
        if (health >= 100 && !dead)
        {
            animator.runtimeAnimatorController = animatorController;
            animator.Play("crash");
            dead = true;
            sink = true;
            gameOver.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            pressF = !pressF;
        }

        if (!pressF && fish!=null)
        {
            fish.SetActive(true);
            fish.transform.parent = null;
            if (Vector3.Distance(shark.transform.position, fish.transform.position) < range)
            {
                Debug.Log("within range");
                shark.transform.position = Vector3.MoveTowards(shark.transform.position, fish.transform.position, 2 * Time.deltaTime);

                if (shark.transform.position == fish.transform.position && !doOnce)
                {
                    fish.SetActive(false);
                    fishEat = true;
                    doOnce = true;
                }
            }
        }

        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            if (hit.transform.name != "FallDetect")
            {
                Debug.Log(hit.transform.name);
                warning.gameObject.SetActive(true);
            }
            else
            {
                warning.gameObject.SetActive(false);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == "Obstacle")
        {
            Debug.Log("triggered");
            health += 10;
            impact = true;
            
        }

        if (other.tag == "Sharks")
        {
            health += 100;
        }

        if (other.tag == "Seamine")
        {
            other.GetComponent<Animator>().Play("seamine");
            StartCoroutine(explosion(other.gameObject));           
        }

        if (other.tag == "LevelComplete")
        {
            levelComp = true;
            nextLevel.SetActive(true);
        }

        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "ExplosionArea" && seamineTriggered)
        {
            Debug.Log("Inside explosive area");
            health += 30;
            GetComponent<Rigidbody>().AddExplosionForce(explosionPower, Vector3.right,10);
            seamineTriggered = false;
        }

        if (other.tag == "Fish" && pressF)
        {            
            pickedupfish = true;            
            fish = other.gameObject;
            fish.GetComponent<BoxCollider>().enabled = false;
            fish.SetActive(false);
            fish.transform.position = transform.position;
            fish.transform.parent = transform;
            fishcap = true;
        }
    }

    IEnumerator explosion(GameObject seamine)
    {
        yield return new WaitForSeconds(2f);
        seamineTriggered = true;
        exploded = true;
        Destroy(seamine.gameObject);
        Destroy(seamine.transform.parent.gameObject,1);
    }
}
