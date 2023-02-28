using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class DomainParser
    {
        public static string ParseTldFromDomainName(string domainName)
        {
            if (string.IsNullOrEmpty(domainName))
            {
                return string.Empty;
            }

            string[] parts = domainName.Split('.');
            if (parts.Length > 1)
            {
                return String.Join(".", parts.Skip(1));
            }

            return string.Empty;
        }
        
        public static string ParseDomainFromDomainName(string domainName)
        {
            if (string.IsNullOrEmpty(domainName))
            {
                return string.Empty;
            }

            string[] parts = domainName.Split('.');
            if (parts.Length > 1)
            {
                return parts[0];
            }

            return string.Empty;
        }

        public static string ParseDomainFromNameServer(string nameServer)
        {
            if (string.IsNullOrEmpty(nameServer))
            {
                return string.Empty;
            }

            string[] parts = nameServer.Split('.');
            if (parts.Length > 1)
            {
                return string.Join(".", parts.Skip(1));
            }

            return string.Empty;
        }
    }
}