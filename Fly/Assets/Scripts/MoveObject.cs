using UnityEngine;

[DisallowMultipleComponent] 
public class MoveObject : MonoBehaviour
{
    [SerializeField] private Vector3 movePos;  
    [SerializeField] [Range(0,1)]float moveProgress;  
    [SerializeField]  private float moveSpeed;  
    [SerializeField] private float rotateSpeed; 
    private Vector3 startPos;   
    void Start()
    {
        startPos = transform.position;   
    }
    void Update()
    {
        moveProgress = Mathf.PingPong(Time.time * moveSpeed, 1);  
        Vector3 offset = movePos * moveProgress;
        transform.position = startPos + offset;   
        transform.Rotate(0f, 0f, rotateSpeed);   
    }
}
