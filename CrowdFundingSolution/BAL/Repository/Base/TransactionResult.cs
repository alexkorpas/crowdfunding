using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class TransactionResult
    {
        public TransResult Result { get; set; }
        public string Message { get; set; }
        public object ReturnObject { get; set; }
        public int? Id { get; set; }

        public TransactionResult()
        {

        }

        public TransactionResult(TransResult result, string message, object returnObject, int? id = null)
        {
            Result = result;
            Message = message;
            ReturnObject = returnObject;
            Id = id;
        }
    }//End Class

    public enum TransResult { Success, Fail }
}//End Namespace
