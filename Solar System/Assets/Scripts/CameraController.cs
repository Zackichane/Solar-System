using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float movementSpeed = 100f;  // Vitesse de déplacement
    public float mouseSensitivity = 500f;  // Sensibilité de la souris
    public float verticalSpeed = 100f;  // Vitesse de déplacement vertical (Q et E)
    public float speedMultiplier = 3f;  // Multiplicateur de vitesse lorsque la barre d'espace est enfoncée
    public GameObject objectToSpawn;  // Objet à faire apparaître
    public float spawnDistance = 2f;  // Distance à laquelle l'objet apparaît devant la caméra

    private float rotationX = 0f;  // Rotation autour de l'axe X (haut-bas)

    void Update()
    {
        // Vitesse actuelle (tripler la vitesse si la barre d'espace est enfoncée)
        float currentSpeed = Input.GetKey(KeyCode.Space) ? movementSpeed * speedMultiplier : movementSpeed;

        // Déplacement avec les touches W, A, S, D
        float horizontal = Input.GetAxis("Horizontal");  // A et D
        float vertical = Input.GetAxis("Vertical");  // W et S

        Vector3 movement = transform.right * horizontal + transform.forward * vertical;

        // Déplacement vertical avec Q (descendre) et E (monter)
        if (Input.GetKey(KeyCode.Q))
        {
            movement += Vector3.down * verticalSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            movement += Vector3.up * verticalSpeed * Time.deltaTime;
        }

        transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);

        // Rotation de la caméra avec la souris
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;  // On inverse l'axe Y pour un comportement plus intuitif
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);  // Limite l'angle vertical de la caméra

        transform.localRotation = Quaternion.Euler(rotationX, transform.localRotation.eulerAngles.y + mouseX, 0f);

        // Spawn object when C is pressed
        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        // Vérifier si l'objet à faire apparaître est assigné
        if (objectToSpawn != null)
        {
            // Calculer la position d'apparition de l'objet (devant la caméra)
            Vector3 spawnPosition = transform.position + transform.forward * spawnDistance;
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Aucun objet assigné à 'objectToSpawn'!");
        }
    }
}
