using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

namespace MyHTTP
{
    /// <summary>
    /// Response.
    /// </summary>
    public class Response
    {
        // Status code of respond message
        private long status;
        // Body of respond message
        private string body;
        private byte[] rawBody;

        public Response(long _status, string _body, byte[] _rawBody)
        {
            this.status = _status;
            this.body = _body;
            this.rawBody = _rawBody;
        }

        /// <summary>
        /// To this instance.
        /// </summary>
        /// <returns>The to.</returns>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public T To<T>()
        {
            return JsonUtility.FromJson<T>(body);
        }

        // Get status
        public long Status()
        {
            return status;
        }

        // Get body
        public string Body()
        {
            return body;
        }

        public byte[] RawBody()
        {
            return rawBody;
        }

        // Check if the request submited successfully.
        public bool IsOK()
        {
            return status >= 200 && status < 300;
        }

        public string ToString()
        {
            return "status: " + status.ToString() + " - response: " + body.ToString();
        }

        // Get respond from a request.
        public static Response From(UnityWebRequest _www)
        {
            return new Response(_www.responseCode, _www.downloadHandler.text, _www.downloadHandler.data);
        }
    }
}