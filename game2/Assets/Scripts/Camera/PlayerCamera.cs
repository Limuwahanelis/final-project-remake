using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Player player;

    public Vector3 offset;


    public bool CheckForBorders = true;
    public Transform leftScreenBorder;
    public Transform rightScreenBorder;
    public Transform upperScreenBorder;
    public Transform lowerScreenBorder;

    public float smoothTime = 0.3f;

    private bool _followOnXAxis=true;
    private bool _followOnYAxis = true;
    private Vector3 _targetPos;
    
    private Vector3 _velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.transform.position+offset;
    }
    private void Update()
    {
        if (CheckForBorders)
        {
            if (player.transform.position.x < leftScreenBorder.position.x)
            {
                _followOnXAxis = false;
                _targetPos = new Vector3(leftScreenBorder.position.x, player.transform.position.y);
            }
            else
            {
                CheckIfPlayerIsOnRightScreenBorder();
            }

            if (player.transform.position.y < lowerScreenBorder.position.y)
            {
                _followOnYAxis = false;
                _targetPos = new Vector3(_targetPos.x, lowerScreenBorder.position.y, _targetPos.z);

            }
            else
            {
                CheckIfPlayerIsOnUpperScreenBorder();
            }
        }
        
    }
    private void FixedUpdate()
    {
        if (CheckForBorders)
        {
            if (_followOnXAxis)
            {
                _targetPos = new Vector3(player.transform.position.x, _targetPos.y);
            }
            if (_followOnYAxis)
            {
                _targetPos = new Vector3(_targetPos.x, player.transform.position.y);
            }
        }
        else
        {
            _targetPos = player.transform.position;
        }
            _targetPos += offset;
            transform.position = Vector3.SmoothDamp(transform.position, _targetPos, ref _velocity, smoothTime);
    }

    private void CheckIfPlayerIsOnRightScreenBorder()
    {
        if (player.transform.position.x > rightScreenBorder.position.x)
        {
            _followOnXAxis = false;
            _targetPos = new Vector3(rightScreenBorder.position.x, player.transform.position.y);
        }
        else
        {
            _followOnXAxis = true;
        }
    }
    private void CheckIfPlayerIsOnUpperScreenBorder()
    {
        if (player.transform.position.y > upperScreenBorder.position.y)
        {
            _followOnYAxis = false;
            _targetPos = new Vector3(_targetPos.x, upperScreenBorder.position.y,_targetPos.z);
        }
        else
        {
            _followOnYAxis = true;
        }
    }
}
