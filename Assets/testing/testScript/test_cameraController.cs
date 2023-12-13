using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_cameraController : MonoBehaviour
{
    public float moveSpeed = 10f; // ความเร็วของการขยับ
    public Vector3 targetPosition = new Vector3(0.06f, 0.95f, 7.331f); // ตำแหน่งที่ต้องการให้กล้องไป
   
   
    void Update()
    {
        // กด W เพื่อเริ่มขยับกล้อง
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveCameraToTarget();
        }
    }

    void MoveCameraToTarget()
    {
        // ขยับกล้องไปที่ตำแหน่ง targetPosition
        transform.position = targetPosition;

        // ค้างไว้ 3 วินาที
        Invoke("MoveCameraToStartPosition", 3f);
    }

    void MoveCameraToStartPosition()
    {
        // ขยับกล้องกลับไปตำแหน่งเดิม (0.06, 0.26, 7.46)
        transform.position = new Vector3(0.06f, 0.26f, 7.46f);
    }
}
