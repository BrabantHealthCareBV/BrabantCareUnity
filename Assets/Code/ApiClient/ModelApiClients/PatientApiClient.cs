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
        if (patient.doctorID == "")
            patient.doctorID = Convert.ToString(Guid.Empty);
        string data = JsonUtility.ToJson(patient);
        return await webClient.SendPostRequest(Route, data);
    }
    public async Awaitable<IWebRequestReponse> UpdatePatient(Patient patient)
    {
        if (patient.doctorID == "")
            patient.doctorID = Convert.ToString(Guid.Empty);
        string data = JsonUtility.ToJson(patient);
        string route = $"{Route}/{patient.id}";
        return await webClient.SendPutRequest(route, data);
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

    public async Task<IWebRequestReponse> GetById(string id)
    {
        string route = $"{Route}/{id}";
        IWebRequestReponse webRequestResponse = await webClient.SendGetRequest(route);

        switch (webRequestResponse)
        {
            case WebRequestData<string> data:
                Debug.Log("Patient Response data raw: " + data.Data);

                try
                {
                    // Deserialize JSON as a single Patient object
                    Patient patient = JsonUtility.FromJson<Patient>(data.Data);
                    return new WebRequestData<Patient>(patient);
                }
                catch (Exception ex)
                {
                    Debug.LogError("Failed to parse patient response: " + ex.Message);
                    return new WebRequestError("Invalid patient data format");
                }

            default:
                return webRequestResponse;
        }
    }

}
