using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUIController : MonoBehaviour
{
    public static DialogueUIController Instance { get; private set; }
    
    [SerializeField] private float dialogueWidth;

    public float DialogueWidth { get => dialogueWidth; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
