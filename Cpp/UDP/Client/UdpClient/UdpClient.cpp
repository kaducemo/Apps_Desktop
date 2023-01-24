#include<iostream>
#include<ws2tcpip.h>
#include<string>

#pragma comment(lib, "ws2_32.lib")

using namespace std;

void main(int argc, char* argv[])
{
    if (argc < 2)
    {
        cout << "\n\nUse the following Sintax: UdpClient [Server Ip] [Server Port] [MessageWithoutSpaces]\n\n";
        return;
    }
    

    cout << "Initializing the UDP Client...\n";

    // 1) Initialize the Winsockets library    
    WSADATA wsData;

    WORD ver = MAKEWORD(2, 2);
    int wsOk = WSAStartup(ver, &wsData);

    if (wsOk != 0)
    {
        cerr << "Can´t Initialize Winsock..." << endl;
        cout << "Use the following Sintax: UdpClient [Server Ip] [Server Port] [MessageWithoutSpaces]" << endl;
        return;
    }

    // 2) Create a Valid Destiny Endpoint and a Socket
    sockaddr_in server;    
    server.sin_family = AF_INET;
    server.sin_port = htons(atoi(argv[2]));
    int serverLength = sizeof(server);
    //PCSTR ipStr = argv[1]; //Ip String    

    inet_pton(AF_INET, argv[1], &server.sin_addr);   //Local host
    SOCKET mySocket = socket(AF_INET, SOCK_DGRAM, 0);

    char myRxBuff[1024]; //Creates a Buffer to Receive an answer from the server
    ZeroMemory(myRxBuff, sizeof(myRxBuff));    
    
    string s(argv[3]); //Mensagem    

    // 3) Send the message to Server
    int nSent = sendto(mySocket, s.c_str(), s.length()+1, 0, (sockaddr*)&server, serverLength);
    if (nSent == SOCKET_ERROR)
    {
        cout << "Cannot send the message..." << endl;        
        cout << "Use the following Sintax: UdpClient [Server Ip] [Server Port] [MessageWithoutSpaces]" << endl;
        closesocket(mySocket);        
        WSACleanup();
        return;
    }        

    std::cout << "Message sent to server: "<< s <<endl;
    
    // 4) Receive the echoed message from Server
    int nReceived = recvfrom(mySocket, myRxBuff, nSent, 0, (sockaddr*)&server, &serverLength);

    if (nReceived == SOCKET_ERROR)
    {
        cout << "Cannot receive a message..." << endl;       
        cout << "Use the following Sintax: UdpClient [Server Ip] [Server Port] [MessageWithoutSpaces]" << endl;
        closesocket(mySocket);        
        WSACleanup();
        return;
    }

    cout << "Message Received: " << myRxBuff << endl;

    // 5) Close the socket
    closesocket(mySocket);

    // 6) Close the Winsockets library
    WSACleanup();

    return;
}