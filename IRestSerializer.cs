using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WebRequestTools
{
    /// <summary>
    /// Interface for serialization options
    /// for RestUtility
    /// </summary>
    public interface IRESTSerializer
    {
        /// <summary>
        /// Serialize object of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="payload"></param>
        /// <returns></returns>
        string Serialize<T>(T payload);

        /// <summary>
        /// deserialize response into
        /// an object of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        T Deserialize<T>(string response);
    }
}
