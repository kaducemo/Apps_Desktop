#ifndef DADO_H
#define DADO_H

#include <iostream>
#include <string>



using namespace std;

namespace Produto_Namespace
{
    class Produto
    {
    
    public:
        static int numeroEstoque;
        Produto();
        Produto(int nSerie, const string& str);
        ~Produto();        

    private:
        int numeroSerie;
        string descricao;

    };
}



#endif