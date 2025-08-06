
import React, { useState } from 'react';
import { Filter, Search } from 'lucide-react';
import MatchCard from '../components/MatchCard';
import BetModal from '../components/BetModal';
import { toast } from '@/components/ui/use-toast';

const Matches = () => {
  const [selectedCompetition, setSelectedCompetition] = useState('all');
  const [searchTerm, setSearchTerm] = useState('');
  const [betModal, setBetModal] = useState<{
    isOpen: boolean;
    bet: any;
  }>({ isOpen: false, bet: null });

  const competitions = [
    { id: 'all', name: 'Todas as Competições' },
    { id: 'seahorse-race', name: 'Corrida de Cavalo Marinho' },
    { id: 'jellyfish-catching', name: 'Caça às Águas Vivas' },
    { id: 'krabby-patty-eating', name: 'Hambúrguer de Siri' },
  ];

  const allMatches = [
    {
      id: '1',
      participant1: 'Bob Esponja',
      participant2: 'Patrick',
      date: '01/07',
      time: '16:30',
      competition: 'Corrida de Cavalo Marinho',
      competitionId: 'seahorse-race',
      odds: { participant1: 2.10, draw: 3.20, participant2: 3.40 },
      isLive: true
    },
    {
      id: '2',
      participant1: 'Lula Molusco',
      participant2: 'Sandy',
      date: '01/07',
      time: '18:00',
      competition: 'Quem Caça Mais Águas Vivas',
      competitionId: 'jellyfish-catching',
      odds: { participant1: 2.80, draw: 3.10, participant2: 2.50 }
    },
    {
      id: '3',
      participant1: 'Sr. Siriguejo',
      participant2: 'Plankton',
      date: '02/07',
      time: '16:00',
      competition: 'Quem Come Mais Hambúrgueres de Siri',
      competitionId: 'krabby-patty-eating',
      odds: { participant1: 1.90, draw: 3.40, participant2: 4.20 }
    },
    {
      id: '4',
      participant1: 'Bob Esponja',
      participant2: 'Lula Molusco',
      date: '03/07',
      time: '20:00',
      competition: 'Corrida de Cavalo Marinho',
      competitionId: 'seahorse-race',
      odds: { participant1: 2.60, draw: 3.00, participant2: 2.90 }
    },
    {
      id: '5',
      participant1: 'Sandy',
      participant2: 'Patrick',
      date: '03/07',
      time: '14:30',
      competition: 'Quem Caça Mais Águas Vivas',
      competitionId: 'jellyfish-catching',
      odds: { participant1: 2.40, draw: 3.60, participant2: 2.80 }
    },
    {
      id: '6',
      participant1: 'Gary',
      participant2: 'Pérola',
      date: '04/07',
      time: '16:00',
      competition: 'Corrida de Cavalo Marinho',
      competitionId: 'seahorse-race',
      odds: { participant1: 2.20, draw: 3.40, participant2: 3.10 }
    }
  ];

  const filteredMatches = allMatches.filter(match => {
    const matchesCompetition = selectedCompetition === 'all' || match.competitionId === selectedCompetition;
    const matchesSearch = searchTerm === '' || 
      match.participant1.toLowerCase().includes(searchTerm.toLowerCase()) ||
      match.participant2.toLowerCase().includes(searchTerm.toLowerCase()) ||
      match.competition.toLowerCase().includes(searchTerm.toLowerCase());
    
    return matchesCompetition && matchesSearch;
  });

  const handleBet = (matchId: string, betType: string, odds: number) => {
    const match = allMatches.find(m => m.id === matchId);
    if (match) {
      setBetModal({
        isOpen: true,
        bet: {
          matchId,
          participant1: match.participant1,
          participant2: match.participant2,
          betType,
          odds
        }
      });
    }
  };

  const handleConfirmBet = (amount: number) => {
    toast({
      title: "Aposta confirmada!",
      description: `Sua aposta de R$ ${amount.toFixed(2)} foi registrada com sucesso na Fenda do Biquíni!`,
    });
  };

  return (
    <div className="min-h-screen bg-gradient-to-b from-cyan-100 to-blue-200 pt-16">
      <div className="container mx-auto px-4 py-8">
        <h1 className="text-3xl font-bold text-ocean-blue mb-8">Todas as Competições da Fenda do Biquíni</h1>
        
        {/* Filters */}
        <div className="bg-white rounded-lg p-6 shadow-md mb-8 border-4 border-sponge-yellow">
          <div className="flex flex-col md:flex-row gap-4">
            <div className="flex-1">
              <label className="block text-sm font-medium text-ocean-blue mb-2">
                <Filter size={16} className="inline mr-1" />
                Competição
              </label>
              <select
                value={selectedCompetition}
                onChange={(e) => setSelectedCompetition(e.target.value)}
                className="w-full p-3 border-2 border-sponge-yellow rounded-lg focus:ring-2 focus:ring-ocean-blue focus:border-ocean-blue"
              >
                {competitions.map(competition => (
                  <option key={competition.id} value={competition.id}>
                    {competition.name}
                  </option>
                ))}
              </select>
            </div>
            
            <div className="flex-1">
              <label className="block text-sm font-medium text-ocean-blue mb-2">
                <Search size={16} className="inline mr-1" />
                Buscar personagens
              </label>
              <input
                type="text"
                placeholder="Digite o nome de um personagem..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                className="w-full p-3 border-2 border-sponge-yellow rounded-lg focus:ring-2 focus:ring-ocean-blue focus:border-ocean-blue"
              />
            </div>
          </div>
        </div>

        {/* Results Count */}
        <div className="mb-6">
          <p className="text-ocean-blue font-medium">
            Mostrando {filteredMatches.length} competição{filteredMatches.length !== 1 ? 'ões' : ''}
            {selectedCompetition !== 'all' && 
              ` em ${competitions.find(c => c.id === selectedCompetition)?.name}`
            }
          </p>
        </div>

        {/* Matches Grid */}
        {filteredMatches.length > 0 ? (
          <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-6">
            {filteredMatches.map((match) => (
              <MatchCard
                key={match.id}
                match={match}
                onBet={handleBet}
              />
            ))}
          </div>
        ) : (
          <div className="text-center py-12 bg-white rounded-lg border-4 border-sponge-yellow">
            <div className="text-gray-400 text-lg mb-2">Nenhuma competição encontrada</div>
            <p className="text-gray-600">Tente alterar os filtros de busca</p>
          </div>
        )}

        <BetModal
          isOpen={betModal.isOpen}
          onClose={() => setBetModal({ isOpen: false, bet: null })}
          bet={betModal.bet}
          onConfirmBet={handleConfirmBet}
        />
      </div>
    </div>
  );
};

export default Matches;
