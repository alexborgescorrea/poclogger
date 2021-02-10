# poclogger
Prova de conceito do Serilog

## Pacote
- Para usar o Serilog é necessário a adição da package <strong>Serilog.AspNetCore</strong>
- dotnet add package Serilog.AspNetCore

### Configuração do Serilog no projeto

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(hostBuilder => 
                {
                    Serilog.Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Console() //Configura as saídas dos logs para o console
                    .WriteTo.Seq("http://localhost:5341/") //Configura as saídas dos logs para o Seq. Seq é uma aplicativo externo para ajudar visualizar os logs gerados pelo o sistema
                    .CreateLogger();
                })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
    
- Essa é a forma sugerida de configuração inicial do Serilog
- GitHub do https://github.com/serilog/serilog-aspnetcore 
- Os logs gerados pelo o Serilog pode ter várias saídas ao mesmo tempo. 
Para adicionar uma nova saída utlizasse o comando <strong>"WriteTo.SaidaDesajada"</strong>. Alguns exemplos de saídas.:

      .WriteTo.Console(....//Console
      .WriteTo.Seq(....//Seq
      .WriteTo.File(....//Arquivo 
      .WriteTo.Elasticsearch(....//Elasticsearch
       Entre outros
- As saídas não fazem parte do pacote principal do Serilog, sendo necessário baixar as desejadas via nuget. Exemplo de pacotes disponiveis.:
Serilog.Sinks.Console, 
Serilog.Sinks.ElasticSearch, 
Serilog.Sinks.File, 
Serilog.Sinks.Seq, 

## Como logar?
- No asp.netcore o log funciona com a injeção da interface <strong>ILogger</strong>.
- Através dela são disponíveis vários métodos de extensão para determinar o nível do log.

        LogCritical
        LogDebug
        LogError
        LogInformation
        LogTrace
        LogWarning

## Como adicionar váriaveis personalizadas ao log
![Customer](/Images/Log_no_Seq.png)
        
