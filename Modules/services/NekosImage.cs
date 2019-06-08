using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace Neko_Test.Responses
{
    /// <summary>
    ///     Represents for /img endpoint.
    /// </summary>
    public class NekosImage
    {
        /// <summary>
        ///     The image URL depends on your search.
        /// </summary>
       public string neko { get; set; }
    }
}