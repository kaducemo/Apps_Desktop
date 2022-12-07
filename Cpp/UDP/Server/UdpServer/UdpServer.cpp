// UdpServer.cpp : Este arquivo contém a função 'main'. A execução do programa começa e termina ali.
//

#include <iostream>
#include<ws2tcpip.h>

#pragma comment(lib, "ws2_32.lib")

using namespace std;

void main()
{
    std::cout << "Initializing the UDP Server!\n";

    // 1) Initialize the Winsockets library
    
    WSADATA wsData;

    WORD ver = MAKEWORD(2, 2);
    int wsOk = WSAStartup(ver, &wsData);

    if (wsOk != 0)
    {
        cerr << "Can´t Initialize Winsock..." << endl;
        return;
    }

    // 2) Create a Valid Socket for the server and bind it
    SOCKET in = socket(AF_INET, SOCK_DGRAM, 0);
    sockaddr_in serverHint;
    serverHint.sin_addr.S_un.S_addr = ADDR_ANY;
    serverHint.sin_family = AF_INET;
    serverHint.sin_port = htons(9760);

    if (bind(in, (sockaddr*)&serverHint, sizeof(serverHint)) == SOCKET_ERROR)
    {
        cout << "Cannot bind the socket..." << endl;
    }   

    // 3) Create a Valid Socket for the Cliente and wait for a conection
    sockaddr_in client; //Client info
    int clientLength = sizeof(client);
    ZeroMemory(&client, clientLength);
    

    char buf[1024]; //RX buffer
    


    // 4) Receive the clients messages and reply them
    while (true)
    {
        ZeroMemory(buf, 1024);
        int bytesIn = recvfrom(in, buf, 1024, 0, (sockaddr*)&client , &clientLength);

        if (bytesIn == SOCKET_ERROR)
        {
            cout << "Error receiving from client" << WSAGetLastError() << endl;
        }

        char clientIp[256];
        ZeroMemory(clientIp, 256);

        inet_ntop(AF_INET, &client.sin_addr, clientIp, 256);
        cout << "Message recev from " << clientIp << " : " << buf << endl;

    }



    // 5) Close the server
    closesocket(in);
    
    // 6) Close the Winsockets library
    WSACleanup();

    return;
}

// Executar programa: Ctrl + F5 ou Menu Depurar > Iniciar Sem Depuração
// Depurar programa: F5 ou menu Depurar > Iniciar Depuração
