using System;
using System.Threading.Tasks;
using UnityEngine;

public class GuardianApiClient : MonoBehaviour
{
    public WebClient webClient;
    private const string Route = "/api/guardians";

    public async Awaitable<IWebRequestReponse> GetAllGuardian()
    {
        return await webClient.SendGetRequest(Route);
    }

    public async Awaitable<IWebRequestReponse> CreateGuardian(Guardian guardian)
    {
        string data = JsonUtility.ToJson(guardian);
        return await webClient.SendPostRequest(Route, data);
    }
    public async Awaitable<IWebRequestReponse> UpdateGuardian(Guardian guardian)
    {
        string data = JsonUtility.ToJson(guardian);
        return await webClient.SendPutRequest(Route, data);
    }

    public async Awaitable<IWebRequestReponse> DeleteGuardian(Guid id)
    {
        string route = $"{Route}/{id}";
        return await webClient.SendDeleteRequest(route);
    }
}
