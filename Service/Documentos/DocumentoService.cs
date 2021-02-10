using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using poclogger.CustomLogger;
using poclogger.Model.Documentos;

namespace poclogger.Service.Documentos
{
    public class DocumentoService : IDocumentoService
    {
        private readonly ILogger<DocumentoService> _logger;

        public DocumentoService(ILogger<DocumentoService> logger)
        {
            _logger = logger;
        }

        public void Processar(Documento docto)
        {
            //Log in-line
            _logger.Scope("Docto_Numero", docto.Numero).LogInformation("Documento iniciou processamento.");
            docto.Arquivar();
            _logger.Scope("Docto_Numero", docto.Numero).LogInformation("Documento terminou terminou processamento.");
        }
        
        public void Protestar(Documento docto)
        {
            //Log com abertura de escopo
            using (_logger.Scope("Docto_Numero", docto.Numero))
            {
                _logger.LogInformation("Documento iniciou protesto.");
                docto.Protestar();
                _logger.LogInformation("Documento terminou terminou protesto.");
            }
        }
        public void Arquivar(Documento docto)
        {
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
        }
    }
}