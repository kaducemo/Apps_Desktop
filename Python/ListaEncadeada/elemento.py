# -*- coding: utf-8 -*-
'''Este arquivo implementa o elemento que é na verdade a própria informação que se
se deseja guardar.'''

class Produto:
    '''
    Classe Produto: É a informação que queremos encadear
    : param estoque - Atributo GLOBAL que todos os produtos compartilham: int
    : param nSerie - Número de Série do produto: int
    : param descricao -  Descrição do produto: string
    '''
    # Classe Produto: É a informação que queremos encadear
    # Atributos

    __estoque = 0 #Este é um atributo que todos os objetos compartilham.

##############################################################################################

    # def __init__(self, nSerie = 1, descricao='' ):
    #     '''
    #     Construtor Padrão - Sem Parâmetros
    #     '''
    #     self.nSerie = nSerie
    #     self.descricao = descricao
    #     estoque = 0
    #     print("Classe Produto Criada, mas membros não inicializados!")

##############################################################################################    

    def __init__(self, nSerie:int=0, descricao:str=''):
        '''
        Constutor com Atributos Locais inicializados
        : param _nSerie - Inicializa o número de série do objeto : int
        : param _descricao - Incializa a Descrição do objeto :  string
        '''
        self.__nSerie = nSerie
        self.__descricao = descricao
        print("Classe Produto Criada com membros também inicializados!")

##############################################################################################

    @staticmethod
    def SetEstoque(_estoque:int):
        '''
        Configura o Atributo Global da classe
        : param _estoque - Parametro utilizado para configurar o número de estoque dos objetos : int
        '''
        Produto.__estoque  = _estoque

##############################################################################################

    @staticmethod
    def GetEstoque()->int:
        '''
        Retorna o número do estoque que é compartilhado por todos os objetos
        : return int
        '''
        return Produto.__estoque

##############################################################################################
    
    def SetnSerie(self,_nSerie:int):
        '''
        Esta função configura o número de série do produto
        : param _val
        ''' 
        self.__nSerie = _nSerie

##############################################################################################

    def GetnSerie(self)->int:
        '''
        Esta função retorna o número de serie de um produto
        '''
        return self.__nSerie

##############################################################################################

    def SetDescricao(self, _descricao:str):
        '''
        Esta função configura a descrição do produto
        : param _val
        ''' 
        self.__descricao = _descricao

##############################################################################################

    def GetDescricao(self)->str:
        '''
        Esta função retorna a descricão do produto
        '''
        return self.__descricao
    
##############################################################################################    
