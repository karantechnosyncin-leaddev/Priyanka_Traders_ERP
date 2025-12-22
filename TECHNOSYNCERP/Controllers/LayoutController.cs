using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace TECHNOSYNCERP.Controllers
{
    public class LayoutController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<LayoutController> _logger;

        public LayoutController(ILogger<LayoutController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public IActionResult GenerateLayout(string DocType, string DocEntry, string Size, string Name)
        {
            try
            {
                string filename = Name;
                if (string.IsNullOrEmpty(filename))
                {
                    if (DocType == "SR" || DocType == "SQ" ||DocType=="SO" ||DocType=="SD" ||DocType=="STI" ||DocType=="SCN")
                    {
                        if (Size=="A4")
                        {
                            filename = "SALES LAYOUT A4.rpt";
                        }
                        else
                        {
                            filename = "SALES LAYOUT A5.rpt";
                        }
                    }
                    else if (DocType == "PQ" || DocType == "PO" || DocType == "GRPO" || DocType == "PDN" || DocType == "PGR")
                    {
                        if (Size == "A4")
                        {
                            filename = "PURCHASE PRINT A4.rpt";
                        }
                        else
                        {
                            filename = "PURCHASE PRINT A5.rpt";
                        }
                    }
                    else if(DocType == "REC" || DocType == "PAY"|| DocType == "JE"|| DocType == "ACON"|| DocType == "LOB"|| DocType == "LOB")
                    {
                        filename = "";
                    }
                   
                }
                if (string.IsNullOrEmpty(filename))
                {
                    return BadRequest("Unsoported Print File Or File Not Specified!");
                }
                var requestPayload = new
                {
                    DocEntry = DocEntry,
                    LoadFileName = filename,
                    FileName = $"{DateTime.Now.ToString()}_{DocEntry}_{Size}.pdf",
                    ObjType = DocType
                };

                using (var client = CreateHttpClient())
                {
                    var jsonContent = new StringContent(
                        System.Text.Json.JsonSerializer.Serialize(requestPayload),
                        System.Text.Encoding.UTF8,
                        "application/json"
                    );
                    HttpResponseMessage response;
                    if (DocType == "GR" || DocType == "GI")
                    {
                        response = client.PostAsync("api/Gen_Layout", jsonContent).Result;
                    }
                    else
                    {
                        response = client.PostAsync("api/TechnoErpLayouts", jsonContent).Result;
                    }
                    if (!response.IsSuccessStatusCode)
                    {
                        return StatusCode((int)response.StatusCode, response);
                    }
                    var jsonData = response.Content.ReadAsStringAsync().Result;

                    dynamic apiResponse = JsonConvert.DeserializeObject<dynamic>(jsonData);

                    if (apiResponse.success == false)
                    {
                        string msg = apiResponse.message != null ? apiResponse.message.ToString() : "Unknown error from layout API.";
                        return StatusCode(500, msg);
                    }

                    string base64Pdf = apiResponse.base64Pdf;
                    string fileName = apiResponse.fileName;

                    if (string.IsNullOrEmpty(base64Pdf))
                    {
                        return StatusCode(500, "Layout API returned success but no PDF content.");
                    }

                    // Remove data URL prefix if present
                    string base64Data = base64Pdf.ToString();
                    var base64Only = base64Data.Contains(",") ? base64Data.Split(',')[1] : base64Data;


                    return Json(new { success = true, message = base64Data, fileName });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while generating the layout. Details: {ex.Message}");
            }
        }
        public IActionResult GenerateAssembyLayout(string FromDate, string ToDate, string DocType)
        {
            try
            {
                string filename = string.Empty;

                // Determine report file based on DocType and Size
                switch (DocType)
                {
                    case "AFVD":
                            filename = "Assembly_FirstVisualData_A5.rpt";
                        break;
                    case "AFIVD":
                        filename = "Assembly_FinalVisualData_A5.rpt";
                        break;
                    case "ADPR":
                        filename = "Assembly_DeflashingProcess_A5.rpt";
                        break;
                    case "AIRR":
                        filename = "Assembly_InprogressRejection_A5.rpt";
                        break;
                    default:
                        return BadRequest("Unsupported document type.");
                }
                if (string.IsNullOrEmpty(filename))
                {
                    return BadRequest("Invalid report size specified.");
                }
                var requestPayload = new
                {
                    FromDate = FromDate,
                    ToDate = ToDate,
                    LoadFileName = filename,
                    FileName = $"{DateTime.Now.ToString()}_{FromDate}_{ToDate}.pdf"
                };
                using (var client = CreateHttpClient())
                {
                    var jsonContent = new StringContent(
                        System.Text.Json.JsonSerializer.Serialize(requestPayload),
                        System.Text.Encoding.UTF8,
                        "application/json"
                    );

                    var response = client.PostAsync("api/Assembly", jsonContent).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        return StatusCode((int)response.StatusCode, "Error fetching layout data.");
                    }

                    var jsonData = response.Content.ReadAsStringAsync().Result;

                    dynamic apiResponse = JsonConvert.DeserializeObject<dynamic>(jsonData);

                    if (apiResponse.success == false)
                    {
                        string msg = apiResponse.message != null ? apiResponse.message.ToString() : "Unknown error from layout API.";
                        return StatusCode(500, msg);
                    }

                    string base64Pdf = apiResponse.base64Pdf;
                    string fileName = apiResponse.fileName;

                    if (string.IsNullOrEmpty(base64Pdf))
                    {
                        return StatusCode(500, "Layout API returned success but no PDF content.");
                    }

                    // Remove data URL prefix if present
                    string base64Data = base64Pdf.ToString();
                    var base64Only = base64Data.Contains(",") ? base64Data.Split(',')[1] : base64Data;


                    return Json(new { success = true, message = base64Data, fileName });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while generating the layout. Details: {ex.Message}");
            }
        }
        public async Task<IActionResult> GENRATEMATERIALOUT(string DocType, string DocEntry, string Size)
        {
            try
            {
                string[] templates = ["Inventory_MaterialOut_Original_A4.rpt", "Inventory_MaterialOut_Duplicate_A4.rpt", "Inventory_MaterialOut_Supplier_A4.rpt"];
                var pdfList = new List<object>();
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string generatedFilename = $"{timestamp}_{DocEntry}_{Size}.pdf";

                // Pre-create reusable objects
                var tasks = new List<Task<object>>();
                var httpClient = CreateHttpClient(); // Reuse single HttpClient

                foreach (var template in templates)
                {
                    tasks.Add(ProcessTemplateAsync(httpClient, template, DocEntry, Size, timestamp));
                }

                // Process all templates concurrently
                var results = await Task.WhenAll(tasks);
                pdfList.AddRange(results);

                // Dispose HttpClient properly
                httpClient.Dispose();

                return Json(new { success = true, data = pdfList, filename = generatedFilename });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while generating the layout. Details: {ex.Message}");
            }
        }
        private async Task<object> ProcessTemplateAsync(HttpClient client, string template, string docEntry, string size, string timestamp)
        {
            var requestPayload = new
            {
                DocEntry = docEntry,
                LoadFileName = template,
                FileName = $"{timestamp}_{docEntry}_{size}.pdf"
            };

            using var jsonContent = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(requestPayload),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("api/Gen_Layout", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error fetching layout data. Status: {response.StatusCode}");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            dynamic apiResponse = JsonConvert.DeserializeObject<dynamic>(jsonData);

            if (apiResponse.success == false)
            {
                string msg = apiResponse.message != null ? apiResponse.message.ToString() : "Unknown error from layout API.";
                throw new Exception(msg);
            }

            string base64Pdf = apiResponse.base64Pdf;
            string fileName = apiResponse.fileName;

            if (string.IsNullOrEmpty(base64Pdf))
            {
                throw new Exception("Layout API returned success but no PDF content.");
            }

            // Remove data URL prefix if present
            string base64Only = base64Pdf.Contains(",") ? base64Pdf.Split(',')[1] : base64Pdf;

            return new
            {
                base64Pdf = base64Only,
                fileName = fileName
            };
        }
        public HttpClient CreateHttpClient()
        {
            var baseUrl = _configuration.GetConnectionString("LayoutApiUrl");

            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new ArgumentNullException("LayoutApiUrl", "The LayoutApiUrl is not configured in appsettings.json or environment variables.");
            }

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUrl)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }


        [HttpGet]
        public async Task<IActionResult> GETStockCheck(string itemId)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = "EXEC GET_ITEMSTOCK @ItemID";

                await using var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ItemID", itemId);

                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var obj = new Dictionary<string, string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        obj[reader.GetName(i)] = reader.GetValue(i)?.ToString();
                    }
                    list.Add(obj);
                }

                return Json(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }






    }
}