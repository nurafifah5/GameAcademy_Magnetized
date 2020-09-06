using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float moveSpeed = 1f;
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

    public void restartPosition()
    {
        //set to start position
        this.transform.position = startPosition;

        //restart rotation 
        this.transform.rotation = Quaternion.Euler(0f, 0f, 90f);

        //set isCrashed to false
        isCrashed = false;
    }
}
