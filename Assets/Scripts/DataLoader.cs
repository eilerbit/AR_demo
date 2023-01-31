using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

public class DataLoader
{
    private JSONNode parsedData;
    private int dataItemIndex = 0;

    public async Task<int> GetItemQuantity(string dataURL)
    {
        using UnityWebRequest request = UnityWebRequest.Get(dataURL);

        var operation = request.SendWebRequest();

        while (!operation.isDone) await Task.Delay(1000 / 30);

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonData = request.downloadHandler.text;

            parsedData = JSON.Parse(jsonData);

            return parsedData["values"].Count - 1;
        }

        else
        {
            Debug.Log($"{dataURL} - Error: \n" + request.error);
            return 0;
        }

    }

    public async Task<List<DataItem>> LoadData(int itemsToLoadQuantity)
    {        
        List <DataItem> data = new List <DataItem>();

        int limit = itemsToLoadQuantity + dataItemIndex;

        for (int i = dataItemIndex + 1; i < limit + 1; i++)
        {
            var row = JSON.Parse(parsedData["values"][i].ToString());

            List<string> strings = row.AsStringList;

            DataItem dateItem = new DataItem();

            dateItem.Index = i - 1;                        
            dateItem.Title = strings[0].ToString();
            dateItem.Preview = await GetTexture(strings[1].ToString());
            dateItem.Texture = await GetTexture(strings[2].ToString());
            dateItem.Size = float.Parse(strings[3]);

            data.Add(dateItem);

            dataItemIndex = i; 
        }

        return data;
    }    
    
    private async Task<Texture> GetTexture(string url)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {            
            var asyncOp = request.SendWebRequest();
                         
            while (asyncOp.isDone == false) await Task.Delay(1000 / 30);
            
            if(request.result != UnityWebRequest.Result.Success)
            {                
                Debug.Log($"{request.error}, URL:{request.url}");
                                
                return null;
            }

            else return DownloadHandlerTexture.GetContent(request);            
        }
    }
}
