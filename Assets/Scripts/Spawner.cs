using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;

    public GameObject Cube;

    public void Place()
    {
        Cube = Instantiate(cubePrefab);        
    }

    public void ApplyTexture(Texture texture, float size)
    {        
        Cube.GetComponent<Renderer>().sharedMaterial.mainTexture = texture;
        Cube.GetComponent<Renderer>().sharedMaterial.mainTextureScale = new Vector2(size, size);
    }

    public void Scale(Vector3 dimensions)
    {
        Cube.transform.localScale = dimensions;
    }


}
