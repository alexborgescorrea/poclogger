using System;
using poclogger.Model.Documentos.Enums;

namespace poclogger.Model.Documentos
{
    public class Documento
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public EnumStatusDocumento Status { get; private set; }
        public DateTime DataHora { get; private set; } = DateTime.Now;
        public string Numero { get; set; }
        public string Descricao { get; set; }
        public string NomeTrantante { get; set; }

        public void Processado()
        {
            Status = EnumStatusDocumento.Processado;
        }
        
        public void Protestar()
        {
            Status = EnumStatusDocumento.Protestado;
        }
        public void Arquivar()
        {
            Status = EnumStatusDocumento.Arquivado;
        }
    }
}