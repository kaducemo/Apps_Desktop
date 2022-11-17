# -*- coding: utf-8 -*-
'''Este módulo implementa uma classe com a qual se pode instanciar listas encadeadas'''

from estrutura import Elo

class Lista:
    '''
    Atributos: 
    :Contagem: Quantidade de itens na lista
    :Primeiro: Primeiro Elo da lista
    :Ultimo: Ultimo Elo da lista
    '''
    
    ##############################################################################################

    def __init__(self, _prm:Elo = None): 
        #Inicializa Membros
        self.Contagem = 0
        self.Primeiro = None
        self.Ultimo = None 
        #Adiciona novo membro na lista caso seja valido      
        self.AdicionaEloNaLista(_prm)

    ##############################################################################################

    def AdicionaEloNaLista(self, _elo:Elo)->bool:
        ret = False
        if self.Contagem == 0 and _elo != None:
            self.Primeiro = _elo
            self.Contagem = 1
            self.Ultimo = _elo
            _elo.SetIndiceDoElo(1)
            ret = True
        elif self.Contagem != 0 and _elo != None:
            _elo.SetIndiceDoElo(self.Ultimo.GetIndiceDoElo()+1)
            _elo.SetEloAnteriorDoElo(self.Ultimo)            
            self.Ultimo.SetEloPosteriorDoElo(_elo)
            self.Ultimo = _elo
            
            self.Contagem += 1            
            ret = True
        return ret
    
    ##############################################################################################

    def RemoveEloDaLista(self, _ind:int)->bool:
        eloRemovido = self.RetornaEloComIndice(_ind)
        if(eloRemovido != None): #O elo está na lista

            eloAux1 = eloRemovido.GetEloAnteriorDoElo()
            eloAux2 = eloRemovido.GetEloPosteriorDoElo()

            if(self.Contagem > 1): #Tem mais do que 1 elo na lista

                if eloRemovido != self.Primeiro: #Elo removido não é o 1o elo
                    eloAux1.EloPosterior = eloAux2  
                else: #O elo removido é o primeiro elo da lista
                    self.Primeiro = eloRemovido.GetEloPosteriorDoElo()                                        
                
                if(eloRemovido != self.Ultimo): #Elo removido não é o último elo
                    eloAux2.EloAnterior = eloAux1                    
                else: #O elo removidos é o  ultimo elo da lista
                    self.Ultimo = eloRemovido.GetEloAnteriorDoElo()
                
                eloTmp = self.Primeiro
                contTmp = 1
                while(eloTmp): #Reorganiza os índices
                    self.Contagem = contTmp
                    eloTmp.SetIndiceDoElo(contTmp)
                    contTmp += 1
                    eloTmp = eloTmp.GetEloPosteriorDoElo()
                                 
            else: #Este é o primeiro e único elo na lista...
                self.Primeiro = None
                self.Ultimo = None
                self.Contagem = 0

        else: #Elo não econtrado na lista
            return False
                
        return True
            
    ##############################################################################################

    def RetornaEloComIndice(self, _ind:int)->Elo:   
            
        if self.Contagem >= _ind and self.Contagem > 0:        
            cnt = 0    
            eloTmp = self.Primeiro

            while(True):
                if(eloTmp.GetIndiceDoElo() == _ind):
                    return eloTmp
                else:                
                    eloTmp = eloTmp.EloPosterior
                    cnt += 1       
                    if(cnt >= self.Contagem):
                        break
        return None        

    ##############################################################################################
