using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using VNCScreen;

public class ConnectVNC : MonoBehaviour {
    public GameObject screens;
    public InputField ipAdress;
    public InputField password;
    public bool ignoreAutoGenerateVNC;

    #region private members 	
    private TcpClient socketConnection;
    private Thread clientReceiveThread;
    #endregion
    /// <summary> 	
    /// Setup socket connection. 	
    /// </summary> 	
    public void ConnectToTcpServer()
    {
        if (ignoreAutoGenerateVNC)
        {
            this.createScreen(0);
        } else
        {
            try
            {
                socketConnection = new TcpClient(ipAdress.text, 19000);
                //socketConnection = new TcpClient("192.168.0.2", 19000);
                this.SendMessage();
                this.ListenForData();
            }
            catch (Exception e)
            {
                Debug.Log("On client connect exception " + e);
            }
        }
    }
    /// <summary> 	
    /// Runs in background clientReceiveThread; Listens for incomming data. 	
    /// </summary>     
    private void ListenForData()
    {
        try
        {
            Debug.Log(ipAdress.text);
            Byte[] bytes = new Byte[1024];
            bool byteReceived = true;
            while (byteReceived)
            {
                // Get a stream object for reading 				
                using (NetworkStream stream = socketConnection.GetStream())
                {
                    int length;
                    // Read incomming stream into byte arrary. 					
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var incommingData = new byte[length];
                        Array.Copy(bytes, 0, incommingData, 0, length);
                        // Convert byte array to string message. 						
                        string serverMessage = Encoding.ASCII.GetString(incommingData);
                        Debug.Log("server message received as: " + serverMessage);
                        this.createScreen(int.Parse(serverMessage));
                        byteReceived = false;
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    private void createScreen(int display)
    {
        GameObject newScreen = Instantiate(screens, Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.rotation);
        GameObject ChildGameObject1 = newScreen.transform.GetChild(0).gameObject;
        VNCScreen.VNCScreen yourObject = ChildGameObject1.GetComponent<VNCScreen.VNCScreen>();
        yourObject.password = password.text;
        yourObject.port = 5900;
        yourObject.host = ipAdress.text;
        yourObject.display = display;
        var holoMenu = GameObject.Find("HoloMenu");
        holoMenu.SetActive(false);
    }
    /// <summary> 	
    /// Send message to server using socket connection. 	
    /// </summary> 	
    private void SendMessage()
    {
        if (socketConnection == null)
        {
            return;
        }
        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = socketConnection.GetStream();
            if (stream.CanWrite)
            {
                string clientMessage = "open";
                // Convert string message to byte array.                 
                byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
                // Write byte array to socketConnection stream.                 
                stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                Debug.Log("Client sent his message - should be received by server");
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
    
}
