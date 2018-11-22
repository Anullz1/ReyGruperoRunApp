using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{

    public Transform lookAt;

    private Vector3 offset = new Vector3(0.36f, 9.5f, -5.85f);

  private void Start()
    {
        transform.position = lookAt.position + offset;
    }


    private void LateUpdate()
    {
      Vector3 desiredPosition = lookAt.position + offset;
      desiredPosition.x = 0;
      transform.position = Vector3.Lerp(transform.position,desiredPosition,Time.deltaTime);
    }
}
