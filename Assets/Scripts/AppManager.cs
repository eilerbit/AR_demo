using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour, IAppManager
{
    [SerializeField] private UIController uiController;
    [SerializeField] private string dataURL;

    [SerializeField] private Camera cameraObject;
    [SerializeField] private Transform ARobject;

    [SerializeField] private Spawner cubeSpawner;

    [SerializeField] private ARInputModule ARInputModule;

    async void Awake()
    {
        DataLoader dataLoader = new DataLoader();
        uiController.Initialize(this);        

        int itemQuantity = await dataLoader.GetItemQuantity(dataURL);
        int itemGroupToLoad = 0;

        uiController.InstantiateItems(itemQuantity);

        if (itemQuantity >= 10) itemGroupToLoad = 4;

        else itemGroupToLoad = itemQuantity;

        int k = itemQuantity % itemGroupToLoad;

        int itemLoadLinit = (itemQuantity - k) / itemGroupToLoad;

        for (int i = 0; i <= itemLoadLinit; i++)
        {
            List<DataItem> data = await dataLoader.LoadData(itemGroupToLoad);

            uiController.AssignItemsData(data);

            if(i + 1 == itemLoadLinit) itemGroupToLoad = k;                        
        }

    }
    public void StartAR(Texture texture, float size, Vector3 dimensions)
    {
        cameraObject.gameObject.SetActive(false);
        ARobject.gameObject.SetActive(true);

        cubeSpawner.Place();
        cubeSpawner.ApplyTexture(texture, size);
        cubeSpawner.Scale(dimensions);

        ARInputModule.Initialize(cubeSpawner.Cube.GetComponent<IDraggable>());
    }

    public void StopAR()
    {
        cameraObject.gameObject.SetActive(true);
        ARobject.gameObject.SetActive(false);

        Destroy(cubeSpawner.Cube);
    }
}
