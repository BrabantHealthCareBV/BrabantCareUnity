using System;
using System.Threading.Tasks;
using UnityEngine;

public class TreatmentPlanCareMomentApiClient : MonoBehaviour
{
    public WebClient webClient;
    private const string Route = "/api/treatmentplan-caremoments";

    public async Awaitable<IWebRequestReponse> GetAll()
    {
        return await webClient.SendGetRequest(Route);
    }

    public async Awaitable<IWebRequestReponse> Create(TreatmentPlan_CareMoment treatmentPlanCareMoment)
    {
        string data = JsonUtility.ToJson(treatmentPlanCareMoment);
        return await webClient.SendPostRequest(Route, data);
    }

    public async Awaitable<IWebRequestReponse> Delete(Guid treatmentPlanId, Guid careMomentId)
    {
        string route = $"{Route}/{treatmentPlanId}/{careMomentId}";
        return await webClient.SendDeleteRequest(route);
    }
}
