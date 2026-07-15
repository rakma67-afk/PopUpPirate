using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Gameplay : MonoBehaviour
{
    public GameObject[] Sword;
    public GameObject[] Hole;
    public GameObject[] pirate;
    public int random = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        random = UnityEngine.Random.Range(0, Hole.Length);
        Debug.Log("Random Int: " + random);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) 
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Holes"))
                {
                    int index = Array.IndexOf(Hole, hit.collider.gameObject);
                    Sword[index].SetActive(true);
                    Hole[index].SetActive(false);
                    if (random == index)
                    {
                        GameOver();
                    }
                }
                Debug.Log("Hit object: " + hit.collider.gameObject.name);
            }
        }
        Vector2 scrollvalue = Mouse.current.scroll.ReadValue();
        scrollvalue.y *= 200;
        transform.Rotate(Vector3.up, 20 * scrollvalue.y * Time.deltaTime);

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }

    private void GameOver()
    {
        Rigidbody rb = GetComponentInChildren<Rigidbody>();
        rb.AddForce(UnityEngine.Random.Range(0, 100),1000, UnityEngine.Random.Range(0, 100));
        pirate[0].transform.Rotate(new Vector3(0, 0, 90));
        pirate[0].transform.SetParent(null);
        Debug.Log("Game Over");
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
