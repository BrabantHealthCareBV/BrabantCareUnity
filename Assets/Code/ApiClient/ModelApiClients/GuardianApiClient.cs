using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GuardianApiClient : MonoBehaviour
{
    public WebClient webClient;
    private const string Route = "/api/guardians";

    public async Awaitable<IWebRequestReponse> GetAll()
    {
        IWebRequestReponse webRequestResponse = await webClient.SendGetRequest(Route);
        return ParseGuardiansListResponse(webRequestResponse);
    }

    public async Awaitable<IWebRequestReponse> CreateGuardian(Guardian guardian)
    {
        string data = JsonUtility.ToJson(guardian);
        IWebRequestReponse webRequestResponse = await webClient.SendPostRequest(Route, data);
        return ParseGuardiansResponse(webRequestResponse);
    }
    public async Awaitable<IWebRequestReponse> UpdateGuardian(Guardian guardian)
    {
        string data = JsonUtility.ToJson(guardian);
        string route = $"{Route}/{guardian.id}";
        IWebRequestReponse webRequestResponse = await webClient.SendPutRequest(route, data);
        return ParseGuardiansResponse(webRequestResponse);
    }

    public async Awaitable<IWebRequestReponse> DeleteGuardian(Guid id)
    {
        string route = $"{Route}/{id}";
        IWebRequestReponse webRequestResponse = await webClient.SendDeleteRequest(route);
        return ParseGuardiansResponse(webRequestResponse);
    }

    private IWebRequestReponse ParseGuardiansListResponse(IWebRequestReponse webRequestResponse)
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
    private IWebRequestReponse ParseGuardiansResponse(IWebRequestReponse webRequestResponse)
    {
        switch (webRequestResponse)
        {
            case WebRequestData<string> data:
                Debug.Log("Guardian Response data raw: " + data.Data);
                Guardian guardians = JsonUtility.FromJson<Guardian>(data.Data);
                WebRequestData<Guardian> parsedGuardianData = new WebRequestData<Guardian>(guardians);
                return parsedGuardianData;
            default:
                return webRequestResponse;
        }
    }

    public async Task<IWebRequestReponse> GetById(string id)
    {
        string route = $"{Route}/{id}";
        IWebRequestReponse webRequestResponse = await webClient.SendGetRequest(route);

        switch (webRequestResponse)
        {
            case WebRequestData<string> data:
                Debug.Log("Guardian Response data raw: " + data.Data);

                try
                {
                    // Deserialize JSON as a single Guardian object
                    Guardian guardian = JsonUtility.FromJson<Guardian>(data.Data);
                    return new WebRequestData<Guardian>(guardian);
                }
                catch (Exception ex)
                {
                    Debug.LogError("Failed to parse guardian response: " + ex.Message);
                    return new WebRequestError("Invalid guardian data format");
                }

            default:
                return webRequestResponse;
        }
    }
}
