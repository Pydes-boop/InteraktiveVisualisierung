//script by Philipp
//simple ladder climbing script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class LadderClimb : MonoBehaviour
{
    public GameObject from, to, up, down;
    private GameObject Player;
    private Vector2 ViewDir = new Vector2(0,0);
    private Canvas_Script script;
    public float speed = 1f;
    public float triggerSize = 1f;
    private float progress = 0f;

    private Vector3 dir;

    private bool isClimbing = false;

    private void Awake()
    {
        this.dir = to.transform.position - from.transform.position;
        Player = GameObject.Find("FirstPersonPlayerTime");
        Vector3 dir = down.transform.position - up.transform.position;
        ViewDir = new Vector2(dir.x, dir.z);
    }
    private void Start()
    {
        script = GameObject.Find("InventoryCanvas").GetComponent<Canvas_Script>();
    }

    private void Update()
    {
        if (!isClimbing)
        {
            checkInput();
        }
        else 
        {
            climb();
        }
        
    }

    private void checkInput() 
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            float dist = (Player.transform.position - up.transform.position).magnitude;
            if (dist < triggerSize) 
            {
                SetIsClimbing(true);
                progress = 0;
                Player.transform.position = from.transform.position;
                setRotation();
                return;
            }

            dist = (Player.transform.position - down.transform.position).magnitude;

            if (dist < triggerSize)
            {
                SetIsClimbing(true);
                progress = 1;
                Player.transform.position = to.transform.position;
                setRotation();
                return;
            }
        }
    }

    private void climb() 
    {
        //break out in case player moves
        //and reset state to allow player to climb again
        if (Keyboard.current.aKey.isPressed
            || Keyboard.current.sKey.isPressed
            || Keyboard.current.dKey.isPressed
            || Keyboard.current.wKey.isPressed
            || Keyboard.current.spaceKey.isPressed)
        {
            SetIsClimbing(false);
            progress = 0;
            resetRotation();
            return;
        }

        //setting players possition based on key pressed and climb direction
        if (Keyboard.current.eKey.isPressed && this.progress <= 1)
        {
            this.progress += this.speed * Time.deltaTime;
            this.Player.transform.position += this.dir * this.speed * Time.deltaTime;
            setRotation();
        }
        else if (Keyboard.current.qKey.isPressed && this.progress >= 0)
        {
            this.progress -= this.speed * Time.deltaTime;
            this.Player.transform.position += -this.dir * this.speed * Time.deltaTime;
            setRotation();
        }
    }

    //sets the players rotation when climbing up and down
    private void setRotation() 
    {
        this.Player.transform.rotation = Quaternion.LookRotation(new Vector3(ViewDir.x, 0, ViewDir.y), dir.normalized);
    }

    private void resetRotation()
    {
        this.Player.transform.rotation = Quaternion.LookRotation(this.Player.transform.forward, new Vector3(0, 1, 0));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0,0.9f,0.7f);
        Gizmos.DrawLine(from.transform.position, to.transform.position);
        Gizmos.DrawLine(up.transform.position, from.transform.position);
        Gizmos.DrawLine(down.transform.position, to.transform.position);

        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0.9f, 0.7f);
        Gizmos.DrawWireSphere(up.transform.position, triggerSize);
        Gizmos.DrawWireSphere(down.transform.position, triggerSize);
    }
    private void SetIsClimbing(bool isClimbing)
    {
        this.isClimbing = isClimbing;
        script.ClimbingStatusChanged(isClimbing);
    }


}
