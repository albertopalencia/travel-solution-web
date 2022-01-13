// ***********************************************************************
// Assembly         : Application
// Author           : alberto palencia
// Created          : 01-06-2022
//
// Last Modified By : alberto palencia
// Last Modified On : 01-11-2022
// ***********************************************************************
// <copyright file="Response.cs" company="Application">
//     Copyright (c) everis. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace TravelWeb.Client.Wrappers
{
    /// <summary>
    /// Class Response.
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public class Response<T>
        {
            public Response()
            {

            }
            public Response(T data, string message = null)
            {
                Succeeded = true;
                Message = message;
                Data = data;
            }

            public Response(string message)
            {
                Succeeded = false;
                Message = message;
            }

            public bool Succeeded { get; set; }
            public string Message { get; set; }
            public List<string> Errors { get; set; }
            public T Data { get; set; }
        }
}