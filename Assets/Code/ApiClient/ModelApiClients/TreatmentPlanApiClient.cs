using System;
using System.Threading.Tasks;
using UnityEngine;

public class TreatmentPlanApiClient : MonoBehaviour
{
    public WebClient webClient;
    private const string Route = "/api/treatment-plans";

    public async Awaitable<IWebRequestReponse> GetAll()
    {
        return await webClient.SendGetRequest(Route);
    }

    public async Awaitable<IWebRequestReponse> Create(TreatmentPlan treatmentPlan)
    {
        string data = JsonUtility.ToJson(treatmentPlan);
        return await webClient.SendPostRequest(Route, data);
    }

    public async Awaitable<IWebRequestReponse> Delete(Guid id)
    {
        string route = $"{Route}/{id}";
        return await webClient.SendDeleteRequest(route);
    }
}
