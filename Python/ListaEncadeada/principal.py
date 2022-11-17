# -*- coding: utf-8 -*-
'''Esta é o módulo que vai testar a lista encadeada'''

from pickle import TRUE
from elemento import Produto
from estrutura import Elo
from lista import Lista

minhaLista = Lista()

def DefinirEstoque():
    print('\n###########Definindo Estoque para os Produtos############\n')
    estoque = input('Informe o número do estoque de todos os produtos: ')
    Produto.SetEstoque(estoque)

def CadastrarProduto():
    print('\n###########Cadastrando Produto############\n')
    nomeElo = input('Informe o nome do produto: ')
    codigoProduto = input('Informe o código do produto: ')
    novoElo = Elo(Produto(codigoProduto,nomeElo))
    
    if(minhaLista.AdicionaEloNaLista(novoElo)):
        print('Elo adicionado com sucesso\n')
    else:
        print('Elo nao adicionado\n')
    
    print(f'Produto Cadastrado! Existem {minhaLista.Contagem} produtos na lista')
    
def ProcurarProduto():
    print('\n###########Procurando Produto############\n')
    try:
        ind = input('Informe o índice do elo: ')
        qElo = minhaLista.RetornaEloComIndice(int(ind))
        qElo.GetProdutoDoElo().GetDescricao()
        print(f'Estoque: {qElo.GetProdutoDoElo().GetEstoque()} Número de Série: {qElo.GetProdutoDoElo().GetnSerie()} Descricrição: {qElo.GetProdutoDoElo().GetDescricao()}')
    except:
        print('Indice Inválido...')
    

def ApagarProduto():    
    print('\n###########Apagar Produto############\n')
    opt = input('Informe o índice do elo que deseja remover: ')
    ret = minhaLista.RemoveEloDaLista(int(opt))
    if ret == True:
        print('\nElo removido!')
    else:
        print('\nElo não removido...')

def ListarProdutos():
    print('\n###########Listando Produtos############\n')    
    if(minhaLista.Contagem > 0):
        qElo = minhaLista.Primeiro
        while qElo != None:
            try:                
                print(f'Estoque: {qElo.GetProdutoDoElo().GetEstoque()} Número de Série: {qElo.GetProdutoDoElo().GetnSerie()} Descricrição: {qElo.GetProdutoDoElo().GetDescricao()}')
                qElo = qElo.GetEloPosteriorDoElo()
            except:
                print('Indice Inválido...')
    else:
        print('Não existem elos na lista...')

if __name__ == '__main__':
        
    opt = 0;  
    eloInicial = Elo()

    while TRUE:
        print('\n################ Menu ##################')
        print('\n( 1 ) Definir o  número do Estoque')
        print('( 2 ) Cadastrar um produto ')
        print('( 3 ) Procurar um produto')
        print('( 4 ) Apagar um produto')
        print('( 5 ) Listar todos os produtos')
        print('( 6 ) Sair do programa')

        try:
            opt = int(input('Escolha uma das opções acima: '))
    
            if(opt == 1):
                DefinirEstoque()                
                
            elif(opt == 2):
                CadastrarProduto()                

            elif(opt == 3):
                ProcurarProduto()
                
            elif(opt == 4):
                ApagarProduto()                

            elif(opt == 5):
                ListarProdutos()

            elif(opt == 6):
                print('Saindo...\n')
                break;
            else:
                print('Opção Inválida!')
        
        except:
            print('Digite um número... \n\n')
