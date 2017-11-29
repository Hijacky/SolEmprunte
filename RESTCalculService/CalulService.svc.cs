using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;
using System.Collections;

namespace RESTCalculService
{    
    public class CalulService : ICalulService
    {


        public List<cAmortissement> GetTabAmortissement(cCalculOutputContract oCalcul)
        {
            
            List<cAmortissement> tabAmortissement = new List<cAmortissement>();
            try
            {
                decimal tempValue = oCalcul.dMontantaEmprunter;
                for (int i = 1; i < oCalcul.CurentInput.iDureeCredit; i++ )
                {
                    cAmortissement oAmortissement = new cAmortissement();
                    oAmortissement.dSoldeDebut = tempValue;
                    oAmortissement.dMensualite = oCalcul.dMensualite;
                    oAmortissement.dInteret = decimal.Round(tempValue * oCalcul.TauxInteretMensuel, 2);
                    oAmortissement.dCapitalRembourse = oCalcul.dMensualite - oAmortissement.dInteret;
                    oAmortissement.dSoldeFin = oAmortissement.dSoldeDebut - oAmortissement.dCapitalRembourse;
                    tempValue = oAmortissement.dSoldeFin;
                    tabAmortissement.Add(oAmortissement);
                }

            }
            catch (Exception ex)
            {
                throw new FaultException<string>
                     (ex.Message);
            }
            return tabAmortissement;
        }

        public cCalculOutputContract CalculEmprunte(cCalulInputContract oCalculInput)
        {
            cCalculOutputContract oCalculOutput = new cCalculOutputContract();
            try
            {
                if (oCalculInput.dMontantAchat > 50000M)
                    oCalculInput.dFraisDachat =  (decimal)((oCalculInput.dFraisDachat*10) / 100);
                oCalculInput.dFraisDhypotheque = (decimal)((oCalculInput.dFraisDachat *2) /100);
                oCalculOutput.dMontantaEmprunter = decimal.Add(Decimal.Subtract(oCalculInput.dFraisDachat, oCalculInput.dFraisDachat),decimal.Add( oCalculInput.dFondsPropores, oCalculInput.dFraisDhypotheque));
                oCalculOutput.TauxInteretMensuel = (decimal) Math.Pow((1 + oCalculInput.iTauxInteretAnnuel), (1 / 12)) - 1;
                oCalculOutput.TauxInteretMensuel = Decimal.Round(oCalculOutput.TauxInteretMensuel, 3) ;
                // Capital ?? !!! 
                double capiatl = 2234;
                oCalculOutput.dMensualite =(decimal) Math.Pow(capiatl * (double)oCalculOutput.TauxInteretMensuel*(1+oCalculInput.iTauxInteretAnnuel),Math.Pow((1+(double) oCalculOutput.TauxInteretMensuel),oCalculInput.iDureeCredit));

            }
            catch (Exception ex)
            {
                throw new FaultException<string>
                     (ex.Message);
            }
            return oCalculOutput;
        }
    }
}
