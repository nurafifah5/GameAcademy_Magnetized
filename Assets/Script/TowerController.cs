using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerController : MonoBehaviour
{
    //private PlayerController player;
    [SerializeField] private Rigidbody2D player;
    //private GameObject player;
    public float pullForce = 100f;
    public float rotateSpeed = 360f;
    private GameObject hookedTower;
    private bool isPulled = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Debug.Log("Tower clicked");
        gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        hookedTower = gameObject;
        if (hookedTower)
        {
            float distance = Vector2.Distance(player.transform.position, hookedTower.transform.position);

            //gravitation toward tower
            Vector3 pullDirection = (hookedTower.transform.position - player.transform.position).normalized;
            float newPullForce = Mathf.Clamp(pullForce / distance, 20, 50);
            player.AddForce(pullDirection * newPullForce);

            //Angular velocity
            player.angularVelocity = -rotateSpeed / distance;
            isPulled = true;
        }
    }

    void OnMouseUp()
    {
        hookedTower = null;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        player.angularVelocity = 0;
        isPulled = false;
    }
    
}
