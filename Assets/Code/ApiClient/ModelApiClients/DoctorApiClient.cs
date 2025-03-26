using System;
using System.Threading.Tasks;
using UnityEngine;

public class DoctorApiClient : MonoBehaviour
{
    public WebClient webClient;
    private const string Route = "/api/doctors";

    public async Awaitable<IWebRequestReponse> GetAll()
    {
        return await webClient.SendGetRequest(Route);
    }

    public async Awaitable<IWebRequestReponse> Create(Doctor doctor)
    {
        string data = JsonUtility.ToJson(doctor);
        return await webClient.SendPostRequest(Route, data);
    }

    public async Awaitable<IWebRequestReponse> Delete(Guid id)
    {
        string route = $"{Route}/{id}";
        return await webClient.SendDeleteRequest(route);
    }
}
