# Project Guidelines

## Code Style
C# code uses PascalCase for class names, methods, and properties (e.g., `KeyVaultOptions`, `Save` method in [KeyVault.cs](../src/plugin.store.keyvault/KeyVault.cs)). Nullable reference types are enabled globally. Asynchronous methods use `async`/`await` pattern. Classes in plugins are marked `internal` and inherit from base option classes like `StorePluginOptions`. Exceptions are thrown with descriptive messages (e.g., `throw new InvalidOperationException("No credentials have been provided")` in [SftpClient.cs](../src/plugin.validation.http.sftp/SftpClient.cs)).

## Architecture
Plugin-based architecture separates core functionality in `main.lib` from extensible plugins in `plugin.*` directories. Main components include ACME client logic, certificate management, and scheduling. Plugins handle validation (DNS/HTTP), storage (KeyVault, user store), and CSR generation. Autofac dependency injection container manages services and dynamically loads plugins via reflection ([Autofac.cs](../src/main/Autofac.cs)). Data flows from command-line arguments through `ArgumentsParser` to plugin resolution and execution in the `Wacs` class.

## Build and Test
- Restore dependencies: `dotnet restore src\main\wacs.csproj`
- Build main project: `dotnet build src\main\wacs.csproj -c Release`
- Publish for multiple architectures: `dotnet publish src\main\wacs.csproj -c Release -r win-x64 --self-contained`
- Run tests: `dotnet test src\main.test\wacs.test.csproj` (uses MSTest framework)

## Project Conventions
Plugins use `[IPlugin.Plugin<...>]` attributes for metadata and registration (e.g., in [KeyVault.cs](../src/plugin.store.keyvault/KeyVault.cs)). Logging via Serilog with levels like `_log.Error`, `_log.Warning`, `_log.Verbose` for structured output. Credentials and secrets accessed through `SecretServiceManager` to avoid plain-text exposure. Assembly scanning loads plugins dynamically without explicit imports.

## Integration Points
Plugins integrate via interfaces like `IStorePlugin`, `IValidationPlugin` and are discovered through `PluginService`. External dependencies include Azure SDKs, DNS APIs, and FTP clients. Cross-component communication uses events and service injection (e.g., `ILogService`, `ISettingsService`).

## Security
Handles sensitive certificate data and private keys; uses `SecretServiceManager` for credential retrieval to prevent logging secrets. Azure KeyVault integration for secure certificate storage ([KeyVault.cs](../src/plugin.store.keyvault/KeyVault.cs)). Avoids exposing private keys in logs; validates inputs for DNS/HTTP challenges. Requires Windows OS platform support.