using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ProjetoApi.Services
{
    public static class FotoService
    {
        public async static Task<string> UploadFoto(IWebHostEnvironment environment, IFormFile arquivo)
        {
            if (arquivo.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(environment.WebRootPath + "\\imagens\\"))
                    {
                        Directory.CreateDirectory(environment.WebRootPath + "\\imagens\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(environment.WebRootPath + "\\imagens\\" + arquivo.Name + arquivo.FileName))
                    {
                        await arquivo.CopyToAsync(filestream);
                        filestream.Flush();
                        return "\\imagens\\"+ arquivo.Name + arquivo.FileName;
                    }
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
            {
                return "Ocorreu uma falha no envio do arquivo...";
            }
        }

        //Conversor da base64 para IFormFile
        public static IFormFile Base64ToImage(string foto, string nome)
        {
            byte[] bytes = Convert.FromBase64String(foto);
            MemoryStream stream = new MemoryStream(bytes);
            string extensao = GetFileExtension(foto);

            IFormFile file = new FormFile(stream, 0, bytes.Length, nome, extensao);

            return file;
        }

        //Obter a extensão do arquivo base64
        public static string GetFileExtension(string base64String)
        {
            var data = base64String.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    return ".png";
                case "/9J/4":
                    return ".jpg";
                case "AAAAF":
                    return ".mp4";
                case "JVBER":
                    return ".pdf";
                case "AAABA":
                    return ".ico";
                case "UMFYI":
                    return ".rar";
                case "E1XYD":
                    return ".rtf";
                case "U1PKC":
                    return ".txt";
                case "MQOWM":
                case "77U/M":
                    return ".srt";
                default:
                    return string.Empty;
            }
        }


                // if (foto.Length > 0)
        // {
        //     try
        //     {
        //         if (!Directory.Exists(environment.WebRootPath + "\\imagens\\"))
        //         {
        //             Directory.CreateDirectory(environment.WebRootPath + "\\imagens\\");
        //         }

        //         string pastaUpload = Path.Combine(environment.WebRootPath, "FotosPoliticos");
        //         string extensao = Path.GetExtension(foto.FileName);
        //         string nomeImagem = foto.Name + extensao;
        //         string caminhoImagem = Path.Combine(pastaUpload, nomeImagem);

        //         using (var fileStream = new FileStream(caminhoImagem, FileMode.Create))
        //         {
        //             foto.CopyTo(fileStream);
        //         }

        //         return foto.Name;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception(ex.Message);
        //     }
        // }
        // else
        // {
        //     throw new Exception("Ocorreu uma falha no envio da foto.");
        // }
    }
}