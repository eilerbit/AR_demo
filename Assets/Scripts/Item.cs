using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI Title;
    [SerializeField] public RawImage Preview;
    [SerializeField] private Button button;

    public event Action<int> PreviewButtonClicked;

    public int Index;
    public Texture Texture;
    public float Size;

    public void Initialize()
    {
        button.onClick.AddListener(() => PreviewButtonClicked(Index));
    }
    
}
