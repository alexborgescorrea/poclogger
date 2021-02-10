using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using poclogger.CustomLogger;
using poclogger.Model.Documentos;
using poclogger.Service.Documentos;

namespace poclogger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentoController : ControllerBase
    {
        private readonly IDocumentoService _documentoService;
        private readonly ILogger _logger;

        public DocumentoController(IDocumentoService documentoService,
                                   ILogger logger)
        {
            _documentoService = documentoService;
            _logger = logger;
        }

        public void Processar(Documento docto)
        {
            using (_logger.Scope("Docto_Id", docto.Id))
            {
                _documentoService.Processar(docto);
            }
        }

        public void Protestar(Documento docto)
        {
            using (_logger.Scope("Docto_Id", docto.Id))
            {
                _documentoService.Protestar(docto);
            }
        }

        public void Arquivar(Documento docto)
        {
            using (_logger.Scope("Docto_Id", docto.Id))
            {
                _documentoService.Arquivar(docto);
            }
        }
    }
}