# -*- coding: utf-8 -*-
'''Este módulo implementa uma classe, a qual possui um elemento interno. É importante
notar que esta é uma classe auxiliar para fazer a lista encadeada'''

from elemento import Produto

class Elo:
    '''
        Atributos:
        : Produdo
        : EloAnterior
        : EloPosterior        
    '''
      
    def __init__(self, _prod:Produto = None, _eloA:"Elo" = None, _eloP:"Elo" = None, _ind:int = 0)->None:
        '''
        Construtor da Classe
        : param _prod: Produto apontado por esse elo da Lista encadeada
        : param _eloA: Elo Anterior da lista encadeada
        : param _eloP: Elo Posterior da Lista encadeada
        '''

        self.SetProdutoDoElo(_prod)
        self.SetEloAnteriorDoElo(_eloA)
        self.SetEloPosteriorDoElo(_eloP)
        self.SetIndiceDoElo(_ind)                 
        print('Elo construído!')

##############################################################################################

    def SetProdutoDoElo(self,_prod:Produto)->None:
        '''
        Seta o Produto apontado por este elo da Lista
        : param_prod: Produto apontado por esse elo da Lista encadeada       
        '''
        self.Produto = _prod

##############################################################################################

    def GetProdutoDoElo(self)->Produto:
        '''
        Retorna o produto apontado por esta classe
        : returns Produto
        '''
        return self.Produto
    
##############################################################################################
    
    def SetEloAnteriorDoElo(self, _eloA)->None:
        '''
        Seta o Elo anterior da lista encadeada
        : param _eloA: É o elo que queremos associar ao elo presente.                
        '''   
        self.EloAnterior = _eloA 

##############################################################################################

    def GetEloAnteriorDoElo(self) -> "Elo":
        '''
        Retorna o Elo anterior da lista encadeada
        : returns Elo
        '''
        return self.EloAnterior

##############################################################################################
   
    def SetEloPosteriorDoElo(self, _eloP)->None:
        '''
        Seta o elo posterior da lista encadeada
        : param _eloP - Novo valor do Elo Posterior da lista encadeada
        '''    
        self.EloPosterior = _eloP

##############################################################################################
    def GetEloPosteriorDoElo(self) -> "Elo":
        '''
        Retorna o Elo Posterior deste elo na lista encadeada
        : returns Elo
        '''
        return self.EloPosterior
        
##############################################################################################
def SetIndiceDoElo(self, ind:int)->None:
        '''
        Seta o índice deste ELO na lista encadeada
        '''
        
        self.Indice = ind
##############################################################################################
def GetIndiceDoElo(self) -> int:
    '''
    Retorna o índice do Elo dentro da lista encadeada
    '''    
    return self.Indice

