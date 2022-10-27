#include <iostream>
#include "../h/Dado.h"

using namespace std;
using namespace Produto_Namespace;

int main()
{
    cout<<"Olá Mundo!\n";
    int i = 0;
    for(i = 0;i<10;i++)
        cout<<i<<endl;
    
    Produto *p1 = new Produto(1,"Sei lá");
    delete(p1);

    cout<< "OK.... FIM DO PROGRAMA!";

    return 0;
}