
import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { Mail, Lock, Eye, EyeOff } from 'lucide-react';
import { toast } from '@/components/ui/use-toast';

const Login = () => {
  const [isLogin, setIsLogin] = useState(true);
  const [showPassword, setShowPassword] = useState(false);
  const [formData, setFormData] = useState({
    email: '',
    password: '',
    confirmPassword: '',
    name: ''
  });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    if (isLogin) {
      toast({
        title: "Login realizado!",
        description: "Bem-vindo de volta ao Bet Esponja!",
      });
    } else {
      if (formData.password !== formData.confirmPassword) {
        toast({
          title: "Erro no cadastro",
          description: "As senhas não coincidem.",
          variant: "destructive",
        });
        return;
      }
      
      toast({
        title: "Conta criada!",
        description: "Sua conta foi criada com sucesso. Bem-vindo ao Bet Esponja!",
      });
    }
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
  };

  return (
    <div className="min-h-screen bg-gray-50 pt-16">
      <div className="flex items-center justify-center min-h-[calc(100vh-4rem)] px-4">
        <div className="max-w-md w-full">
          {/* Logo */}
          <div className="text-center mb-8">
            <h1 className="text-3xl font-bold text-primary-blue mb-2">🧽 Bet Esponja</h1>
            <p className="text-gray-600">
              {isLogin ? 'Entre na sua conta' : 'Crie sua conta'}
            </p>
          </div>

          {/* Form */}
          <div className="bg-neutral-white rounded-lg shadow-md p-8">
            <form onSubmit={handleSubmit} className="space-y-6">
              {!isLogin && (
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-2">
                    Nome completo
                  </label>
                  <input
                    type="text"
                    name="name"
                    value={formData.name}
                    onChange={handleChange}
                    className="w-full p-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-blue focus:border-transparent"
                    placeholder="Digite seu nome completo"
                    required={!isLogin}
                  />
                </div>
              )}

              <div>
                <label className="block text-sm font-medium text-gray-700 mb-2">
                  E-mail
                </label>
                <div className="relative">
                  <Mail className="absolute left-3 top-3 text-gray-400" size={20} />
                  <input
                    type="email"
                    name="email"
                    value={formData.email}
                    onChange={handleChange}
                    className="w-full pl-10 pr-3 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-blue focus:border-transparent"
                    placeholder="Digite seu e-mail"
                    required
                  />
                </div>
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700 mb-2">
                  Senha
                </label>
                <div className="relative">
                  <Lock className="absolute left-3 top-3 text-gray-400" size={20} />
                  <input
                    type={showPassword ? 'text' : 'password'}
                    name="password"
                    value={formData.password}
                    onChange={handleChange}
                    className="w-full pl-10 pr-10 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-blue focus:border-transparent"
                    placeholder="Digite sua senha"
                    required
                  />
                  <button
                    type="button"
                    onClick={() => setShowPassword(!showPassword)}
                    className="absolute right-3 top-3 text-gray-400 hover:text-gray-600"
                  >
                    {showPassword ? <EyeOff size={20} /> : <Eye size={20} />}
                  </button>
                </div>
              </div>

              {!isLogin && (
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-2">
                    Confirmar senha
                  </label>
                  <div className="relative">
                    <Lock className="absolute left-3 top-3 text-gray-400" size={20} />
                    <input
                      type={showPassword ? 'text' : 'password'}
                      name="confirmPassword"
                      value={formData.confirmPassword}
                      onChange={handleChange}
                      className="w-full pl-10 pr-3 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-blue focus:border-transparent"
                      placeholder="Confirme sua senha"
                      required={!isLogin}
                    />
                  </div>
                </div>
              )}

              {isLogin && (
                <div className="flex items-center justify-between">
                  <label className="flex items-center">
                    <input type="checkbox" className="mr-2" />
                    <span className="text-sm text-gray-600">Lembrar de mim</span>
                  </label>
                  <Link to="/esqueci-senha" className="text-sm text-primary-blue hover:underline">
                    Esqueci minha senha
                  </Link>
                </div>
              )}

              <button
                type="submit"
                className="w-full bg-primary-blue text-neutral-white py-3 rounded-lg font-semibold hover:bg-primary-blue/90 transition-colors"
              >
                {isLogin ? 'Entrar' : 'Criar conta'}
              </button>
            </form>

            {/* Switch between login/register */}
            <div className="mt-6 text-center">
              <p className="text-gray-600">
                {isLogin ? 'Não tem uma conta?' : 'Já tem uma conta?'}
                <button
                  onClick={() => setIsLogin(!isLogin)}
                  className="ml-1 text-primary-blue font-semibold hover:underline"
                >
                  {isLogin ? 'Cadastre-se' : 'Faça login'}
                </button>
              </p>
            </div>

            {/* Terms */}
            {!isLogin && (
              <div className="mt-4 text-xs text-gray-500 text-center">
                Ao criar uma conta, você concorda com nossos{' '}
                <Link to="/termos" className="text-primary-blue hover:underline">
                  Termos de Uso
                </Link>{' '}
                e{' '}
                <Link to="/privacidade" className="text-primary-blue hover:underline">
                  Política de Privacidade
                </Link>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;
