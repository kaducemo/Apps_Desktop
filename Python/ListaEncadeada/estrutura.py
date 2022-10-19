'''Este módulo implementa uma classe, a qual possui um elemento interno. É importante
notar que esta é uma classe auxiliar para fazer a lista encadeada'''

from elemento import Produto

class Elo:
    '''
        Atributos:
        : Produdo
        : EloAnterior
        : EloPosterior
        : Indice
    '''
    
    def __init__(self, _prod:Produto = None, _eloA:"Elo" = None, _eloP:"Elo" = None)->None:
        '''
        Construtor da Classe
        : param _prod: Produto apontado por esse elo da Lista encadeada
        : param _eloA: Elo Anterior da lista encadeada
        : param _eloP: Elo Posterior da Lista encadeada
        '''
        self.Produto = _prod
        self.EloAnterior = _eloA
        self.EloPosterior = _eloP
        
        # if(_prod != None):


        
        print('Elo construído com as inicializações dos atributos!')

##############################################################################################

    def SetProduto(self,_prod:Produto):
        '''
        Seta o Produto apontado por este elo da Lista
        : param_prod: Produto apontado por esse elo da Lista encadeada       
        '''
        self.Produto = _prod

##############################################################################################

    def GetProduto(self)->Produto:
        '''
        Retorna o produto apontado por esta classe
        : returns Produto
        '''
        return self.Produto
    
##############################################################################################
    
    def SetEloAnterior(self, _eloA):
        '''
        Seta o Elo anterior da lista encadeada
        : param _eloA: É o elo que queremos associar ao elo presente.                
        '''   
        self.EloAnterior = _eloA 

##############################################################################################

    def GetEloAnterior(self) -> "Elo":
        '''
        Retorna o Elo anterior da lista encadeada
        : returns Elo
        '''
        return self.EloAnterior

##############################################################################################
   
    def SetEloPosterior(self, _eloP):
        '''
        Seta o elo posterior da lista encadeada
        : param _eloP - Novo valor do Elo Posterior da lista encadeada
        '''    
        self.EloPosterior = _eloP

##############################################################################################
    def GetEloPosterior(self) -> "Elo":
        '''
        Retorna o Elo Posterior deste elo na lista encadeada
        : returns Elo
        '''
        return self.EloPosterior
        
##############################################################################################
def __SetIndice(self):
        '''
        Método privado. Seta o índice deste ELO na lista encadeada
        '''
        if (self.EloAnterior == None):
            self.Indice = 0
        else:
            indAnterior = self.EloAnterior.GetIndice()
        self.Indice = indAnterior+1
##############################################################################################
def GetIndice(self) -> int:
    '''
    Retorna o índice do Elo dentro da lista encadeada
    '''
    
    return self.Indice