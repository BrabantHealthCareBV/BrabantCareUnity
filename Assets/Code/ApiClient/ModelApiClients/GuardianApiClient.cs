using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GuardianApiClient : MonoBehaviour
{
    public WebClient webClient;
    private const string Route = "/api/guardians";

    public async Awaitable<IWebRequestReponse> GetAllGuardian()
    {
        IWebRequestReponse webRequestResponse = await webClient.SendGetRequest(Route);
        return ParseGuardianResponse(webRequestResponse);
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

    private IWebRequestReponse ParseGuardianResponse(IWebRequestReponse webRequestResponse)
    {
        switch (webRequestResponse)
        {
            case WebRequestData<string> data:
                Debug.Log("Guardian Response data raw: " + data.Data);
                List<Guardian> guardians = JsonHelper.ParseJsonArray<Guardian>(data.Data);
                WebRequestData<List<Guardian>> parsedGuardianData = new WebRequestData<List<Guardian>>(guardians);
                return parsedGuardianData;
            default:
                return webRequestResponse;
        }
    }
}
