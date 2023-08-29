using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
        
    private bool isWalking;
    private const float PLAYER_RADIUS = 0.6f;
    private const float PLAYER_HEIGHT = 2f;

    private void FixedUpdate() => HandleMovement();

    private void HandleMovement()
    {
        Vector2 inputVector = GetMovementVectorNormalized();
        float moveDistance = moveSpeed * Time.fixedDeltaTime;
        Vector3 moveDirection = GetMoveDirection(inputVector, moveDistance);
        isWalking = moveDirection != Vector3.zero;

        if (moveDirection == Vector3.zero) return;
        Move(moveDirection, moveDistance);
        RotateFollowMoveDirection(moveDirection);
        //Debug.DrawRay(transform.parent.position, moveDirection, Color.red, PLAYER_RADIUS);
    }

    private Vector2 GetMovementVectorNormalized() => InputManager.Instance.GetMovementVectorNormalized();
    
    private Vector3 GetMoveDirection(Vector2 inputVector, float moveDistance)
    {
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        if (IsHasCapsuleCollision(moveDirection, moveDistance))
        {
            Vector3 moveDirX = new Vector3(moveDirection.x, 0, 0);
            if (!IsHasCapsuleCollision(moveDirX, moveDistance)) {
                return moveDirX;
            }
            
            Vector3 moveDirZ = new Vector3(0, 0, moveDirection.z);
            if (!IsHasCapsuleCollision(moveDirZ, moveDistance)){
                return moveDirZ;
            }
            return Vector3.zero;
        }
        
        return moveDirection;
    }
    
    private bool IsHasCapsuleCollision(Vector3 moveDirection, float moveDistance)
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

    private void RotateFollowMoveDirection(Vector3 moveDirection)
    {
        // eulerAngles, LookAt(), forward, up , right
        transform.parent.forward = Vector3.Slerp(transform.parent.forward, 
                                                moveDirection, 
                                                Time.fixedDeltaTime * rotateSpeed);
    }
    
    public bool GetIsWalking() => isWalking;
}
