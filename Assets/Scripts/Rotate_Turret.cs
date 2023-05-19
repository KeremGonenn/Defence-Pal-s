using UnityEngine;

public class Rotate_Turret : MonoBehaviour
{
    public float rotationSpeed = 10f; // Namlu d�n�� h�z�

    void Update()
    {
        // Klavye giri�ini al
        float horizontalInput = Input.GetAxis("Horizontal");

        // Yatay giri�e g�re nesneyi d�nd�r
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
