using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class NetworkService{
    //JSON -- slightly different URL (remove &mode=xml) Chicago, Tonopah, Ogden
    private const string jsonApi = "https://api.weather.gov/gridpoints/TOP/95,44/forecast/hourly";//http://api.openweathermap.org/data/2.5/weather?q=KansasCity,us&APPID=<insert API key here>"; //Example: Cairo, Chicago
    
    private const string webImage = "http://upload.wikimedia.org/wikipedia/commons/c/c5/Moraine_Lake_17092005.jpg";

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