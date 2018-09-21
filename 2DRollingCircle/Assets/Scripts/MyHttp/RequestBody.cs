using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;

namespace MyHTTP
{
    /// <summary>
    /// Request body.
    /// </summary>
    public class RequestBody
    {
        // Content Type
        private string contentType;
        // Body of message
        private byte[] body;

        // Contructor
        public RequestBody(string _contentType, byte[] _body)
        {
            this.contentType = _contentType;
            this.body = _body;
        }

        /// <summary>
        /// Get request body from the specified string.
        /// </summary>
        /// <returns>The from.</returns>
        /// <param name="_value">Value.</param>
        public static RequestBody From(string _value)
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(_value.ToCharArray());
            return new RequestBody("application/x-www-form-urlencoded", bodyRaw);
        }

        /// <summary>
        /// Get request body from the specified _value.
        /// </summary>
        /// <returns>The from.</returns>
        /// <param name="_value">Value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static RequestBody From<T>(T _value)
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(_value).ToCharArray());
            return new RequestBody("application/json", bodyRaw);
        }

        // Get content type
        public string ContentType()
        {
            return contentType;
        }

        // Get body
        public byte[] Body()
        {
            return body;
        }
    }

}