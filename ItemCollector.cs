using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int collectables = 0;

    [SerializeField] private Text CollectableText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectables"))
        {
            Destroy(collision.gameObject);
            collectables++;
            Debug.Log("Collectables: " + collectables);
            CollectableText.text = "Collectables: " + collectables;
        }
    }
}
