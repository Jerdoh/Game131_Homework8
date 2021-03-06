using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class NetworkService{
    //JSON -- slightly different URL (remove &mode=xml) Chicago, Tonopah, Ogden
    private const string jsonApi = "https://api.weather.gov/gridpoints/TOP/95,44/forecast";//"https://api.weather.gov/gridpoints/TOP/95,44/forecast/hourly";
    
    private const string webImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/14/Landscape_Arnisee-region.JPG/640px-Landscape_Arnisee-region.JPG";

    private IEnumerator CallAPI(string url, Action<string> callback) {
        using (UnityWebRequest request = UnityWebRequest.Get(url)) {
            yield return request.Send();

            if(request.isNetworkError) {
                Debug.LogError("network problem: " + request.error);
            }

            else if (request.responseCode != (long)System.Net.HttpStatusCode.OK) {
                Debug.LogError("response error: " + request.responseCode);
            }

            else {
                callback(request.downloadHandler.text);
            }
        }
    }
    
    //JSON
    public IEnumerator GetWeatherJSON(Action<string> callback){
        return CallAPI(jsonApi, callback);
    }

    public IEnumerator DownloadImage(Action<Texture2D> callback){
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(webImage);
        yield return request.Send();
        callback(DownloadHandlerTexture.GetContent(request));
    }
}