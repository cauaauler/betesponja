
import React from 'react';
import { Link, useLocation } from 'react-router-dom';
import { Home, Calendar, Trophy, User } from 'lucide-react';

const Navbar = () => {
  const location = useLocation();
  
  const navItems = [
    { path: '/', label: 'Competições', icon: Home },
    { path: '/partidas', label: 'Competições', icon: Calendar },
    { path: '/minhas-apostas', label: 'Minhas Apostas', icon: Trophy },
 //   { path: '/perfil', label: 'Perfil', icon: User },
  ];

  return (
    <nav className="bg-primary-blue text-neutral-white shadow-lg fixed top-0 w-full z-50">
      <div className="container mx-auto px-4">
        <div className="flex justify-between items-center h-16">
          <Link to="/" className="flex items-center">
            <img 
              src="/lovable-uploads/299dedfd-4ee3-42fd-8366-1af55b8e40b5.png" 
              alt="Bet Esponja" 
              className="h-14 w-auto"
            />
          </Link>
          
          <div className="hidden md:flex space-x-8">
            {navItems.map((item) => {
              const Icon = item.icon;
              return (
                <Link
                  key={item.path}
                  to={item.path}
                  className={`flex items-center space-x-2 px-3 py-2 rounded-lg transition-colors ${
                    location.pathname === item.path
                      ? 'bg-accent-yellow text-primary-blue'
                      : 'hover:bg-white/10'
                  }`}
                >
                  <Icon size={18} />
                  <span>{item.label}</span>
                </Link>
              );
            })}
          </div>

          <div className="flex items-center space-x-4">
            <span className="hidden md:block text-sm">Saldo: R$ 250,00</span>
            <Link
              to="/login"
              className="bg-accent-yellow text-primary-blue px-4 py-2 rounded-lg font-medium hover:bg-yellow-300 transition-colors"
            >
              Entrar
            </Link>
          </div>
        </div>
      </div>

      {/* Mobile Navigation */}
      <div className="md:hidden bg-primary-blue border-t border-white/20">
        <div className="flex justify-around py-2">
          {navItems.map((item) => {
            const Icon = item.icon;
            return (
              <Link
                key={item.path}
                to={item.path}
                className={`flex flex-col items-center py-2 px-3 rounded-lg ${
                  location.pathname === item.path
                    ? 'text-accent-yellow'
                    : 'text-white/70'
                }`}
              >
                <Icon size={18} />
                <span className="text-xs mt-1">{item.label}</span>
              </Link>
            );
          })}
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
