﻿using System.Net.Http;

public class HttpHelper
{
    public static HttpClient GetHttpClient()
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Connection", "close");

        return client;
    }
}