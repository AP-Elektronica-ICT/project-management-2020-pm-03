using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopupSystem : MonoBehaviour
{
    public GameObject dialogbox;
    public Text dialogtext;
    public string dialog;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogbox.SetActive(true);
            dialogtext.text = dialog;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogbox.SetActive(false);
            dialogtext.text = dialog;
        }
    }
  

}
