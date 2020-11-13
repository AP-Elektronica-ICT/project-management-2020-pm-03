using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class END : MonoBehaviour
{
    public UnityEvent onTrigger;
    public bool destroyAfterTrigger = true;
    
    
        void Awake()
        {
            if(onTrigger == null)
            {
                onTrigger = new UnityEvent();
            }
        }
    

    public void OnTriggerEnter2D(Collider2D other)
    {
        onTrigger.Invoke();
        if (destroyAfterTrigger)
        {
            Destroy(gameObject);
        }
    }


}
