using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace RESTCalculService
{
    [DataContract]
    public class cCalulInputContract
    {
        [DataMember] 
        public decimal dMontantAchat { get; set; }

        [DataMember] 
        public decimal dFondsPropores { get; set; }

        [DataMember]
        public decimal dFraisDachat { get; set; }
        [DataMember]
        public decimal dFraisDhypotheque { get; set; }
        [DataMember] 
        public int iDureeCredit { get; set; }

        [DataMember] 
        public double iTauxInteretAnnuel { get; set; }
    }

    [DataContract]
    public class cCalculOutputContract
    {
        [DataMember]
        public decimal dMontantaEmprunter { get; set; }

        [DataMember]
        public decimal dMensualite { get; set; }

        [DataMember]

        public decimal TauxInteretMensuel { get; set; }

        public virtual cCalulInputContract CurentInput { get; set; }

        public decimal valeurInconnue { get; set; }
    }

    [DataContract]
    public class cAmortissement
    {
        [DataMember]
        public decimal dSoldeDebut { get; set; }
        [DataMember]
        public decimal dMensualite { get; set; }
        [DataMember]
        public decimal dInteret { get; set; }
        [DataMember]
        public decimal dCapitalRembourse { get; set; }
        [DataMember]
        public decimal dSoldeFin { get; set; }
    }
}