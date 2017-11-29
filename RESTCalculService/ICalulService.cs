using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace RESTCalculService
{   
    [ServiceContract]
    public interface ICalulService 
    {

        [OperationContract]
        [WebInvoke(UriTemplate = "/GetTabAmortissement", 
            RequestFormat=WebMessageFormat.Json,            
            ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        List<cAmortissement> GetTabAmortissement(cCalculOutputContract oCalcul);

        [OperationContract]
        [WebInvoke(UriTemplate = "/CalculEmprunte", 
            RequestFormat= WebMessageFormat.Json,   
            ResponseFormat = WebMessageFormat.Json, Method = "POST")] 
        cCalculOutputContract CalculEmprunte(cCalulInputContract oCalcul);
    }
}
