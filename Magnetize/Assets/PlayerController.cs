using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float moveSpeed = 5f;

    public float pullForce = 100f;
    public float rotateSpeed = 360f;
    // Tugas : closestTower diubah menjadi public agar bisa diakses oleh Towes;
    public GameObject closestTower;
    private GameObject hookedTower;
    public bool isPulled = false;
    private UIController uiControl;
    private AudioSource myAudio;
    private bool isCrashed = false;
    public Vector3 startPosition;

    void Start()
    {
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();
        uiControl = GameObject.Find("Canvas").GetComponent<UIController>();
        myAudio = this.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        rb2D.velocity = -transform.up * moveSpeed;
        // Tugas : Input.Getkey diubah menjadi Input.GetMouseButtonDown agar bukan mendeteksi tombol z namun klik mouse
        if(Input.GetMouseButtonDown(0) && !isPulled)
        {
            if (closestTower && !hookedTower)
            {
                hookedTower=closestTower;
            }
            if (hookedTower)
            {
                float distance = Vector2.Distance(this.gameObject.transform.position, hookedTower.transform.position);

                Vector3 pullDirection = (hookedTower.transform.position - this.gameObject.transform.position).normalized;
                float pullForceNew = Mathf.Clamp(this.pullForce/(distance), 30, 100);
                rb2D.AddForce(pullForceNew * pullDirection);

                rb2D.angularVelocity = -rotateSpeed/distance;
                isPulled = true;
            }
        }

        // Tugas : Input.GetkeyUp diubah menjadi Input.GetMouseButtonUp agar bukan mendeteksi tombol z namun klik mouse
        if (Input.GetMouseButtonUp(0))
        {
            isPulled = false;
            hookedTower = null;
            rb2D.angularVelocity = 0;
        }

        if (isCrashed)
        {
            if (!myAudio.isPlaying)
            {
                Restart_Position();
            }
        }
        else
        {
            rb2D.velocity = -transform.up * moveSpeed;

        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Wall")
        {
            if (!isCrashed)
            {
                myAudio.Play();
                rb2D.velocity = new Vector3(0,0,0);
                rb2D.angularVelocity = 0;
                isCrashed = true;
            }
        }        
    }

    void OnTriggerEnter2D(Collider2D other) {
        //  Bonus : penambahan LoadScene untuk mberpindah ke scene2
        if (other.gameObject.tag == "Goal")
        {
            SceneManager.LoadScene("Scene2");
        }

        if (other.gameObject.tag == "Goal2")
        {
            print("Level Clear !!");
            uiControl.End_Game();
        }        
    }

// Tugas : function onTriggerStay2d di hapus agar tidak mendeteksi 2d collider
    // void OnTriggerStay2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "Tower")
    //     {
    //         closestTower = other.gameObject;
    //         other.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    //     }        
    // }

    void OnTriggerExit2D(Collider2D other)
    {
        if (isPulled)
        {
            return;
        }

        if (other.gameObject.tag == "Tower")
        {
            closestTower = null;
            other.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }        
    }

    void Restart_Position(){
        this.transform.position = startPosition;
        this.transform.rotation = Quaternion.Euler(0,0,90f);
        isCrashed = false;
        if (closestTower)
        {
            closestTower.GetComponent<SpriteRenderer>().color = Color.white;
            closestTower = null;
        }
    }


}
