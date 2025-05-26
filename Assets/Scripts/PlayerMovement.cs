using System;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 moveInput;
    private Rigidbody rb;
    public Transform weapon;
    public GameObject playerLightWeapon;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (PlayerHealth.IsDead) return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        moveInput = new Vector3(moveX, 0f, moveZ).normalized;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // This creates a horizontal plane at the object's Y position
        Plane plane = new Plane(Vector3.up, transform.position);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            Vector3 direction = (hitPoint - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0); // Only rotate around Y
        }
    }

    void FixedUpdate()
    {
        if (PlayerHealth.IsDead) return;

        var newPosition = rb.position + moveSpeed * Time.fixedDeltaTime * moveInput;
        rb.MovePosition(newPosition);
    }

    public void EnableLightWeapon(bool enable) {
        playerLightWeapon.SetActive(enable);
    }
}