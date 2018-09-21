using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace MyHTTP
{
    /// <summary>
    /// Client.
    /// </summary>
    public class Client
    {
        // Handle respond
        private Response response;
        private string error;
        // Use Unity Web Request
        private UnityWebRequest www;

        public Client()
        {
            error = null;
            response = null;
            www = null;
        }

        /// <summary>
        /// Send the specified request.
        /// </summary>
        /// <returns>The send.</returns>
        /// <param name="_request">Request.</param>
        public IEnumerator Send(Request _request)
        {
            // Employing `using` will ensure that the UnityWebRequest is properly cleaned in case of uncaught exceptions
            using (www = new UnityWebRequest(_request.Url(), _request.Method()))
            {

                www.timeout = _request.Timeout();

                RequestBody body = _request.Body();
                if (body != null)
                {
                    UploadHandler uploader = new UploadHandlerRaw(body.Body());
                    uploader.contentType = body.ContentType();
                    www.uploadHandler = uploader;
                }

                Dictionary<string, string> headers = _request.Headers();
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        www.SetRequestHeader(header.Key, header.Value);
                    }
                }

                www.downloadHandler = new DownloadHandlerBuffer();

                yield return www.SendWebRequest();

                if (www.isHttpError)
                {
                    error = www.error;
                }
                else
                {
                    response = MyHTTP.Response.From(www);
                }
            }
        }

        /// <summary>
        /// Abort this instance.
        /// </summary>
        public void Abort()
        {
            www.Abort();
        }

        /// <summary>
        /// Check if a request is the successful or not.
        /// </summary>
        /// <returns><c>true</c>, if error is null, <c>false</c> otherwise.</returns>
        public bool IsSuccessful()
        {
            return error == null;
        }

        /// <summary>
        /// Get error message.
        /// </summary>
        /// <returns>The error.</returns>
        public string Error()
        {
            return error;
        }

        /// <summary>
        /// Get respond content.
        /// </summary>
        /// <returns>The response.</returns>
        public MyHTTP.Response Response()
        {
            return response;
        }
    }
}