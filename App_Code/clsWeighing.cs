using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Collections;

/// <summary>
/// Summary description for clsWeighing
/// </summary>
public class clsWeighing
{
    
private TcpClient Client = new TcpClient();
public bool GlbSocket_Status;
public IPHostEntry DeviceInfo;
public IPAddress DeviceAddress;
public Socket GtcpSocket;
public string strDeviceIP;
public IPEndPoint serverEndPoint;
public IPAddress IPAddressServer;
public Int32 intBytesToFetch = 1;
private Thread trd;





public bool IsConnected
{
    get
    {
       
        if ((GtcpSocket == null))
        {
            return false;
         
        }
        if (!GtcpSocket.Connected)
        {
            return false;
    
        }
        try
        {
            return !GtcpSocket.Poll(10, SelectMode.SelectError);
        }
        catch (System.Exception ex)
        {
            return false;
          
        }
     
    }
}


public Int16 InitializeTCPIPClient(string strIP, string Port)
{
    try
    {
        GlbSocket_Status = true;
        //DeviceInfo = Dns.GetHostEntry(Dns.GetHostName)
        //DeviceAddress = DeviceInfo.AddressList(0)
        //strDeviceIP = DeviceAddress.ToString
        GtcpSocket = null;
        IPAddressServer = IPAddress.Parse(strIP);
        serverEndPoint = new IPEndPoint(IPAddressServer,Convert.ToInt32(Port));
        GtcpSocket = new Socket(serverEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        GtcpSocket.SendTimeout = 3000;
        GtcpSocket.ReceiveTimeout = 3000;
        GtcpSocket.Connect(serverEndPoint);

        if (GtcpSocket.Connected)
        {
            return 0;
        }
        else
        {
            return 1;
        }

    }
    catch (Exception ex)
    {
        return 1;
    }
}


public void SendDataToServer(string strData)
{
    Byte[] dataBuffer = System.Text.Encoding.ASCII.GetBytes("");
    dataBuffer = System.Text.Encoding.ASCII.GetBytes(strData);
    Thread.Sleep(500);
    GtcpSocket.Send(dataBuffer);
}

public void TerminateTCPClient()
{
    try
    {
        GtcpSocket.Shutdown(SocketShutdown.Both);
        GtcpSocket.Close();
        GtcpSocket = null;
    }
    catch (Exception ex)
    {
    }
}

public string ReceiveDataFromServer()
{
    ArrayList ReturnValue = new ArrayList();
    string strVal = string.Empty;
    try
    {
        if (IsConnected == true)
        {
            byte[] dataBuffer = System.Text.Encoding.ASCII.GetBytes("");
            Int32 NoOfBytes = default(Int32);
            int i = 0;
            dataBuffer = new byte[intBytesToFetch + 1];
            GtcpSocket.ReceiveTimeout = 1200;

            for (i = 0; i <= 10; i++)
            {
                if (!GtcpSocket.Connected)
                {
                    i = 1;
                    //              
                    break; // TODO: might not be correct. Was : Exit For
                }

                NoOfBytes = GtcpSocket.Receive(dataBuffer);
                strVal = strVal + Convert.ToString(System.Text.Encoding.ASCII.GetString(dataBuffer, 0, NoOfBytes));
            }

            TerminateTCPClient();
            return strVal;

        }
        else
        {
            TerminateTCPClient();
        }

    }
    catch (Exception ex)
    {
        TerminateTCPClient();
        return strVal;
    }
    return strVal;
}

public void Send_Data_To_Weighing_Scale(string strWeigh_IP, string strWeigh_Port, string strLine1, string strLine2, string strLine3, string strLine4)
{
    InitializeTCPIPClient(strWeigh_IP, strWeigh_Port);
    SendDataToServer(strLine1);
    SendDataToServer(strLine2);
    SendDataToServer(strLine3);
    SendDataToServer(strLine4);
    Thread.Sleep(100);
    ReceiveDataFromServer();
}

	
}


	
