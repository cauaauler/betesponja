
import React, { useState } from 'react';
import { Calendar, TrendingUp, TrendingDown, Clock, CheckCircle, XCircle } from 'lucide-react';

const MyBets = () => {
  const [filter, setFilter] = useState('all');

  const bets = [
    {
      id: '1',
      date: '30/06/2024',
      time: '14:30',
      homeTeam: 'Flamengo',
      awayTeam: 'Palmeiras',
      betType: 'Vitória da Casa',
      odds: 2.10,
      amount: 50,
      potentialReturn: 105,
      status: 'won' // won, lost, pending
    },
    {
      id: '2',
      date: '29/06/2024',
      time: '16:00',
      homeTeam: 'Santos',
      awayTeam: 'Corinthians',
      betType: 'Empate',
      odds: 3.20,
      amount: 25,
      potentialReturn: 80,
      status: 'lost'
    },
    {
      id: '3',
      date: '01/07/2024',
      time: '18:00',
      homeTeam: 'São Paulo',
      awayTeam: 'Grêmio',
      betType: 'Vitória Visitante',
      odds: 4.20,
      amount: 30,
      potentialReturn: 126,
      status: 'pending'
    },
    {
      id: '4',
      date: '28/06/2024',
      time: '20:00',
      homeTeam: 'Atlético-MG',
      awayTeam: 'Internacional',
      betType: 'Vitória da Casa',
      odds: 2.60,
      amount: 40,
      potentialReturn: 104,
      status: 'won'
    }
  ];

  const filteredBets = bets.filter(bet => {
    if (filter === 'all') return true;
    return bet.status === filter;
  });

  const totalBets = bets.length;
  const wonBets = bets.filter(bet => bet.status === 'won').length;
  const totalAmountBet = bets.reduce((sum, bet) => sum + bet.amount, 0);
  const totalReturns = bets
    .filter(bet => bet.status === 'won')
    .reduce((sum, bet) => sum + bet.potentialReturn, 0);
  const profit = totalReturns - totalAmountBet;

  const getStatusIcon = (status: string) => {
    switch (status) {
      case 'won': return <CheckCircle className="text-green-500" size={20} />;
      case 'lost': return <XCircle className="text-red-500" size={20} />;
      case 'pending': return <Clock className="text-yellow-500" size={20} />;
      default: return null;
    }
  };

  const getStatusText = (status: string) => {
    switch (status) {
      case 'won': return 'Ganha';
      case 'lost': return 'Perdida';
      case 'pending': return 'Pendente';
      default: return '';
    }
  };

  const getStatusColor = (status: string) => {
    switch (status) {
      case 'won': return 'text-green-600 bg-green-50';
      case 'lost': return 'text-red-600 bg-red-50';
      case 'pending': return 'text-yellow-600 bg-yellow-50';
      default: return '';
    }
  };

  return (
    <div className="min-h-screen bg-gray-50 pt-16">
      <div className="container mx-auto px-4 py-8">
        <h1 className="text-3xl font-bold text-gray-800 mb-8">Minhas Apostas</h1>
        
        {/* Statistics Cards */}
        <div className="grid md:grid-cols-4 gap-6 mb-8">
          <div className="bg-white p-6 rounded-lg shadow-md">
            <div className="flex items-center justify-between">
              <div>
                <p className="text-gray-600 text-sm">Total de Apostas</p>
                <p className="text-2xl font-bold text-gray-800">{totalBets}</p>
              </div>
              <Calendar className="text-[#0A3D62]" size={32} />
            </div>
          </div>
          
          <div className="bg-white p-6 rounded-lg shadow-md">
            <div className="flex items-center justify-between">
              <div>
                <p className="text-gray-600 text-sm">Taxa de Acerto</p>
                <p className="text-2xl font-bold text-gray-800">
                  {totalBets > 0 ? Math.round((wonBets / totalBets) * 100) : 0}%
                </p>
              </div>
              <TrendingUp className="text-green-500" size={32} />
            </div>
          </div>
          
          <div className="bg-white p-6 rounded-lg shadow-md">
            <div className="flex items-center justify-between">
              <div>
                <p className="text-gray-600 text-sm">Total Apostado</p>
                <p className="text-2xl font-bold text-gray-800">R$ {totalAmountBet.toFixed(2)}</p>
              </div>
              <TrendingDown className="text-orange-500" size={32} />
            </div>
          </div>
          
          <div className="bg-white p-6 rounded-lg shadow-md">
            <div className="flex items-center justify-between">
              <div>
                <p className="text-gray-600 text-sm">Lucro/Prejuízo</p>
                <p className={`text-2xl font-bold ${profit >= 0 ? 'text-green-600' : 'text-red-600'}`}>
                  R$ {profit.toFixed(2)}
                </p>
              </div>
              {profit >= 0 ? 
                <TrendingUp className="text-green-500" size={32} /> :
                <TrendingDown className="text-red-500" size={32} />
              }
            </div>
          </div>
        </div>

        {/* Filter */}
        <div className="bg-white rounded-lg p-6 shadow-md mb-8">
          <div className="flex flex-wrap gap-2">
            <button
              onClick={() => setFilter('all')}
              className={`px-4 py-2 rounded-lg transition-colors ${
                filter === 'all' 
                  ? 'bg-[#0A3D62] text-white' 
                  : 'bg-gray-100 text-gray-700 hover:bg-gray-200'
              }`}
            >
              Todas ({totalBets})
            </button>
            <button
              onClick={() => setFilter('won')}
              className={`px-4 py-2 rounded-lg transition-colors ${
                filter === 'won' 
                  ? 'bg-green-600 text-white' 
                  : 'bg-gray-100 text-gray-700 hover:bg-gray-200'
              }`}
            >
              Ganhas ({wonBets})
            </button>
            <button
              onClick={() => setFilter('lost')}
              className={`px-4 py-2 rounded-lg transition-colors ${
                filter === 'lost' 
                  ? 'bg-red-600 text-white' 
                  : 'bg-gray-100 text-gray-700 hover:bg-gray-200'
              }`}
            >
              Perdidas ({bets.filter(bet => bet.status === 'lost').length})
            </button>
            <button
              onClick={() => setFilter('pending')}
              className={`px-4 py-2 rounded-lg transition-colors ${
                filter === 'pending' 
                  ? 'bg-yellow-600 text-white' 
                  : 'bg-gray-100 text-gray-700 hover:bg-gray-200'
              }`}
            >
              Pendentes ({bets.filter(bet => bet.status === 'pending').length})
            </button>
          </div>
        </div>

        {/* Bets List */}
        <div className="space-y-4">
          {filteredBets.map(bet => (
            <div key={bet.id} className="bg-white rounded-lg shadow-md p-6">
              <div className="flex flex-col md:flex-row justify-between items-start md:items-center">
                <div className="flex-1 mb-4 md:mb-0">
                  <div className="flex items-center mb-2">
                    <h3 className="font-semibold text-lg mr-3">
                      {bet.homeTeam} vs {bet.awayTeam}
                    </h3>
                    <span className={`px-3 py-1 rounded-full text-xs font-medium ${getStatusColor(bet.status)}`}>
                      {getStatusIcon(bet.status)}
                      <span className="ml-1">{getStatusText(bet.status)}</span>
                    </span>
                  </div>
                  
                  <div className="text-sm text-gray-600 mb-2">
                    {bet.date} às {bet.time} • {bet.betType} (Odd: {bet.odds.toFixed(2)})
                  </div>
                  
                  <div className="flex items-center space-x-4 text-sm">
                    <span>Valor apostado: <strong>R$ {bet.amount.toFixed(2)}</strong></span>
                    <span>Retorno potencial: <strong>R$ {bet.potentialReturn.toFixed(2)}</strong></span>
                  </div>
                </div>
                
                <div className="text-right">
                  {bet.status === 'won' && (
                    <div className="text-green-600 font-bold">
                      + R$ {(bet.potentialReturn - bet.amount).toFixed(2)}
                    </div>
                  )}
                  {bet.status === 'lost' && (
                    <div className="text-red-600 font-bold">
                      - R$ {bet.amount.toFixed(2)}
                    </div>
                  )}
                </div>
              </div>
            </div>
          ))}
        </div>

        {filteredBets.length === 0 && (
          <div className="text-center py-12">
            <div className="text-gray-400 text-lg mb-2">Nenhuma aposta encontrada</div>
            <p className="text-gray-600">Você ainda não fez apostas nesta categoria</p>
          </div>
        )}
      </div>
    </div>
  );
};

export default MyBets;
