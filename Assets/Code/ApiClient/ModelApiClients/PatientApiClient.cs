using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PatientApiClient : MonoBehaviour
{
    public WebClient webClient;
    private const string Route = "/api/patients";

    public async Awaitable<IWebRequestReponse> GetAll()
    {
        IWebRequestReponse webRequestResponse = await webClient.SendGetRequest(Route);
        return ParsePatientResponse(webRequestResponse);
    }

    public async Awaitable<IWebRequestReponse> CreatePatient(Patient patient)
    {
        string data = JsonUtility.ToJson(patient);
        return await webClient.SendPostRequest(Route, data);
    }
    public async Awaitable<IWebRequestReponse> UpdatePatient(Patient patient)
    {
        string data = JsonUtility.ToJson(patient);
        return await webClient.SendPutRequest(Route, data);
    }

    public async Awaitable<IWebRequestReponse> DeletePatient(string id)
    {
        string route = $"{Route}/{id}";
        return await webClient.SendDeleteRequest(route);
    }
    private IWebRequestReponse ParsePatientResponse(IWebRequestReponse webRequestResponse)
    {
        switch (webRequestResponse)
        {
            case WebRequestData<string> data:
                Debug.Log("Patient Response data raw: " + data.Data);
                List<Patient> patients = JsonHelper.ParseJsonArray<Patient>(data.Data);
                WebRequestData<List<Patient>> parsedPatientData = new WebRequestData<List<Patient>>(patients);
                return parsedPatientData;
            default:
                return webRequestResponse;
        }
    }
}
