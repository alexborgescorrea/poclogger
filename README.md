# poclogger
Prova de conceito do Serilog

## Pacote
- Para usar o Serilog é necessário a adição da package **Serilog.AspNetCore**
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
Para adicionar uma nova saída utlizasse o comando **"WriteTo.SaidaDesajada"**. Alguns exemplos de saídas.:

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
- No asp.netcore o log funciona com a injeção da interface **ILogger**.
- Através dela são disponíveis vários métodos de extensão para determinar o nível do log.

        LogCritical
        LogDebug
        LogError
        LogInformation
        LogTrace
        LogWarning

## Como adicionar váriaveis personalizadas ao log
![Log_no_Seq](/Images/Log_no_Seq.png)

- Para conseguir adicionar propriedades, que serão gravadas no json do log gerado, deve ser adicionado o comando **Enrich.FromLogContext()** na configuração incial do serilog.
![Enrich_FromLogContext](/Images/Enrich_FromLogContext.png)

       
### Adicionando váriaveis personalizadas para todos os log da aplicação
- Caso você deseje que uma determina propriedade esteja incluída em qualquer log gerado pelo o sitema, você deve usar o comando **Enrich.WithProperty**.
![Enrich_WithProperty](/Images/Enrich_WithProperty.png)
- O Serilog também disponibiliza propriedades extras, através da adição de pacotes.

        .Enrich.WithThreadId()
        .Enrich.WithThreadName()
        .Enrich.WithMachineName()
        .Enrich.WithEnvironmentUserName()        
- Pacotes a serem adicionados Serilog.Enrichers.Environment e Serilog.Enrichers.Thread    

### Adicionando váriaveis personalizadas em um cotexto específico
- Para isso, foi criada a classe **ICustomLoggerScope** para abstrair chamadas diretas do SeriLog no meio do código.
- Para habilitar a abstração é necessário adicionar o serviço na classe **Startup** da aplicação.
![ConfigureServices](/Images/ConfigureServices.png)
- Com isso agora é possível trabalhar das seguintes formas:
![LogScope](/Images/LogScope.png)

- Utilizando o método de extensão **Scope**. Ele cria um escopo de Log que implementa a interface **IDisposable**. 
*O c# usa a estratégia de escopos IDisposable encadeados. Então os escopos mais internos são filhos dos mais externos*        
1. Scope inline. Cria um scope e loga a aperação. Dessa forma o **Scope.Dispose** é chamado dentro do método **LogInformation** não precisando se preocupar com o ciclo de vida do scope

        //Log in-line
        _logger.Scope("Docto_Numero", docto.Numero).LogInformation("Documento iniciou processamento.");
        docto.Arquivar();
        _logger.Scope("Docto_Numero", docto.Numero).LogInformation("Documento terminou terminou processamento.");
        
2. Scope aberto. Onde todo o log chamado dentro desse scope vai conter as propriedades definidas na abertura do scope.

        //Log com abertura de escopo
        using (_logger.Scope("Docto_Numero", docto.Numero))
        {
                _logger.LogInformation("Documento iniciou protesto.");
                docto.Protestar();
                _logger.LogInformation("Documento terminou terminou protesto.");
        }

3. Scope com mais de uma propriedade. Funciona da mesma forma do scope aberto, com a diferença que é possível definir várias propriedades na abertura.

        //Log com abertura de escopo, com adição de várias propriedades
        using (_logger.Scope(new Dictionary<string, object>
        {
                ["Docto_Numero"] = docto.Numero,
                ["Docto_Trantante"] = docto.NomeTrantante
        }))
        {
                _logger.LogInformation("Documento iniciou arquivamento.");
                docto.Arquivar();
                _logger.LogInformation("Documento terminou terminou arquivamento.");
        }        

