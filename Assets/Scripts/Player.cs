using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    // Start is called before the first frame update
    public Deck playerDeck;
    public static Player Instance {
        get;
        private set;
    }
    void Awake(){
        if(Instance != null){
            Destroy(gameObject);
        } else {
            Instance = this;
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
