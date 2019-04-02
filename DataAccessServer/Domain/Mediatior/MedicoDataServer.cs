using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

public class MedicoDataServer{

    private IMedicoModel _MedicoModel;
    private Socket welcomeSocket;
    private EndPoint endPoint;

    public MedicoDataServer(IMedicoModel MedicoModel,IPAddress IP,int Port)
    {
        this._MedicoModel = MedicoModel;
        this.welcomeSocket =new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
        this.endPoint=new IPEndPoint(IP,Port);
    }

    public void run()
    {
        welcomeSocket.Bind(endPoint);
        welcomeSocket.Listen(1000);
        Console.ForegroundColor= ConsoleColor.Cyan;
        Console.WriteLine("Server started");
        Console.WriteLine("Waiting for connections");
        Console.ResetColor();

        while (true)
        {
            Socket socket = welcomeSocket.Accept();
            MedicoCommunicationHandler c = new MedicoCommunicationHandler(socket,_MedicoModel);
            Thread thread = new Thread(()=>c.run());
            thread.Start();
            
        }
    }
}
