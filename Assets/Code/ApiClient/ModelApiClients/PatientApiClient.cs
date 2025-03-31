using System;
using System.Threading.Tasks;
using UnityEngine;

public class PatientApiClient : MonoBehaviour
{
    public WebClient webClient;
    private const string Route = "/api/patients";

    public async Awaitable<IWebRequestReponse> GetAll()
    {
        return await webClient.SendGetRequest(Route);
    }

    public async Awaitable<IWebRequestReponse> Create(Patient patient)
    {
        string data = JsonUtility.ToJson(patient);
        return await webClient.SendPostRequest(Route, data);
    }

    public async Awaitable<IWebRequestReponse> Delete(string id)
    {
        string route = $"{Route}/{id}";
        return await webClient.SendDeleteRequest(route);
    }
}
