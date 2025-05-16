
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

2. Restaure os pacotes:

   dotnet restore

3. Execute o projeto:

   dotnet build
   dotnet run --project ProductCatalog.Admin.Mobile

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
- Políticas de Retry com `RetryHelper` e `HttpClient`
- MVVM com CommunityToolkit.Mvvm
- Navegação com `INavigationService`
- Favoritos são persistidos com `Preferences`

✅ Primeiro Build no Visual Studio

- Abra o `.sln` no Visual Studio
- Selecione o projeto `ProductCatalog.Admin.Mobile` como inicial
- Escolha `Windows Machine` como target
- Pressione `F5` para rodar

📌 Observações

- O projeto suporta comunicação entre viewmodels via mensagens.
- É possível simular favoritar produtos offline.
- Todos os estilos e fontes estão no diretório `Resources/`.

📬 Contato

Caso tenha dúvidas ou sugestões, entre em contato com o mantenedor do projeto.
