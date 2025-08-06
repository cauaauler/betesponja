
import React, { useState } from 'react';
import { User, CreditCard, History, Settings, LogOut, Eye, EyeOff } from 'lucide-react';

const Profile = () => {
  const [activeTab, setActiveTab] = useState('dashboard');
  const [showBalance, setShowBalance] = useState(true);

  const tabs = [
    { id: 'dashboard', label: 'Dashboard', icon: User },
    { id: 'account', label: 'Minha Conta', icon: Settings },
    { id: 'transactions', label: 'Transações', icon: CreditCard },
  ];

  const transactions = [
    { id: '1', type: 'deposit', amount: 100, date: '30/06/2024', description: 'Depósito via PIX' },
    { id: '2', type: 'bet', amount: -50, date: '30/06/2024', description: 'Aposta Flamengo vs Palmeiras' },
    { id: '3', type: 'win', amount: 105, date: '30/06/2024', description: 'Ganho da aposta' },
    { id: '4', type: 'withdraw', amount: -80, date: '29/06/2024', description: 'Saque para conta bancária' },
  ];

  const getTransactionIcon = (type: string) => {
    switch (type) {
      case 'deposit': return '💰';
      case 'bet': return '🎯';
      case 'win': return '🏆';
      case 'withdraw': return '🏧';
      default: return '💳';
    }
  };

  const getTransactionColor = (type: string) => {
    switch (type) {
      case 'deposit':
      case 'win': return 'text-green-600';
      case 'bet':
      case 'withdraw': return 'text-red-600';
      default: return 'text-gray-600';
    }
  };

  return (
    <div className="min-h-screen bg-gray-50 pt-16">
      <div className="container mx-auto px-4 py-8">
        <h1 className="text-3xl font-bold text-gray-800 mb-8">Área do Usuário</h1>
        
        {/* Profile Header */}
        <div className="bg-white rounded-lg shadow-md p-6 mb-8">
          <div className="flex items-center justify-between">
            <div className="flex items-center">
              <div className="w-16 h-16 bg-[#0A3D62] rounded-full flex items-center justify-center text-white text-xl font-bold mr-4">
                JD
              </div>
              <div>
                <h2 className="text-xl font-bold text-gray-800">João da Silva</h2>
                <p className="text-gray-600">joao.silva@email.com</p>
                <p className="text-sm text-gray-500">Membro desde 15/05/2024</p>
              </div>
            </div>
            
            <div className="text-right">
              <div className="flex items-center">
                <span className="text-sm text-gray-600 mr-2">Saldo:</span>
                <span className="text-2xl font-bold text-[#0A3D62]">
                  {showBalance ? 'R$ 250,00' : '••••••'}
                </span>
                <button
                  onClick={() => setShowBalance(!showBalance)}
                  className="ml-2 text-gray-500 hover:text-gray-700"
                >
                  {showBalance ? <EyeOff size={20} /> : <Eye size={20} />}
                </button>
              </div>
              <div className="flex space-x-2 mt-3">
                <button className="bg-[#0A3D62] text-white px-4 py-2 rounded-lg text-sm hover:bg-[#0A3D62]/90">
                  Depositar
                </button>
                <button className="border border-[#0A3D62] text-[#0A3D62] px-4 py-2 rounded-lg text-sm hover:bg-[#0A3D62]/10">
                  Sacar
                </button>
              </div>
            </div>
          </div>
        </div>

        {/* Tabs */}
        <div className="bg-white rounded-lg shadow-md mb-8">
          <div className="border-b border-gray-200">
            <div className="flex">
              {tabs.map(tab => {
                const Icon = tab.icon;
                return (
                  <button
                    key={tab.id}
                    onClick={() => setActiveTab(tab.id)}
                    className={`flex items-center px-6 py-4 font-medium text-sm border-b-2 transition-colors ${
                      activeTab === tab.id
                        ? 'border-[#0A3D62] text-[#0A3D62]'
                        : 'border-transparent text-gray-600 hover:text-gray-800'
                    }`}
                  >
                    <Icon size={18} className="mr-2" />
                    {tab.label}
                  </button>
                );
              })}
            </div>
          </div>

          <div className="p-6">
            {activeTab === 'dashboard' && (
              <div>
                <h3 className="text-lg font-semibold mb-6">Resumo da Conta</h3>
                
                <div className="grid md:grid-cols-3 gap-6 mb-8">
                  <div className="bg-gray-50 p-4 rounded-lg">
                    <h4 className="font-medium text-gray-700 mb-2">Apostas este mês</h4>
                    <p className="text-2xl font-bold text-gray-800">12</p>
                  </div>
                  <div className="bg-gray-50 p-4 rounded-lg">
                    <h4 className="font-medium text-gray-700 mb-2">Taxa de acerto</h4>
                    <p className="text-2xl font-bold text-green-600">75%</p>
                  </div>
                  <div className="bg-gray-50 p-4 rounded-lg">
                    <h4 className="font-medium text-gray-700 mb-2">Lucro do mês</h4>
                    <p className="text-2xl font-bold text-green-600">+R$ 145,00</p>
                  </div>
                </div>

                <div className="bg-blue-50 p-4 rounded-lg border border-blue-200">
                  <h4 className="font-medium text-blue-800 mb-2">🎉 Parabéns!</h4>
                  <p className="text-blue-700">Você está entre os top 20% apostadores este mês!</p>
                </div>
              </div>
            )}

            {activeTab === 'account' && (
              <div>
                <h3 className="text-lg font-semibold mb-6">Configurações da Conta</h3>
                
                <div className="space-y-6">
                  <div className="grid md:grid-cols-2 gap-6">
                    <div>
                      <label className="block text-sm font-medium text-gray-700 mb-2">
                        Nome completo
                      </label>
                      <input
                        type="text"
                        defaultValue="João da Silva"
                        className="w-full p-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-[#0A3D62] focus:border-transparent"
                      />
                    </div>
                    <div>
                      <label className="block text-sm font-medium text-gray-700 mb-2">
                        E-mail
                      </label>
                      <input
                        type="email"
                        defaultValue="joao.silva@email.com"
                        className="w-full p-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-[#0A3D62] focus:border-transparent"
                      />
                    </div>
                  </div>
                  
                  <div className="grid md:grid-cols-2 gap-6">
                    <div>
                      <label className="block text-sm font-medium text-gray-700 mb-2">
                        Telefone
                      </label>
                      <input
                        type="tel"
                        defaultValue="(11) 99999-9999"
                        className="w-full p-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-[#0A3D62] focus:border-transparent"
                      />
                    </div>
                    <div>
                      <label className="block text-sm font-medium text-gray-700 mb-2">
                        Data de nascimento
                      </label>
                      <input
                        type="date"
                        defaultValue="1990-01-01"
                        className="w-full p-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-[#0A3D62] focus:border-transparent"
                      />
                    </div>
                  </div>

                  <div className="flex justify-between items-center pt-6">
                    <button className="bg-[#0A3D62] text-white px-6 py-3 rounded-lg hover:bg-[#0A3D62]/90">
                      Salvar Alterações
                    </button>
                    
                    <button className="flex items-center text-red-600 hover:text-red-700">
                      <LogOut size={18} className="mr-2" />
                      Sair da conta
                    </button>
                  </div>
                </div>
              </div>
            )}

            {activeTab === 'transactions' && (
              <div>
                <h3 className="text-lg font-semibold mb-6">Histórico de Transações</h3>
                
                <div className="space-y-3">
                  {transactions.map(transaction => (
                    <div key={transaction.id} className="flex items-center justify-between p-4 border border-gray-200 rounded-lg">
                      <div className="flex items-center">
                        <span className="text-2xl mr-3">
                          {getTransactionIcon(transaction.type)}
                        </span>
                        <div>
                          <p className="font-medium">{transaction.description}</p>
                          <p className="text-sm text-gray-600">{transaction.date}</p>
                        </div>
                      </div>
                      <span className={`font-bold ${getTransactionColor(transaction.type)}`}>
                        {transaction.amount > 0 ? '+' : ''}R$ {Math.abs(transaction.amount).toFixed(2)}
                      </span>
                    </div>
                  ))}
                </div>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Profile;
