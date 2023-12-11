using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameDatabase : MonoBehaviour
{
    // Start is called before the first frame update
    public static InGameDatabase instance;
    public Vector2 playerPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
