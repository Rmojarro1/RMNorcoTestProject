using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridMovement))]
[RequireComponent(typeof(GridObject))]

public class PlayerMovement : GridMovement
{
    // Start is called before the first frame update
    void Start()
    {

        OnMovementStart += StartMovementH;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();


        
        if(Input.GetKeyDown(KeyCode.W))
        {
            TryMoveToNeighborInPosition(Vector2Int.up, true, true, true); //Vector2Int.down, right, left, up
        }
    }

    public void playerSwipe(string direction)
    {
        if (direction == "Right")
        {
            TryMoveToNeighborInPosition(Vector2Int.right, false, false, true);
            Debug.Log("Move right"); 
        }
        else if (direction == "Left")
        {
            TryMoveToNeighborInPosition(Vector2Int.left, false, false, true);
        }
        else if(direction == "Up")
        {
            TryMoveToNeighborInPosition(Vector2Int.up, false, false, true);
        }
        else if (direction == "Down")
        {
            TryMoveToNeighborInPosition(Vector2Int.down, false, false, true);
        }
        //TryMoveToNeighborInPosition(Vector2Int.up, false, false, true);
    }

    protected void OnDestroy()
    {
        OnMovementStart -= StartMovementH;
    }

    private void StartMovementH(GridMovement gridMovement, GridTile startGridTile, GridTile endGridTile)
    {
        //Check whatever you want when OnMovementStart happens for the player right here. this is when
        //it is going to move because it is available. After this, the actual movement happens.
    }
}


/*public class PlayerMovement : MonoBehaviour
{
        private Rigidbody rb; 
    public float speed = 5f; 

    private Vector3 input = Vector3.zero; 
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        input = Vector3.zero; 
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical"); 

        if (input != Vector3.zero)
        {
            transform.forward = input; 
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * speed * Time.fixedDeltaTime); 
    }

}*/
