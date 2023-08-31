using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
        
    private bool isWalking;
    private const float PLAYER_RADIUS = 0.6f;
    private const float PLAYER_HEIGHT = 2f;

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        float moveDistance = moveSpeed * Time.fixedDeltaTime;
        Vector3 moveDirection = GetMoveDirection(moveDistance);
        if (moveDirection != Vector3.zero) 
            Move(moveDirection, moveDistance);
        
        isWalking = moveDirection != Vector3.zero;
    }
    
    private Vector3 GetMoveDirection(float moveDistance)
    {
        Vector3 moveDir = GetInputVector3();
        
        if (HasCapsuleCollision(moveDir, moveDistance))
        {
            Vector3 moveDirX = moveDir; moveDirX.y = 0; moveDirX.z = 0;
            if  (!HasCapsuleCollision(moveDirX, moveDistance)) 
                return moveDirX;
            
            Vector3 moveDirZ = moveDir; moveDirX.x = 0; moveDirX.y = 0;
            if (!HasCapsuleCollision(moveDirZ, moveDistance))
                return moveDirZ;
            
            return Vector3.zero;
        }
        
        return moveDir;
    }
    
    private Vector3 GetInputVector3()
    {
        Vector2 inputVector = InputManager.Instance.GetMovementVectorNormalized();
        return Vector3.forward * inputVector.y + Vector3.right * inputVector.x;
    }
    
    private bool HasCapsuleCollision(Vector3 moveDirection, float moveDistance)
    {
        return Physics.CapsuleCast(transform.parent.position, 
                                transform.parent.position + Vector3.up * PLAYER_HEIGHT,
                                    PLAYER_RADIUS, 
                                    moveDirection, 
                                    moveDistance);
    }
    
    private void Move(Vector3 moveDirection, float moveDistance)
    {
        transform.parent.position += moveDirection * moveDistance;
    }

    //Rotate follow input direction
    private void HandleRotation()
    {
        // eulerAngles, LookAt(), forward, up , right
        transform.parent.forward = Vector3.Slerp(transform.parent.forward, 
                                                GetInputVector3(), 
                                                Time.fixedDeltaTime * rotateSpeed);
    }
    
    public bool GetIsWalking() => isWalking;
}
