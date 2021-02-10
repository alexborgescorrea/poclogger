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
![Log_no_Seq](/Images/Log_no_Seq.png)

- Para conseguir adicionar propriedades, que serão gravadas no json do log gerado, deve ser adicionado o comando <strong>Enrich.FromLogContext()</strong> na configuração incial do serilog.
![Enrich_FromLogContext](/Images/Enrich_FromLogContext.png)

       
### Adicionando váriaveis personalizadas para todos os log da aplicação
- Caso você deseje que uma determina propriedade esteja incluída em qualquer log gerado pelo o sitema, você deve usar o comando <strong>Enrich.WithProperty</strong>.
![Enrich_WithProperty](/Images/Enrich_WithProperty.png)
- O Serilog também disponibiliza propriedades extras, através da adição de pacotes.

        .Enrich.WithThreadId()
        .Enrich.WithThreadName()
        .Enrich.WithMachineName()
        .Enrich.WithEnvironmentUserName()        
- Pacotes a serem adicionados Serilog.Enrichers.Environment e Serilog.Enrichers.Thread    

### Adicionando váriaveis personalizadas em um cotexto específico
- Para isso, foi criada a classe <strong>ICustomLoggerScope</strong> para abstrair chamadas diretas do SeriLog no meio do código.
- Para habilitar a abstração é necessário adicionar o serviço na classe <strong>Startup</strong> da aplicação.
![ConfigureServices](/Images/ConfigureServices.png)

