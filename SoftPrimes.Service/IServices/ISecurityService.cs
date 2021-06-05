using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Service.IServices
{
    public interface ISecurityService : IBusinessService
    {
        string GetSha256Hash(string input);
        string EncryptData(string textData);
        string DecryptData(string EncryptedText);
    }
}
