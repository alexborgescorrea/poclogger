using poclogger.Model.Documentos;

namespace poclogger.Service.Documentos
{
    public interface IDocumentoService
    {
        void Processar(Documento docto);
        void Protestar(Documento docto);
        void Arquivar(Documento docto);
    }
}