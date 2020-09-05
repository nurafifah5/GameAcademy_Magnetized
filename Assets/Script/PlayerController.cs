using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float moveSpeed = 5f;
    public float pullForce = 100f;
    public float rotateSpeed = 360f;
    private GameObject closestTower;
    private GameObject hookedTower;
    private bool isPulled = false;
    private UIController uiControl;
    private AudioSource myAudio;
    private bool isCrashed = false;
    private Vector3 startPosition = new Vector3(-6.43f, 2.61f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();
        myAudio = this.gameObject.GetComponent<AudioSource>();
        uiControl = GameObject.Find("Canvas").GetComponent<UIController>();   
    }

    // Update is called once per frame
   void Update()
    {
        rb2D.velocity = -transform.up * moveSpeed;
        if (isCrashed)
        {
            if (!myAudio.isPlaying)
            {
                //restart scene
                restartPosition();
                uiControl.crashGame();
            }
        }
        
        if (Input.GetKey(KeyCode.Z) && !isPulled)
        {
            Debug.Log("Z clicked");
            if (closestTower != null && hookedTower == null)
            {
                hookedTower = closestTower;
            }
            if (hookedTower)
            {
                float distance = Vector2.Distance(transform.position, hookedTower.transform.position);

                //Gravitation toward tower
                Vector3 pullDirection = (hookedTower.transform.position - transform.position).normalized;
                float newPullForce = Mathf.Clamp(pullForce / distance, 20, 50);
                rb2D.AddForce(pullDirection * newPullForce);

                //Angular velocity
                rb2D.angularVelocity = -rotateSpeed / distance;
                isPulled = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            rb2D.angularVelocity = 0;
            isPulled = false;
            hookedTower = null;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            //hide game object
            //this.gameObject.SetActive(false);
            if (!isCrashed)
            {
                //play sfx
                myAudio.Play();
                rb2D.velocity = new Vector3(0f, 0f, 0f);
                rb2D.angularVelocity = 0f;
                isCrashed = true;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            Debug.Log("Level Clear!");
            restartPosition();
            uiControl.endGame();
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tower")
        {
            closestTower = collision.gameObject;

            //Change tower color back to green as indicator
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (isPulled) return;
        if(collision.gameObject.tag == "Tower")
        {
            closestTower = null;
            hookedTower = null;
            //change tower color back to normal
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void restartPosition()
    {
        //set to start position
        this.transform.position = startPosition;

        //restart rotation 
        this.transform.rotation = Quaternion.Euler(0f, 0f, 90f);

        //set isCrashed to false
        isCrashed = false;
        if (closestTower)
        {
            closestTower.GetComponent<SpriteRenderer>().color = Color.white;
            closestTower = null;
        }
    }
}
