using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    private AudioSource audioSource;
    private void OnMouseEnter(){
        IncreaseScale(true);
        audioSource.Play();
    }
    private void OnMouseExit(){
        IncreaseScale(false);
    }

    private Vector3 initialScale;
    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void IncreaseScale(bool status){
        Vector3 finalScale = initialScale;
        if(status){
            finalScale = initialScale * 1.1f;
        }

        transform.localScale = finalScale;
    }
}
