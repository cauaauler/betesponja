
import React, { useState } from 'react';
import { TrendingUp, Clock, Star } from 'lucide-react';
import MatchCard from '../components/MatchCard';
import BetModal from '../components/BetModal';
import { toast } from '@/components/ui/use-toast';

const Home = () => {
  const [betModal, setBetModal] = useState<{
    isOpen: boolean;
    bet: any;
  }>({ isOpen: false, bet: null });

  // Mock data for Bob Esponja competitions
  const liveMatches = [
    {
      id: '1',
      participant1: 'Bob Esponja',
      participant2: 'Patrick',
      date: 'Hoje',
      time: '16:30',
      competition: 'Corrida de Cavalo Marinho',
      odds: { participant1: 2.10, draw: 3.20, participant2: 3.40 },
      isLive: true
    },
    {
      id: '2',
      participant1: 'Lula Molusco',
      participant2: 'Sandy',
      date: 'Hoje',
      time: '18:00',
      competition: 'Quem Caça Mais Águas Vivas',
      odds: { participant1: 2.80, draw: 3.10, participant2: 2.50 },
      isLive: true
    }
  ];

  const upcomingMatches = [
    {
      id: '3',
      participant1: 'Sr. Siriguejo',
      participant2: 'Plankton',
      date: 'Amanhã',
      time: '16:00',
      competition: 'Quem Come Mais Hambúrgueres de Siri',
      odds: { participant1: 1.90, draw: 3.40, participant2: 4.20 }
    },
    {
      id: '4',
      participant1: 'Bob Esponja',
      participant2: 'Lula Molusco',
      date: 'Domingo',
      time: '20:00',
      competition: 'Corrida de Cavalo Marinho',
      odds: { participant1: 2.60, draw: 3.00, participant2: 2.90 }
    }
  ];

  const handleBet = (matchId: string, betType: string, odds: number) => {
    const match = [...liveMatches, ...upcomingMatches].find(m => m.id === matchId);
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
      {/* Hero Banner */}
      <div className="bg-neutral-white py-16 relative overflow-hidden border-b-4 border-accent-yellow">
        <div className="container mx-auto px-4 text-center relative z-10">
          <h1 className="text-4xl md:text-5xl font-bold mb-4 text-accent-yellow">
            Bem-vindos à Fenda do Biquíni!
          </h1>
          <p className="text-xl mb-8 text-primary-blue">
            As melhores apostas submarinas com os personagens mais queridos!
          </p>
          <div className="flex justify-center space-x-8 text-center">
            <div className="bg-sponge-yellow/20 p-4 rounded-lg">
              <div className="text-2xl font-bold text-sponge-yellow">50+</div>
              <div className="text-sm text-white/80">Competições por mês</div>
            </div>
            <div className="bg-patrick-pink/20 p-4 rounded-lg">
              <div className="text-2xl font-bold text-sponge-yellow">98%</div>
              <div className="text-sm text-white/80">Pagamentos rápidos</div>
            </div>
            <div className="bg-sponge-yellow/20 p-4 rounded-lg">
              <div className="text-2xl font-bold text-sponge-yellow">24/7</div>
              <div className="text-sm text-white/80">Suporte online</div>
            </div>
          </div>
        </div>
      </div>

      <div className="container mx-auto px-4 py-8">
        {/* Live Matches Section */}
        <section className="mb-12">
          <div className="flex items-center mb-6">
            <div className="w-3 h-3 bg-patrick-pink rounded-full mr-3 animate-pulse"></div>
            <h2 className="text-2xl font-bold text-ocean-blue">Competições ao Vivo</h2>
            <TrendingUp className="ml-2 text-patrick-pink" size={24} />
          </div>
          
          <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-6">
            {liveMatches.map((match) => (
              <MatchCard
                key={match.id}
                match={match}
                onBet={handleBet}
              />
            ))}
          </div>
        </section>

        {/* Upcoming Matches Section */}
        <section>
          <div className="flex items-center mb-6">
            <Clock className="mr-3 text-ocean-blue" size={24} />
            <h2 className="text-2xl font-bold text-ocean-blue">Próximas Competições</h2>
            <Star className="ml-2 text-sponge-yellow" size={24} />
          </div>
          
          <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-6">
            {upcomingMatches.map((match) => (
              <MatchCard
                key={match.id}
                match={match}
                onBet={handleBet}
              />
            ))}
          </div>
        </section>

        {/* Quick Stats */}
        <section className="mt-12 bg-white rounded-lg p-8 shadow-md border-4 border-sponge-yellow">
          <h3 className="text-xl font-bold mb-6 text-center text-ocean-blue">Estatísticas da Fenda do Biquíni</h3>
          <div className="grid md:grid-cols-4 gap-6 text-center">
            <div>
              <div className="text-3xl font-bold text-ocean-blue">8</div>
              <div className="text-gray-600">Competições hoje</div>
            </div>
            <div>
              <div className="text-3xl font-bold text-green-600">425</div>
              <div className="text-gray-600">Apostas ativas</div>
            </div>
            <div>
              <div className="text-3xl font-bold text-patrick-pink">R$ 15.8k</div>
              <div className="text-gray-600">Total em apostas</div>
            </div>
            <div>
              <div className="text-3xl font-bold text-brown-pants">4.2x</div>
              <div className="text-gray-600">Maior odd do dia</div>
            </div>
          </div>
        </section>
      </div>

      <BetModal
        isOpen={betModal.isOpen}
        onClose={() => setBetModal({ isOpen: false, bet: null })}
        bet={betModal.bet}
        onConfirmBet={handleConfirmBet}
      />
    </div>
  );
};

export default Home;
