using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPIAccessClass
{
    public class TarjetaEntity
    {
        public virtual string cui { get; set; }
        public virtual string direccion { get; set; }
        public virtual string direccion_municipio { get; set; }
        public virtual string direccion_departamento { get; set; }
        public virtual string emsion { get; set; }
        public virtual string vencimiento { get; set; }
        public virtual string primer_nombre { get; set; }
        public virtual string segundo_nombre { get; set; }
        public virtual string primer_apellido { get; set; }
        public virtual string segundo_apellido { get; set; }
        public virtual string apellido_casada { get; set; }
        public virtual string mrz { get; set; }
        public virtual string genero { get; set; }
        public virtual string estado_civil { get; set; }
        public virtual string nacionalidad { get; set; }
        public virtual string profesion { get; set; }
        public virtual string no_cedula { get; set; }
        public virtual string municipio_cedula { get; set; }
        public virtual string departamento_cedula { get; set; }
        public virtual string vecindad_municipio { get; set; }
        public virtual string vecindad_departamento { get; set; }
        public virtual string nacimiento_fecha { get; set; }
        public virtual string nacimiento_municipio { get; set; }
        public virtual string nacimiento_departamento { get; set; }
        public virtual string nacimiento_pais { get; set; }
        public virtual string libro { get; set; }
        public virtual string folio { get; set; }
        public virtual string partida { get; set; }
        public virtual string sleer { get; set; }
        public virtual string sescribir { get; set; }
        public virtual string oficial_activo { get; set; }
        public virtual byte[] imagen { get; set; }


    }
}
