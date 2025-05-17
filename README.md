
📦 README - ProductCatalog

Este projeto é um aplicativo de catálogo de produtos, desenvolvido com .NET MAUI e ASP.NET Core 8. Ele consome dados da API pública: https://fakestoreapi.com.

🧰 Pré-requisitos

- .NET 8 SDK: https://dotnet.microsoft.com/en-us/download/dotnet/8.0
- Visual Studio 2022 ou superior com as cargas de trabalho:
  - Desenvolvimento de Aplicativos Multiplataforma (.NET MAUI)
  - ASP.NET e desenvolvimento Web
- Windows 10 ou superior (para executar como app Windows)

▶️ Como rodar o projeto

1. Clone o repositório:

   git clone https://github.com/gigoliveira/ProductCatalog.git
   cd ProductCatalog

✅ Primeiro Build no Visual Studio

- Abra o `.sln` no Visual Studio
- Selecione o projeto `ProductCatalog.Admin.Mobile` como inicial
- Escolha `Android Emulator` como target
- Pressione `F5` para rodar

📁 Estrutura do Projeto

- `Converters/`: Converters para bindings em XAML (ex: bool → ícone).
- `Helpers/`: Funções auxiliares como `RetryHelper`, `TimeoutHelper`.
- `Messages/`: Mensagens para comunicação entre viewmodels (ex: FavoriteChangedMessage).
- `Models/`: Representações dos dados da API (`ProductModel`, `RatingModel`).
- `Platforms/`: Implementações específicas por plataforma (Android, Windows).
- `Repositories/`: Regras de acesso à API externa (`ProductRepository.cs`).
- `Resources/`: Strings e imagens do app.
- `Services/`: Camada de lógica entre Repositories e ViewModels (`IProductService`).
- `ViewModels/`: ViewModels com estados e comandos para a UI.
- `Views/`: Arquivos XAML e code-behind para as telas (ex: `ProductDetailPage.xaml`).

🔧 Detalhes Técnicos

- A API utilizada: https://fakestoreapi.com
- Arquitetura MVVM: com CommunityToolkit.Mvvm (v8.2.2)
- UI & Funcionalidades: com .NET MAUI Community Toolkit (CommunityToolkit.Maui v7.0.1)
- Testes Unitários: com Moq (v4.20.72)
- MAUI Controls: baseados no .NET MAUI 8 (Microsoft.Maui.Controls v8.0.100)
- Navegação com `INavigationService`
- Favoritos são persistidos com `Preferences`
- Políticas de Retry com `RetryHelper` e `HttpClient`

📌 Observações

- O projeto suporta comunicação entre viewmodels via mensagens.
- Todos os estilos e fontes estão no diretório `Resources/`.

📬 Contato

Caso tenha dúvidas ou sugestões, entre em contato com o mantenedor do projeto.
