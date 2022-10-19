'''Esta é o módulo que vai testar a lista encadeada'''

from pickle import TRUE
from elemento import Produto
from estrutura import Elo

def DefinirEstoque():
    print('Definindo Estoque... ')
    estoque = input('Informe o número do estoque de todos os produtos: ')
    Produto.SetEstoque(estoque)

def CadastrarProduto():
    print('Cadastrando Produto... ')

def ProcurarProduto():
    print('Procurando Produto...')   

def ApagarProduto():
    print('Apagando Produto')

def ListarProdutos():
    print('Mostrando Produtos')

if __name__ == '__main__':
        
    opt = 0;  
    eloInicial = Elo()

    while TRUE:

        print('\n\n( 1 ) Definir o  número do Estoque')
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
                print('Escolheu 2\n')
            elif(opt == 3):
                print('Escolheu 3\n')
            elif(opt == 4):
                print('Escolheu 4\n')
            elif(opt == 5):
                print('Escolheu 5\n')
            elif(opt == 6):
                print('Saindo...\n')
                break;
            else:
                print('Opção Inválida!')
        
        except:
            print('Digite um número... \n\n')
