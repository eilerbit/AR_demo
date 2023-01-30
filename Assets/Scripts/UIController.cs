using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Canvas ARcanvas;

    [SerializeField] private Transform content;
    [SerializeField] private Transform horizontalLayoutGroupPrefab;
    [SerializeField] private Item itemPrefab;
    [SerializeField] private Transform popup;

    [SerializeField] private Button okButton;
    [SerializeField] private Button cancelButton;

    [SerializeField] private Button backButton;

    [SerializeField] private TMP_InputField inputFieldWidth;
    [SerializeField] private TMP_InputField inputFieldLength;
    [SerializeField] private TMP_InputField inputFieldDepth;

    public IAppManager manager;

    private Item[] items;

    private Texture texture;
    private float size;

    public void Initialize(IAppManager manager)
    {
        okButton.onClick.AddListener(() => OkButtonClicked());
        cancelButton.onClick.AddListener(() => CancelButtonClicked());
        backButton.onClick.AddListener(() => BackButtonClicked());

        this.manager = manager;
    }

    public void InstantiateItems(int itemsQuantity)
    {
        items = new Item[itemsQuantity];

        Transform horizontalLayoutGroup = null;
        
        for (int i = 0; i < itemsQuantity; i++)
        {
            if (i % 2 == 0) horizontalLayoutGroup = Instantiate(horizontalLayoutGroupPrefab, content);

            items[i] = Instantiate(itemPrefab.gameObject, horizontalLayoutGroup).GetComponent<Item>();
            items[i].Initialize();
            items[i].PreviewButtonClicked += OnPreviewButtonClicked;
        }

    }

    public void AssignItemsData(List<DataItem> data)
    {
        for (int i = 0; i < data.Count; i++)
        {
            items[data[i].Index].Index = data[i].Index;
            items[data[i].Index].Title.text = data[i].Title;
            items[data[i].Index].Preview.texture = data[i].Preview;
            items[data[i].Index].Texture = data[i].Texture;
            items[data[i].Index].Size = data[i].Size;
        }
    }

    public void OnPreviewButtonClicked(int itemIndex)
    {
        popup.gameObject.SetActive(true);

        texture = items[itemIndex].Texture;
        size = items[itemIndex].Size;
    }

    private void OkButtonClicked()
    {
        canvas.gameObject.SetActive(false);
        ARcanvas.gameObject.SetActive(true);

        Vector3 dimensions = new Vector3(float.Parse(inputFieldWidth.text) / 100, float.Parse(inputFieldLength.text) / 100, float.Parse(inputFieldDepth.text) / 100);

        manager.StartAR(texture, size, dimensions);
    }

    private void CancelButtonClicked()
    {
        popup.gameObject.SetActive(false);
    }

    private void BackButtonClicked()
    {
        canvas.gameObject.SetActive(true);
        ARcanvas.gameObject.SetActive(false);

        manager.StopAR();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].PreviewButtonClicked -= OnPreviewButtonClicked;
        }        
    }
}
