# Futebol Simples Bets Hub - ASP.NET Core

Uma aplicação de apostas esportivas com tema da Fenda do Biquíni, desenvolvida em ASP.NET Core MVC.

## 🚀 Tecnologias Utilizadas

- **Backend**: ASP.NET Core 8.0
- **ORM**: Entity Framework Core
- **Banco de Dados**: SQL Server (LocalDB)
- **Frontend**: Razor Views + Tailwind CSS
- **Autenticação**: Session-based authentication

## 📋 Pré-requisitos

- .NET 8.0 SDK
- SQL Server LocalDB (incluído no Visual Studio)
- Visual Studio 2022 ou VS Code

## 🛠️ Como Executar

1. **Clone o repositório**
   ```bash
   git clone <url-do-repositorio>
   cd futebol-simples-bets-hub
   ```

2. **Restore as dependências**
   ```bash
   dotnet restore
   ```

3. **Execute as migrações do banco de dados**
   ```bash
   dotnet ef database update
   ```

4. **Execute a aplicação**
   ```bash
   dotnet run
   ```

5. **Acesse a aplicação**
   - URL: `https://localhost:5001` ou `http://localhost:5000`

## 👤 Conta Demo

Para testar a aplicação, use as seguintes credenciais:
- **Usuário**: `demo`
- **Senha**: `123456`

## 🏗️ Estrutura do Projeto

```
FutebolSimplesBetsHub/
├── Controllers/          # Controllers MVC
├── Data/                # DbContext e configurações do EF
├── Models/              # Modelos de dados
│   └── ViewModels/      # ViewModels para as views
├── Services/            # Serviços de negócio
├── Views/               # Views Razor
│   ├── Home/           # Views da página inicial
│   ├── Matches/        # Views das competições
│   ├── Bets/           # Views das apostas
│   ├── Account/        # Views de autenticação
│   └── Shared/         # Layout e componentes compartilhados
├── Program.cs           # Configuração da aplicação
├── appsettings.json     # Configurações
└── FutebolSimplesBetsHub.csproj
```

## 🎯 Funcionalidades

### ✅ Implementadas
- [x] Página inicial com partidas ao vivo e próximas
- [x] Listagem de todas as competições
- [x] Filtros por competição e busca
- [x] Sistema de apostas
- [x] Autenticação de usuários
- [x] Perfil do usuário
- [x] Histórico de apostas
- [x] Gerenciamento de saldo

### 🔄 Em Desenvolvimento
- [ ] Modal de apostas funcional
- [ ] Sistema de pagamentos
- [ ] Notificações em tempo real
- [ ] API REST para integração

## 🎨 Design

A aplicação utiliza o tema da Fenda do Biquíni com:
- **Cores principais**: Azul oceânico, amarelo esponja, rosa Patrick
- **Personagens**: Bob Esponja, Patrick, Lula Molusco, Sandy, etc.
- **Competições**: Corrida de Cavalo Marinho, Caça às Águas Vivas, etc.

## 📊 Banco de Dados

O banco de dados inclui as seguintes entidades:
- **Users**: Usuários da aplicação
- **Matches**: Partidas/competições
- **Bets**: Apostas dos usuários

## 🔧 Configuração

### Connection String
A connection string está configurada para SQL Server LocalDB:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=FutebolSimplesBetsHub;Trusted_Connection=true;MultipleActiveResultSets=true"
}
```

### Seed Data
O banco de dados é populado automaticamente com dados de exemplo:
- Usuário demo
- Partidas de exemplo
- Competições da Fenda do Biquíni

## 🚀 Deploy

Para fazer deploy em produção:
1. Configure a connection string para seu banco de dados
2. Execute `dotnet publish`
3. Configure o servidor web (IIS, Azure, etc.)

## 📝 Licença

Este projeto é apenas para fins educacionais e de demonstração.

## 🤝 Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull requests.
