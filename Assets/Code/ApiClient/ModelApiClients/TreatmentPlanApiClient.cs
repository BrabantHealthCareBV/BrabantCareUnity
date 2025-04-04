using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TreatmentPlanApiClient : MonoBehaviour
{
    public WebClient webClient;
    private const string Route = "/api/treatmentplans";

    public async Awaitable<IWebRequestReponse> GetAll()
    {
        IWebRequestReponse webRequestResponse = await webClient.SendGetRequest(Route); ;
        return ParseTreatmentPlanListResponse(webRequestResponse);
    }
    private IWebRequestReponse ParseTreatmentPlanListResponse(IWebRequestReponse webRequestResponse)
    {
        switch (webRequestResponse)
        {
            case WebRequestData<string> data:
                Debug.Log("Treatmentplan Response data raw: " + data.Data);
                List<TreatmentPlan> treatmentPlans = JsonHelper.ParseJsonArray<TreatmentPlan>(data.Data);
                WebRequestData<List<TreatmentPlan>> parsedTreatmentPlanData = new WebRequestData<List<TreatmentPlan>>(treatmentPlans);
                return parsedTreatmentPlanData;
            default:
                return webRequestResponse;
        }
    }
}
