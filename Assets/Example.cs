using UnityEngine;

public class Example : MonoBehaviour
{
    // Awake is called when the script instance is being loaded
    void Awake()
    {
        Debug.Log("Awake called");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Debug.Log("Start called");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update called");
    }

    //fixed update is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        Debug.Log("FixedUpdate called");
    }   
}
