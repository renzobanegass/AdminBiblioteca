using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminBiblioteca.Models
{
    public interface ISocioView
    {
        Socio Socio { get; }
        void SendInfo(string strInfo);
    }
}
