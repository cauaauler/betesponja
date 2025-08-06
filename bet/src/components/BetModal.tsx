
import React, { useState } from 'react';
import { X, Calculator } from 'lucide-react';

interface BetModalProps {
  isOpen: boolean;
  onClose: () => void;
  bet: {
    matchId: string;
    participant1: string;
    participant2: string;
    betType: string;
    odds: number;
  } | null;
  onConfirmBet: (amount: number) => void;
}

const BetModal: React.FC<BetModalProps> = ({ isOpen, onClose, bet, onConfirmBet }) => {
  const [betAmount, setBetAmount] = useState<number>(10);

  if (!isOpen || !bet) return null;

  const potentialReturn = betAmount * bet.odds;
  const profit = potentialReturn - betAmount;

  const getBetTypeLabel = (type: string) => {
    switch (type) {
      case 'participant1': return `Vitória de ${bet?.participant1}`;
      case 'draw': return 'Empate';
      case 'participant2': return `Vitória de ${bet?.participant2}`;
      default: return type;
    }
  };

  const handleConfirm = () => {
    onConfirmBet(betAmount);
    onClose();
  };

  return (
    <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50 p-4">
      <div className="bg-white rounded-lg max-w-md w-full p-6 border-4 border-sponge-yellow">
        <div className="flex justify-between items-center mb-4">
          <h2 className="text-xl font-bold text-ocean-blue">Confirmar Aposta</h2>
          <button onClick={onClose} className="text-gray-500 hover:text-gray-700">
            <X size={24} />
          </button>
        </div>
        
        <div className="mb-6">
          <div className="bg-sponge-yellow/20 p-4 rounded-lg mb-4 border-2 border-sponge-yellow">
            <h3 className="font-semibold text-ocean-blue">{bet.participant1} vs {bet.participant2}</h3>
            <p className="text-sm text-gray-600">
              Aposta: {getBetTypeLabel(bet.betType)} (Odd: {bet.odds.toFixed(2)})
            </p>
          </div>
          
          <div className="space-y-4">
            <div>
              <label className="block text-sm font-medium text-ocean-blue mb-2">
                Valor da Aposta (R$)
              </label>
              <input
                type="number"
                min="1"
                step="1"
                value={betAmount}
                onChange={(e) => setBetAmount(Number(e.target.value))}
                className="w-full p-3 border-2 border-sponge-yellow rounded-lg focus:ring-2 focus:ring-ocean-blue focus:border-ocean-blue"
              />
            </div>
            
            <div className="bg-green-50 p-4 rounded-lg border-2 border-green-200">
              <div className="flex items-center mb-2">
                <Calculator size={18} className="text-green-600 mr-2" />
                <span className="font-semibold text-green-800">Retorno Potencial</span>
              </div>
              <div className="space-y-1 text-sm">
                <div className="flex justify-between">
                  <span>Valor apostado:</span>
                  <span>R$ {betAmount.toFixed(2)}</span>
                </div>
                <div className="flex justify-between">
                  <span>Lucro potencial:</span>
                  <span className="text-green-600 font-semibold">R$ {profit.toFixed(2)}</span>
                </div>
                <div className="flex justify-between border-t pt-1">
                  <span className="font-semibold">Total a receber:</span>
                  <span className="font-bold text-green-600">R$ {potentialReturn.toFixed(2)}</span>
                </div>
              </div>
            </div>
          </div>
        </div>
        
        <div className="flex space-x-3">
          <button
            onClick={onClose}
            className="flex-1 py-3 border-2 border-gray-300 text-gray-700 rounded-lg hover:bg-gray-50 transition-colors"
          >
            Cancelar
          </button>
          <button
            onClick={handleConfirm}
            className="flex-1 py-3 bg-ocean-blue text-white rounded-lg hover:bg-blue-800 transition-colors font-semibold border-2 border-sponge-yellow"
          >
            Confirmar Aposta
          </button>
        </div>
      </div>
    </div>
  );
};

export default BetModal;
