using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Gameplay
{
    [DefaultExecutionOrder(int.MinValue)]
    public class GameplayNetwork : MonoBehaviour
    {
        public static GameplayNetwork Instance;

        public UserData account;
        public Action onCreateNew;


        private void Awake()
        {
            Instance = this;
        }

        public void CreateAccount(string userName, Action onResponse)
        {
            const string url = "https://dummy.restapiexample.com/api/v1/create";
            var user = new UserData() { employee_name = userName };

            StartCoroutine(postRequest(url, JsonConvert.SerializeObject(user), response =>
            {
                if (response.result is UnityWebRequest.Result.ConnectionError)
                {
                    account.employee_name = $"{userName} (Client)";
                }
                else
                {
                    try
                    {
                        account = JsonConvert.DeserializeObject<CreateUserResponse>(response.downloadHandler.text).data;
                        account.employee_name = $"{account.employee_name} ({account.id})";
                    }
                    catch (Exception)
                    {
                        account.employee_name = $"{userName} (Client)";
                    }
                }

                onResponse?.Invoke();
                onCreateNew?.Invoke();
            }));
        }

        public void DeleteAccount()
        {
            var url = $"https://dummy.restapiexample.com/api/v1/employee/{account.id}";
            StartCoroutine(deleteRequest(url));
        }


        // IEnumerator getRequest(string uri, Action<UnityWebRequest> callback)
        // {
        //     UnityWebRequest uwr = UnityWebRequest.Get(uri);
        //     yield return uwr.SendWebRequest();
        //
        //     callback?.Invoke(uwr);
        //
        //     if (uwr.result is UnityWebRequest.Result.ConnectionError)
        //     {
        //         Debug.Log("Error While Sending: " + uwr.error);
        //     }
        //     else
        //     {
        //         Debug.Log("Received: " + uwr.downloadHandler.text);
        //     }
        // }

        IEnumerator postRequest(string url, string json, Action<UnityWebRequest> callback)
        {
            var uwr = new UnityWebRequest(url, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            uwr.uploadHandler = new UploadHandlerRaw(jsonToSend);
            uwr.downloadHandler = new DownloadHandlerBuffer();
            uwr.SetRequestHeader("Content-Type", "application/json");

            yield return uwr.SendWebRequest();
            callback?.Invoke(uwr);

            if (uwr.result is UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Error While Sending: " + uwr.error);
            }
            else
            {
                Debug.Log("Received: " + uwr.downloadHandler.text);
            }
        }

        IEnumerator deleteRequest(string url)
        {
            UnityWebRequest uwr = UnityWebRequest.Delete(url);
            yield return uwr.SendWebRequest();

            if (uwr.result is UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Error While Sending: " + uwr.error);
            }
            else
            {
                Debug.Log("Deleted");
            }
        }
    }

    [Serializable]
    public class UserData
    {
        public int id;
        public string employee_name;
    }

    [Serializable]
    public class CreateUserResponse
    {
        public UserData data;
    }
}