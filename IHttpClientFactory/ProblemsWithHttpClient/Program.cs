// time_wait --> means that the connection has beeen closed on one side (ours) but we are still waiting to see
// if any additional packets comes


// windows will hold a connection in this state (time_wait) for 240 seconds

// there is a limit to how quickly windows open new sockets so if we exhaust the connection pool then we will see error
//like this ->  Unable to connect to the remote server
//System.Net.Sockets.SocketException: Only one usage of each socket address (protocol/network address/port) is normally permitted.





Console.WriteLine("Start Connection");
for (int i = 0; i < 10; i++)
{
    using (var client = new HttpClient())
    {
        var result = await client.GetAsync("https://google.com");
        Console.WriteLine(result.StatusCode);
    }
}
Console.WriteLine("Connection ended!");


//=======================================================================================================================================

//HttpClient does not respect dns changes (DNS changes are NOT honoured)

// httpclient (through HttpClientHandler) hogs the connections until socket is closed 
// HttpClientHandler creates a connection group and does not close the connections in the group until getting disposed , this means the dns 
// check never  happens as long as a connection is open



//niave solution 

//var client = new HttpClient();
//client.DefaultRequestHeaders.ConnectionClose = true;


//good solution

//var sp = ServicePointManager.FindServicePoint(new Uri("http://foo.bar/baz/123?a=ab"));
//sp.ConnectionLeaseTimeout = 60 * 1000; // 1 minute


// but wonderful solution is to use HttpClientFactory





