using System;
using System.Threading.Tasks;
using UnityEngine;

public class CareMomentApiClient : MonoBehaviour
{
    public WebClient webClient;
    private const string Route = "/api/care-moments";

    public async Awaitable<IWebRequestReponse> GetAll()
    {
        return await webClient.SendGetRequest(Route);
    }

    public async Awaitable<IWebRequestReponse> Create(CareMoment careMoment)
    {
        string data = JsonUtility.ToJson(careMoment);
        return await webClient.SendPostRequest(Route, data);
    }

    public async Awaitable<IWebRequestReponse> Delete(Guid id)
    {
        string route = $"{Route}/{id}";
        return await webClient.SendDeleteRequest(route);
    }
}
