using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    public static CollectionManager Instance;
    public int count = 0;
    public Text TMP_Text; // O podés usar TMP_Text si usás TextMeshPro

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddCollectible()
    {
        count++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (TMP_Text != null)
        {
            TMP_Text.text = count.ToString();
        }
    }
}