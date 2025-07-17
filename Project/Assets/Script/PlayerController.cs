using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using Input = UnityEngine.Input;




public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed = 75f;
    public float jumpForce;
    public Rigidbody rig;
    public Animator anim;

    public int health;

    public int coinCount;

    private void Move()
    {
        //get the imput axis
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 rotation = Vector3.up * x;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);


        //calculate a direction relative to where we are facing 
        Vector3 dir = (transform.forward * z + transform.right * x) * moveSpeed;
        dir.y = rig.velocity.y;
        //set that as our velocity
        rig.velocity = dir;
        rig.MoveRotation(rig.rotation * angleRot);

        //check if the player is moving 
        if (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }
    void TryJump()
    {
        //create a ray facing down
        Ray ray = new Ray(transform.position, Vector3.down);
        //shoot the raycast
        if (Physics.Raycast(ray, 2f))
        {
            anim.SetTrigger("isJumping");
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //input for movement
        Move();

        //inout for jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Enemy")
        {
            health -= 5;
        }

        if (other.gameObject.name == "FallCollider")
        {
            SceneManager.LoadScene(0);
        }
    }
}