using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Trabis.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Test().GetAwaiter().GetResult();
        }

        static async Task Test()
        {
            #region Definitions

            string ote1UserToken = "Ga7boK1U1UHvz4GRDAkgE8Pk1+FKO73EaaYZ0PPUDsi+ktYX3i68+Y8Nbp5gBHsMWJy3+YxpDnaVKDL1wMijmw==";
            string ote2UserToken = "dWHqIKzYdJ7sFABVMCSa3shyJmqObSK5X/yxAloaOE0vgwEa26hRKB+DxxNU6q3BnXiPlVuYf6odDTkUv+yttw==";

            string apiUrl = "https://rest-gbs-trabis.domainnameapi.com/";

            HttpClient ote1Client = new HttpClient();

            ote1Client.BaseAddress = new Uri(apiUrl);
            ote1Client.DefaultRequestHeaders.Accept.Clear();
            ote1Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ote1Client.DefaultRequestHeaders.Authorization = null;
            ote1Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", ote1UserToken);

            HttpClient ote2Client = new HttpClient();

            ote2Client.BaseAddress = new Uri(apiUrl);
            ote2Client.DefaultRequestHeaders.Accept.Clear();
            ote2Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ote2Client.DefaultRequestHeaders.Authorization = null;
            ote2Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", ote2UserToken);

            string content = "";

            string citizienRepeatCode = RandomStringGenerator.GenerateLowwerCase(5, 5);
            string citizienDomain = string.Format("example{0}.com.tr", citizienRepeatCode);
            string citizienNameServer1 = string.Format("ns1.example{0}.net", citizienRepeatCode);
            string citizienNameServer2 = string.Format("ns2.example{0}.net", citizienRepeatCode);
            string citizienMail = string.Format("info@example{0}.net", citizienRepeatCode);

            string organizationRepeatCode = RandomStringGenerator.GenerateLowwerCase(5, 5);
            string organizationDomain = string.Format("example{0}.org.tr", organizationRepeatCode);
            string organizationNameServer1 = string.Format("ns1.example{0}.com", organizationRepeatCode);
            string organizationNameServer2 = string.Format("ns2.example{0}.com", organizationRepeatCode);
            string organizationMail = string.Format("info@example{0}.biz", organizationRepeatCode);

            #endregion Definitions

            /*
             - 1. Hello Method, 

                  Sistemin ayakta olduğunu öğrenmek için kullanılır.

                  Used to find out if the system is up.

             */

            Console.WriteLine("TEST : 1 - Hello");
            try
            {

                HelloRequest request = new HelloRequest() { IsTestActive = true };
                HelloResponse response = null;

                HttpResponseMessage webResponse = await ote1Client.PostAsync("api/hello", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();

                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<HelloResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();

            }


            /*
             - 2. Check Availability Method

                  Toplu “Alan Adı(alt alan adı uzantısı ile birlikte)” durum ve uygunluk bilgisini sorgulamak için
                  kullanılan servistir. Sorgu sonucunda Alan Adları durum bilgilerinin yer aldığı liste dönülür.

                  Bulk "Domain Name (with subdomain tld)" is the service used to query status and eligibility information. 
                  As a result of the query, a list of Domain Names status information is returned.
             */
            Console.WriteLine("TEST : 2 - Check Availability");
            try
            {

                CheckAvailabilityRequest request = new CheckAvailabilityRequest()
                {
                    DomainList = new List<string>()
                    {
                        "one1.com.tr",
                        ",two2.net.tr",
                        "three3.web.tr",
                        "four4.org.tr",
                        "five5.k12.tr"
                    },
                    IsTestActive = true
                };

                CheckAvailabilityResponse response = null;

                HttpResponseMessage webResponse = await ote1Client.PostAsync("api/checkavailability", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();

                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<CheckAvailabilityResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();
            }

            /*
             - 3. Register Citizien Number Method

                  Yeni “Alan Adı Sahibi” bilgileri ile, “Alan Adı Başvurusu” yapmak için kullanılan servistir
                  İlk olarak bireysel kimlik numarası  ile başvuru yapılır. 

            	  It is the service used to make a "Domain Name Application" with the new "Registrant" information.
		          First, an application is made with the individual identification number. 
             */
            Console.WriteLine("TEST : 3 - Register Citizien Number");

            try
            {

                RegisterRequest request = new RegisterRequest()
                {
                    DomainName = citizienDomain,
                    NameServers = new List<NameServer>() {
                         new NameServer(){ Name =  citizienNameServer1},// Ns definition
                         new NameServer(){ Name =  citizienNameServer2}// Ns definition
                    },
                    Period = 1,
                    IsTestActive = true,
                    DomainCategory = DomainCategory.NAMESURNAMECATEGORY,
                    RegContacts = new RegContacts()
                    {
                        Admin = new ContactInfo() { Email = citizienMail, Name = "John DOE" },
                        Billing = new ContactInfo() { Email = citizienMail, Name = "John DOE" },
                        Tech = new ContactInfo() { Email = citizienMail, Name = "John DOE" }

                    },
                    OwnerContact = new OwnerContact()
                    {
                        CountryId = 888, // Unknown Country
                        CityId = 999, //  Out Of Turkey
                        Email = citizienMail,
                        Phone = new PhoneInfo() { CountryCode = "01", AreaCode = "321", Number = "1987654" },
                        Fax = new PhoneInfo() { CountryCode = "01", AreaCode = "123", Number = "4567891" },
                        WWW = "example.com",
                        CitizenId = "00000000000",
                        Name = "John DOE",
                        Address1 = "Yenişehir Mah. Arda Sk. No:36/1",
                        Address2 = "Izmit - Kocaeli - Türkiye"
                    }

                };
                RegisterResponse response = null;

                HttpResponseMessage webResponse = await ote1Client.PostAsync("api/register", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();

                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<RegisterResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();
            }


            /*
             - 4. Register Organization Method

                  Yeni “Alan Adı Sahibi” bilgileri ile, “Alan Adı Başvurusu” yapmak için kullanılan servistir
                  Firma ünvanı vergi dairesi ve vergi numarası ile başvuru yapılır. 

            	  It is the service used to make a "Domain Name Application" with the new "Domain Name Owner" information.
		          Application is made with company title, tax office and tax number. 
             */
            Console.WriteLine("TEST : 4 - Register Organization");

            try
            {

                RegisterRequest request = new RegisterRequest()
                {
                    DomainName = organizationDomain,
                    NameServers = new List<NameServer>() {
                         new NameServer(){ Name =  organizationNameServer1},// Ns definition
                         new NameServer(){ Name =  organizationNameServer2}// Ns definition
                    },
                    Period = 1,
                    IsTestActive = true,
                    DomainCategory = DomainCategory.DEFAULTCATEGORY,
                    RegContacts = new RegContacts()
                    {
                        Admin = new ContactInfo() { Email = organizationMail, Name = "John DOE" },
                        Billing = new ContactInfo() { Email = organizationMail, Name = "John DOE" },
                        Tech = new ContactInfo() { Email = organizationMail, Name = "John DOE" }

                    },
                    OwnerContact = new OwnerContact()
                    {
                        CountryId = 888, // Unknown Country
                        CityId = 999, //  Out Of Turkey
                        Email = citizienMail,
                        Phone = new PhoneInfo() { CountryCode = "01", AreaCode = "321", Number = "1987654" },
                        Fax = new PhoneInfo() { CountryCode = "01", AreaCode = "123", Number = "4567891" },
                        WWW = "example.net",
                        ZipCode = "985623",
                        TaxOffice = "Gebesler",
                        TaxNumber = "6850520554",
                        Organization = "BREAK POINT LTD.",
                        Name = "John DOE",
                        Address1 = "Yenişehir Mah. Arda Sk. No:36/1",
                        Address2 = "Izmit - Kocaeli - Türkiye"
                    }

                };
                RegisterResponse response = null;

                HttpResponseMessage webResponse = await ote1Client.PostAsync("api/register", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();

                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<RegisterResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();
            }


            /*
             - 5. Get Domain Method

                  Domain tüm bilgilerine erişim için kullanılır.

                  Used to access all domain information.
             */
            Console.WriteLine("TEST : 5 - Get Domain");
            try
            {

                GetDomainRequest request = new GetDomainRequest() { IsTestActive = false, DomainName = organizationDomain };
                GetDomainResponse response = null;

                HttpResponseMessage webResponse = await ote1Client.PostAsync("api/getdomain", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();

                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<GetDomainResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();

            }



            /*
             - 6. Update Nameservers Method

                  Alan Adının “Alan Adı Sunucu” bilgilerini güncellemek için kullanılan servistir. Tanımlanan zamana
                  göre, “Alan Adı Sunucu” güncellemeleri sistem tarafından gerçekleştirilir.

                  It is the service used to update the "Domain Name Server" information of the Domain Name. 
                  Defined time Accordingly, "Domain Name Server" updates are performed by the system.
             */
            Console.WriteLine("TEST : 6 - Update nameservers");
            try
            {

                UpdateNameServersRequest request = new UpdateNameServersRequest()
                {
                    IsTestActive = false,
                    DomainName = citizienDomain,

                    CurrentNameServers = new List<NameServer>()
                    {
                        new NameServer() {Name =citizienNameServer1}
                        , new NameServer() {Name =citizienNameServer2}
                    }
                    ,
                    NewNameServers = new List<NameServer>()
                    {
                        new NameServer() {Name ="ns1.example.xyz"}
                        , new NameServer() {Name ="ns1.example.xyz"}
                    }
                };

                UpdateNameServersResponse response = null;


                HttpResponseMessage webResponse = await ote1Client.PostAsync("api/updatenameserver", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();


                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<UpdateNameServersResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();

            }



            /*
             - 7. Disable Transfer Lock Method

                  Müşterinin talep etmesi durumunda, Verici Kayıt Kuruluşu tarafından, Alan adı Transfer Kilidini
                  Kaldırmak için kullanılan servistir.

                  This is the service used by the Issuing Registrar to Unlock the Domain name, if requested by the customer.
             */
            Console.WriteLine("TEST : 7 - Disable Transfer Lock");
            try
            {

                DisableTransferLockRequest request = new DisableTransferLockRequest() { IsTestActive = true, DomainName = organizationDomain };
                DisableTransferLockResponse response = null;

                HttpResponseMessage webResponse = await ote1Client.PostAsync("api/disabletransferlock", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();


                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<DisableTransferLockResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();

            }



            /*
             - 8. Enable Transfer Lock Method
                  
                  Müşterinin talep etmesi durumunda, Verici Kayıt Kuruluşu tarafından, Alan adı Transfer Kilidi Koymak
                  için kullanılan servistir.

                  It is the service used by the Issuing Registrar to Set the Domain Name Transfer Lock, if requested by the Customer.
             */
            Console.WriteLine("TEST : 8 - Enable Transfer Lock");
            try
            {

                EnableTransferLockRequest request = new EnableTransferLockRequest() { IsTestActive = true, DomainName = organizationDomain };
                EnableTransferLockResponse response = null;

                HttpResponseMessage webResponse = await ote1Client.PostAsync("api/enabletransferlock", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();

                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<EnableTransferLockResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();

            }


            /*
             - 9. Modify Privacy Method
                  “Alan Adı Sahibi gizlilik ayarları”nı güncellemek için kullanılan servistir. 
                  Whois ayarlarınızı gizlemek veya görüntülemek için kullanılır.

                  This is the service used to update "Registrant privacy settings". 
                  It is used to hide or display your Whois settings.
             */
            Console.WriteLine("TEST : 9 - Modify Privacy");
            try
            {

                ModifyPrivacyRequest request = new ModifyPrivacyRequest()
                {
                    IsTestActive = true,
                    DomainName = citizienDomain,
                    AddressHidden = false,
                    EmailHidden = false,
                    FaxHidden = false,
                    NameHidden = false,
                    PhoneHidden = false
                };

                ModifyPrivacyResponse response = null;

                HttpResponseMessage webResponse = await ote1Client.PostAsync("api/modifyprivacy", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();

                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<ModifyPrivacyResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();

            }



            /*
             - 10. Update Contact Method

                   Alan Adı Kayıt Sorumlusu bilgilerini güncellemek için kullanılan servistir. Alan Adı “İdari Sorumlu”,
                   “Ödeme Sorumlusu” ve “Teknik Sorumlu” bilgilerini güncellemek için bu servis kullanılmalıdır.

                   This is the service used to update the Domain Name Registrar information. Domain Name "Administrative Responsible", 
                   This service must be used to update the "Payment Responsible" and "Technical Responsible" information.
             */
            Console.WriteLine("TEST : 10 - Update Contact");
            try
            {

                UpdateContactRequest request = new UpdateContactRequest()
                {
                    DomainName = citizienDomain,
                    isOwnerChange = true,
                    IsTestActive = true,
                    OwnerContact = new OwnerContact()
                    {
                        CityId = 41,
                        CountryId = 215,
                        Email = "info@examle.xyz",
                        Phone = new PhoneInfo() { CountryCode = "90", AreaCode = "262", Number = "6547893" },
                        Fax = new PhoneInfo() { CountryCode = "90", AreaCode = "262", Number = "3254587" },
                        ZipCode = "410000",
                        WWW = "necati.com.tr",
                        Name = "John WICK",
                        CitizenId = "111111111111",
                        Address1 = "Kocaeli Üniversitesi Teknopark A.S. Vatan Cd.",
                        Address2 = "No:83 41275 - Başiskele, Kocaeli - TURKEY"

                    },
                    RegContacts = new RegContacts()
                    {
                        Admin = new ContactInfo() { Email = "info@examle.xyz", Name = "John WICK" },
                        Billing = new ContactInfo() { Email = "info@examle.xyz", Name = "John WICK" },
                        Tech = new ContactInfo() { Email = "info@examle.xyz", Name = "John WICK" }
                    }
                };
                UpdateContactResponse response = null;

                HttpResponseMessage webResponse = await ote1Client.PostAsync("api/updatecontact", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();

                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<UpdateContactResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();

            }



            /*
             - 11. Renew Method

                   Süresi biten alan adlarının yenilemek için kullanılan servistir.

                   It is the service used to renew expired domain names.
             */
            Console.WriteLine("TEST : 11 - Renew");
            try
            {

                RenewRequest request = new RenewRequest() { IsTestActive = true, DomainName = organizationDomain, Period = 1 };
                RenewResponse response = null;

                HttpResponseMessage webResponse = await ote1Client.PostAsync("api/renew", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();


                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<RenewResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();

            }



            /*
             - 12. Get Auth Code Method

                   Verici Kayıt Kuruluşu tarafından, Alan adının TRABİS veritabanındaki transfer yetki kodunun
                   sorgulandığı servistir.

                   It is the service where the transfer authorization code of the Domain name in the TRABIS database is queried by the Issuing Registrar.
             */
            Console.WriteLine("TEST : 12 - Get auth code");
            try
            {

                GetAuthCodeRequest request = new GetAuthCodeRequest() { IsTestActive = true, DomainName = organizationDomain };
                GetAuthCodeResponse response = null;

                HttpResponseMessage webResponse = await ote1Client.PostAsync("api/getauthcode", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();

                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<GetAuthCodeResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();

            }



            /*
             - 13. Add Host Method

                   Domain üzerinde yeni private nameserver bilgisi, host kaydı açmak için kullanılır.

                   The new private nameserver information on the domain is used to open the host record.
            */
            Console.WriteLine("TEST : 13 - Add Host");
            try
            {

                AddHostRequest request = new AddHostRequest()
                {
                    IsTestActive = true,
                    DomainName = organizationDomain,
                    Host = new Host()
                    {
                        IpAddressV4 = "1.1.1.1",
                        Name = "ns1." + organizationDomain
                    }
                };
                AddHostResponse response = null;

                HttpResponseMessage webResponse = await ote1Client.PostAsync("api/addhost", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();

                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<AddHostResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();

            }




            /*
             - 14. Get Host Method

                   Domain üzerinde tanımlanan private nameserver bilgisi, host kaydını belirtir. Tanımladığınız host kaydı görüntülenir.

                   The private nameserver information defined on the domain specifies the host record. The host record you defined is displayed.
             */
            Console.WriteLine("TEST : 14 - Get Host");
            try
            {

                GetHostRequest request = new GetHostRequest() { IsTestActive = true, HostName = "ns1." + organizationDomain };
                GetHostResponse response = null;

                HttpResponseMessage webResponse = await ote1Client.PostAsync("api/gethost", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();


                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<GetHostResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();

            }




            /*
             - 15. Add Host Method

                   Domain üzerinde yeni private nameserver bilgisi, host kaydı açmak için kullanılır.

                   It is used to open new private nameserver information, host registration on the domain.
            */
            Console.WriteLine("TEST : 15 - Add Host");
            try
            {

                AddHostRequest request = new AddHostRequest()
                {
                    IsTestActive = true,
                    DomainName = organizationDomain,
                    Host = new Host()
                    {
                        IpAddressV4 = "3.3.2.1",
                        Name = "ns2." + organizationDomain
                    }
                };
                AddHostResponse response = null;

                HttpResponseMessage webResponse = await ote1Client.PostAsync("api/addhost", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();

                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<AddHostResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();

            }




            /*
             - 16. Host Update Method

                   Host bilgisini günceller.

                   Updates the host information.
             */
            Console.WriteLine("TEST : 16 - Update Host");
            try
            {
                UpdateHostRequest request = new UpdateHostRequest()
                {
                    IsTestActive = true,
                    DomainName = organizationDomain,
                    Host = new Host()
                    {
                        IpAddressV4 = "2.2.2.1",
                        Name = "ns2." + organizationDomain
                    }
                };
                UpdateHostResponse response = null;

                HttpResponseMessage webResponse = await ote1Client.PostAsync("api/updatehost", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();

                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<UpdateHostResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();

            }





            /*
             - 17. Get Host List Method

                   Sisteme kayıtlı host bilgisini getirir.

                   Gets the host information registered in the system.
             */
            Console.WriteLine("TEST : 17 - Get Host List");
            try
            {

                GetHostListRequest request = new GetHostListRequest() { IsTestActive = false, DomainName = organizationDomain };
                GetHostListResponse response = null;

                HttpResponseMessage webResponse = await ote1Client.PostAsync("api/gethostlist", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();

                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<GetHostListResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();

            }


            #region TEST SENARYOSUNA GÖRE HAZIRLANAN DOMAİNLERİN BİLGİLERİ DEĞİŞEBİLİYOR ÖZELLİKLE TRANSFER REDDİ OLDUĞUNDA KİLİTLİYOR VE AUTH CODE DEĞİŞİYOR

            string transferedDomain = "rh116202301261234k066079.biz.tr";
            string authCode = "";

            try
            {
                DisableTransferLockRequest __request = new DisableTransferLockRequest() { IsTestActive = true, DomainName = transferedDomain };

                HttpResponseMessage __webResponse = await ote1Client.PostAsync("api/disabletransferlock", new StringContent(JsonConvert.SerializeObject(__request), Encoding.UTF8, "application/json"));

                if (__webResponse.IsSuccessStatusCode)
                {
                    // Transfer kilidini devre dışı bırak.
                }

                GetAuthCodeRequest request = new GetAuthCodeRequest() { IsTestActive = true, DomainName = transferedDomain };
                GetAuthCodeResponse response = null;

                HttpResponseMessage webResponse = await ote1Client.PostAsync("api/getauthcode", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();

                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<GetAuthCodeResponse>(content);

                    authCode = response.AuthCode;

                    // Auth code eşitleniyor.

                }
            }
            catch (Exception)
            {


            }
            #endregion TEST SENARYOSUNA GÖRE HAZIRLANAN DOMAİNLERİN BİLGİLERİ DEĞİŞEBİLİYOR ÖZELLİKLE TRANSFER REDDİ OLDUĞUNDA KİLİTLİYOR VE AUTH CODE DEĞİŞİYOR


            /*
             - 18. Transfer Method

                  Alıcı Kayıt Kuruluşu tarafından, Alan adı “Transfer Talebi”’ni başlatmak için kullanılan servistir.

                  It is the service used by the Buyer Registrar to initiate a Domain Name "Transfer Request".
             */
            Console.WriteLine("TEST : 16 - Transfer");

            try
            {
                TransferRequest request = new TransferRequest()
                {
                    IsTestActive = true,
                    DomainName = transferedDomain,
                    AuthCode = authCode,
                    RegContacts = new RegContacts()
                    {
                        Admin = new ContactInfo() { Email = "info@example.org.tr", Name = "Keanu Reeves" },
                        Billing = new ContactInfo() { Email = "info@example.org.tr", Name = "Keanu Reeves" },
                        Tech = new ContactInfo() { Email = "info@example.org.tr", Name = "Keanu Reeves" }

                    }
                };
                TransferResponse response = null;

                HttpResponseMessage webResponse = await ote2Client.PostAsync("api/transfer", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();

                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<TransferResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();

            }




            /*
             - 19. Cancel Transfer Method

                  Alıcı Kayıt Kuruluşu tarafından, Daha önce başlatmış olduğu, “Transfer Talebi”nin İptal edilebileceği servistir.
                 
                 This is the service where the "Transfer Request" previously initiated by the Buyer Registry can be canceled.
            */
            Console.WriteLine("TEST : 19 - Cancel Transfer");
            try
            {

                CancelTransferRequest request = new CancelTransferRequest() { IsTestActive = true, DomainName = transferedDomain };
                CancelTransferResponse response = null;

                HttpResponseMessage webResponse = await ote2Client.PostAsync("api/canceltransfer", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();

                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<CancelTransferResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();

            }




            /*
             - 20. Transfer Method

                  Alıcı Kayıt Kuruluşu tarafından, Alan adı “Transfer Talebi”’ni başlatmak için kullanılan servistir.

                  It is the service used by the Buyer Registrar to initiate a Domain Name "Transfer Request".
             */
            Console.WriteLine("TEST : 20 - Transfer");

            try
            {

                TransferRequest request = new TransferRequest()
                {
                    IsTestActive = true,
                    DomainName = transferedDomain,
                    AuthCode = authCode,
                    RegContacts = new RegContacts()
                    {
                        Admin = new ContactInfo() { Email = "info@example.org.tr", Name = "Keanu Reeves" },
                        Billing = new ContactInfo() { Email = "info@example.org.tr", Name = "Keanu Reeves" },
                        Tech = new ContactInfo() { Email = "info@example.org.tr", Name = "Keanu Reeves" }

                    }
                };
                TransferResponse response = null;

                HttpResponseMessage webResponse = await ote2Client.PostAsync("api/transfer", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();

                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<TransferResponse>(content);

                }

                WriteResponse(content, response);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();

            }




            /*
             - 21. Reject Transfer Method

                  Alıcı Kayıt Kuruluşu tarafından, Daha önce başlatmış olduğu, “Transfer Talebi”nin red edilebileceği servistir.

                  This is the service where the "Transfer Request" previously initiated by the Buyer Registry can be rejected.
            */
            Console.WriteLine("TEST : 21 - Reject Transfer");

            try
            {
                RejectTransferRequest request = new RejectTransferRequest()
                {
                    IsTestActive = true,
                    DomainName = transferedDomain,
                    Reason = "test"
                };
                RejectTransferResponse response = null;

                HttpResponseMessage webResponse = await ote1Client.PostAsync("api/rejecttransfer", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();

                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<RejectTransferResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();

            }


            /*
             - 22. Check Transfer Method

                  Domain transfer durumunun sorgulandığı servistir. Transfer edilebilir mi ? edilemez mi ?

                  It is the service where the domain transfer status is queried. Can it be transferred or not?
            */
            Console.WriteLine("TEST : 22 - Check Transfer");
            try
            {

                CheckTransferRequest request = new CheckTransferRequest() { IsTestActive = true, DomainName = transferedDomain };
                CheckTransferResponse response = null;

                HttpResponseMessage webResponse = await ote2Client.PostAsync("api/checktransfer", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();

                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<CheckTransferResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();

            }


            /*
             - 22. Delete Method

                  Domaini kayıt veri tabanından siler.

                  Delete domain from the registration database.
            */
            Console.WriteLine("TEST : 22 - Delete");
            try
            {

                DeleteRequest request = new DeleteRequest() { IsTestActive = true, DomainName = organizationDomain };
                DeleteResponse response = null;

                HttpResponseMessage webResponse = await ote1Client.PostAsync("api/delete", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (webResponse.IsSuccessStatusCode)
                {
                    // string content
                    content = await webResponse.Content.ReadAsStringAsync();


                    // convert to newtonsoft json
                    response = JsonConvert.DeserializeObject<DeleteResponse>(content);

                }

                WriteResponse(content, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading test data list.. Exception is : " + ex.Message + Environment.NewLine);
            }
            finally
            {
                content = "";
                Console.WriteLine();

            }


            Console.ReadLine();
        }

        private static async void WriteResponse(string content, BaseResponse response)
        {
            if (response == null)
            {
                Console.WriteLine("Response was null");
                return;
            }

            if (content != null) { Console.WriteLine("Content " + content); }
            if (response.Code <= 0) { Console.WriteLine("Code:" + response.Code); }
            if (response.Message != null) { Console.WriteLine("Message:" + response.Message); }


            Console.WriteLine("** press <Enter> to continue **");
            Thread.Sleep(500);
            Console.ReadLine();
        }
    }
}
