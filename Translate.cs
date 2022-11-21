 static string Translate(string Text, string Origine, string Destination)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.reverso.net/translate/v1/translation");
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    streamWriter.Write(new JavaScriptSerializer().Serialize(new Dictionary<string, object>() {
                            {"format","text" },
                            {"from",Origine },
                            {"to", Destination},
                            {"input", Text},
                            {"options", new Dictionary<string, object>(){
                                {"sentenceSplitter",false },
                                { "origin","translation.web"},
                                { "contextResults",false},
                                { "languageDetection",false}
                                } 
                            }
                    }));
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                    Dictionary<string, object> Data = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(streamReader.ReadToEnd());
                    return ((ArrayList)Data["translation"])[0].ToString();
                }
                   
            } catch (Exception e) {
                Console.WriteLine(e.Message);

            }
            return String.Empty;
        }
