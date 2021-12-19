using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Ball : MonoBehaviour
{
    private AudioSource collideSound;
    private bool bIsOnTheMove = true;
    public Rigidbody player;
    private float moveSpeed = 6f;
    private bool positionSet = false;
    private bool holding;
    //private bool shooting = false;
    private float ballForce = 0f;
    private Vector3 newPosition;
    private int count;
    public GameObject[] positions;
    private Vector3 startPosition;
    private int currentPoint;
    private Vector3 spawnPosition;
    public GameManager manager;
    // Update is called once per frame
    private void Start()
    {
        collideSound = player.GetComponent<AudioSource>();
        bIsOnTheMove = true;
        positionSet = false;
        holding = false;
        //shooting = false;
        player = GetComponent<Rigidbody>();
        spawnPosition = player.transform.position;
        currentPoint = 0;
        count = 0;
        Debug.Log("Chances :" + PlayerPrefs.GetInt("Chances").ToString());
        setColor();
        
    }
    private void Update()
    {
        if (!positionSet)
        {
            SetPosition();
        }

        if (holding)
        {
            Shoot();
        }
        StartCoroutine(CheckMoving());
        if (bIsOnTheMove == false) StopCoroutine(CheckMoving());
    }
    void SetPosition()
    {
        if (player.transform.position == positions[currentPoint].transform.position)
        {
            currentPoint = (currentPoint + 1) % 2;
        }
        else
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, positions[currentPoint].transform.position, moveSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //positionSet = true;
            holding = true;
        }
    }
    void setColor()
    {
        if(PlayerPrefs.GetInt("Chances")%2 == 0)
        {
            player.GetComponent<Renderer>().material.color = Color.green;
        }
        else player.GetComponent<Renderer>().material.color = Color.red;

    }
    void Shoot()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ballForce += 200f;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            positionSet = true;
            holding = false;
            //shooting = true;
            player.AddForce(Vector3.forward * ballForce);
            //shooting = false;
        }

    }
    public void Respawn()
    {
        count += 1;
        if (count == 1)
        {
            manager.roundFinish();
        }

    }
    private IEnumerator CheckMoving()
    {
        Vector3 startPos = player.transform.position;
        yield return new WaitForSeconds(1f);
        Vector3 finalPos = player.transform.position;
        if (startPos.x == finalPos.x && startPos.y == finalPos.y
            && startPos.z == finalPos.z)
            bIsOnTheMove = false;
        if (bIsOnTheMove == false) Invoke("Respawn", 4f);
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform.tag == "pins")
        {
            Invoke("Respawn", 5f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            Invoke("Respawn", 3f);
        }
        if(collision.transform.tag == "pins") collideSound.Play();
    }
    //void ResetBall()
    //{
    //    player.transform.position = Pos;
    //    player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    //    player.transform.rotation = Quaternion.identity;
    //    cam.transform.position = player.position + offset;
    //    cam.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    cam.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    //    cam.transform.rotation = Quaternion.identity;
    //}
}
