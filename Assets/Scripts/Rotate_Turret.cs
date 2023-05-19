using UnityEngine;

public class Rotate_Turret : MonoBehaviour
{
    public float rotationSpeed = 10f; // Namlu dönüþ hýzý

    void Update()
    {
        // Klavye giriþini al
        float horizontalInput = Input.GetAxis("Horizontal");

        // Yatay giriþe göre nesneyi döndür
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
