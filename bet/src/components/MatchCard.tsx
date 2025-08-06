
import React from 'react';
import { Clock, Star } from 'lucide-react';

interface MatchCardProps {
  match: {
    id: string;
    participant1: string;
    participant2: string;
    date: string;
    time: string;
    competition: string;
    odds: {
      participant1: number;
      draw: number;
      participant2: number;
    };
    isLive?: boolean;
  };
  onBet: (matchId: string, betType: string, odds: number) => void;
}

const MatchCard: React.FC<MatchCardProps> = ({ match, onBet }) => {
  return (
    <div className="bg-white rounded-lg shadow-md p-6 border-2 border-sponge-yellow hover:shadow-lg transition-shadow">
      <div className="flex items-center justify-between mb-4">
        <span className="text-sm text-gray-600 font-medium">{match.competition}</span>
        {match.isLive && (
          <div className="flex items-center text-patrick-pink text-sm font-medium">
            <div className="w-2 h-2 bg-patrick-pink rounded-full mr-2 animate-pulse"></div>
            AO VIVO
          </div>
        )}
      </div>
      
      <div className="text-center mb-4">
        <div className="flex items-center justify-between">
          <div className="text-center flex-1">
            <h3 className="font-semibold text-lg text-ocean-blue">{match.participant1}</h3>
          </div>
          <div className="mx-4 text-gray-400">
            <Star className="w-6 h-6 text-sponge-yellow" />
          </div>
          <div className="text-center flex-1">
            <h3 className="font-semibold text-lg text-ocean-blue">{match.participant2}</h3>
          </div>
        </div>
        
        <div className="flex items-center justify-center mt-2 text-gray-600">
          <Clock size={16} className="mr-1" />
          <span className="text-sm">{match.date} às {match.time}</span>
        </div>
      </div>
      
      <div className="grid grid-cols-3 gap-2 mb-4">
        <button
          onClick={() => onBet(match.id, 'participant1', match.odds.participant1)}
          className="bg-gray-100 hover:bg-ocean-blue hover:text-white p-3 rounded-lg text-center transition-colors group border-2 border-transparent hover:border-sponge-yellow"
        >
          <div className="text-xs text-gray-600 group-hover:text-white/80">{match.participant1}</div>
          <div className="font-bold">{match.odds.participant1.toFixed(2)}</div>
        </button>
        
        <button
          onClick={() => onBet(match.id, 'draw', match.odds.draw)}
          className="bg-gray-100 hover:bg-patrick-pink hover:text-white p-3 rounded-lg text-center transition-colors group border-2 border-transparent hover:border-sponge-yellow"
        >
          <div className="text-xs text-gray-600 group-hover:text-white/80">Empate</div>
          <div className="font-bold">{match.odds.draw.toFixed(2)}</div>
        </button>
        
        <button
          onClick={() => onBet(match.id, 'participant2', match.odds.participant2)}
          className="bg-gray-100 hover:bg-ocean-blue hover:text-white p-3 rounded-lg text-center transition-colors group border-2 border-transparent hover:border-sponge-yellow"
        >
          <div className="text-xs text-gray-600 group-hover:text-white/80">{match.participant2}</div>
          <div className="font-bold">{match.odds.participant2.toFixed(2)}</div>
        </button>
      </div>
    </div>
  );
};

export default MatchCard;
