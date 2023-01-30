

using UnityEngine;

public interface IAppManager
{
    void StartAR(Texture texture, float size, Vector3 dimensions);

    void StopAR();
}
