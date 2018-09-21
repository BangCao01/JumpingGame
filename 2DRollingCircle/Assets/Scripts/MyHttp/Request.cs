using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;

namespace MyHTTP
{
    /// <summary>
    /// Request.
    /// </summary>
    public class Request
    {
        // API
        private string url;
        // Method of an http request, POST, PUT, GET..
        private string method;
        // Header of message
        private Dictionary<string, string> headers;
        // Body of message
        private RequestBody body;
        // Time out
        private int timeout;

        public Request(string url)
        {
            this.method = "GET";
            this.url = url;
            this.body = null;
            this.timeout = 0;
            this.headers = new Dictionary<string, string>();
        }

        /// <summary>
        /// Make a request by specified _url.
        /// </summary>
        /// <returns>The URL.</returns>
        /// <param name="_url">URL.</param>
        public Request Url(string _url)
        {
            this.url = _url;
            return this;
        }

        /// <summary>
        /// Make a request by given _method and _body.
        /// </summary>
        /// <returns>The method.</returns>
        /// <param name="_method">Method.</param>
        /// <param name="_body">Body.</param>
        public Request Method(string _method, RequestBody _body)
        {
            if (_method == null)
            {
                throw new NullReferenceException("method cannot be null");
            }
            else if (_method.Length == 0)
            {
                throw new InvalidOperationException("method cannot be empty");
            }

            this.method = _method;
            this.body = _body;
            return this;
        }

        /// <summary>
        /// Adds the header to request.
        /// </summary>
        /// <returns>The header.</returns>
        /// <param name="_name">Name.</param>
        /// <param name="_value">Value.</param>
        public Request AddHeader(string _name, string _value)
        {
            this.headers.Add(_name, _value);
            return this;
        }

        /// <summary>
        /// Removes the header.
        /// </summary>
        /// <returns>The header.</returns>
        /// <param name="_name">Name.</param>
        public Request RemoveHeader(string _name)
        {
            this.headers.Remove(_name);
            return this;
        }

        /// <summary>
        /// Timeout the specified _timeout.
        /// </summary>
        /// <returns>The timeout.</returns>
        /// <param name="_timeout">Timeout.</param>
        public Request Timeout(int _timeout)
        {
            this.timeout = _timeout;
            return this;
        }

        /// <summary>
        /// Make a GET request.
        /// </summary>
        /// <returns>The get.</returns>
        public Request Get()
        {
            Method(UnityWebRequest.kHttpVerbGET, null);
            return this;
        }

        /// <summary>
        /// Make a POST request with the specified _body.
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="_body">Body.</param>
        public Request Post(RequestBody _body)
        {
            Method(UnityWebRequest.kHttpVerbPOST, _body);
            return this;
        }

        /// <summary>
        /// Make a PUT request with the specified _body.
        /// </summary>
        /// <returns>The put.</returns>
        /// <param name="_body">Body.</param>
        public Request Put(RequestBody _body)
        {
            Method(UnityWebRequest.kHttpVerbPUT, _body);
            return this;
        }

        // Delete
        public Request Delete()
        {
            Method(UnityWebRequest.kHttpVerbDELETE, null);
            return this;
        }

        // Get url
        public string Url()
        {
            return url;
        }

        // Get method
        public string Method()
        {
            return method;
        }

        // Get request body
        public RequestBody Body()
        {
            return body;
        }

        // Get Header
        public Dictionary<string, string> Headers()
        {
            return headers;
        }

        // Get Timeout.
        public int Timeout()
        {
            return timeout;
        }
    }
}