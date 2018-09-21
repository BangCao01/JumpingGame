using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using CI.HttpClient;
using MyHTTP;

/// <summary>
/// Http manager, manage RESTful API 
/// </summary>
public class HttpManager
{
    //Http client
    private HttpClient client;
    //Single instance.
    private static HttpManager instance = null;
    //Singleton
    public static HttpManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new HttpManager();

            }
            return instance;
        }
    }

    public HttpManager()
    {
        client = new HttpClient();
    }

    /// <summary>
    /// Sends the score to leader board.
    /// </summary>
    /// <param name="_username">Username.</param>
    /// <param name="_score">Score.</param>
    public void SendScoreToLeaderBoard(string _username, int _score)
    {
        string dataToSend = "{\"username\":" + _username + ",\"score\":" + _score;
        StringContent content = new StringContent(dataToSend);


        client.Post(new System.Uri(HConst.LeaderBoardApi), content, HttpCompletionOption.AllResponseContent, (respondData) =>
        {
            if ((int)respondData.StatusCode == HConst.HttpRequestOK)
            {
                Debug.Log("your score submit successfully !");
                //Stub for parsing leader board data here
            }
            else if ((int)respondData.StatusCode == HConst.HttpRequestNotFound)
            {
                Debug.Log("Username not found !");
            }
            else if ((int)respondData.StatusCode == HConst.HttpRequestInvalidUser)
            {
                Debug.Log("Invalid Username supplied !");
            }

        });
    }

    /// <summary>
    /// Sends the score to leader board with my http.
    /// </summary>
    /// <returns>The score to leader board with my http.</returns>
    /// <param name="_username">Username.</param>
    /// <param name="_score">Score.</param>
    public IEnumerator SendScoreToLeaderBoardWithMyHttp(string _username, int _score)
    {
        MPost post = new MPost(_username, _score);

        Request request = new Request(HConst.LeaderBoardApi)
            .AddHeader("Test-Header", "test")
            .Post(RequestBody.From<MPost>(post));

        Client http = new Client();
        yield return http.Send(request);
        ProcessResult(http);
    }

    /// <summary>
    /// Handle post data, using by MyHttp
    /// </summary>
    private class MPost
    {
        private string userName;
        private int userScore;

        public MPost(string _userName, int _userScore)
        {
            this.userName = _userName;
            this.userScore = _userScore;
        }
    }

    /// <summary>
    /// Processes the result, using by MyHttp
    /// </summary>
    /// <param name="_httpClient">Http client.</param>
    private void ProcessResult(Client _httpClient)
    {
        if (_httpClient.IsSuccessful())
        {
            Response resp = _httpClient.Response();
            if (resp.Status() == HConst.HttpRequestOK)
            {
                Debug.Log("your score submited successfully !");
            }
            else if (resp.Status() == HConst.HttpRequestNotFound)
            {
                Debug.LogError("Username not found !");
            }
            else if (resp.Status() == HConst.HttpRequestInvalidUser)
            {
                Debug.LogError("Invalid Username supplied !");
            }
        }
        else
        {
            Debug.LogError("your score submited not successfully !");
        }

    }

}