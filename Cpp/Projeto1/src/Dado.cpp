#include "../h/Dado.h"

using namespace Produto_Namespace;

Produto::Produto()
{
    this->numeroSerie = 0;
    this->descricao = nullptr;
    cout << "Produto Construído!\r\n";
}

Produto::Produto(int nSerie, const string& str)
{
    this->numeroSerie = nSerie;
    this->descricao = str;
    cout << "Produto Construído!\r\n";
}

Produto::~Produto()
{
    cout << "Produto Destruído!\r\n";
}


