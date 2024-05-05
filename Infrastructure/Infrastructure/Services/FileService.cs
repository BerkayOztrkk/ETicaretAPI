
using ETicaretAPI.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services
{
    public class FileService 
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment=webHostEnvironment;
        }

        

         async Task<string> FileRenameAsync(string path,string fileName,bool first=true)
        {
           string newFileName= await Task.Run<string>(async () =>
              {
                  string extension = Path.GetExtension(path);
              string newFileName=string.Empty;
                  if (first)
                  {
                      string oldName = Path.GetFileNameWithoutExtension(fileName);
                       newFileName = $"{NameService.CharacterRegulatory(oldName)}{extension}";
                  }
                  else
                  {
                      newFileName=fileName;
                      int indexNo1= newFileName.IndexOf('-');
                      if (indexNo1==-1)
                      {
                          newFileName=$"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                          
                      }
                      else
                      {
                          int lastindex = 0;
                          while (true)
                          {
                              lastindex=indexNo1;
                              lastindex=newFileName.IndexOf("-", indexNo1+1);
                              if (indexNo1==-1)
                              {
                                  indexNo1 = lastindex;
                                  break;
                              }

                          }

                          int indexNo2= newFileName.IndexOf(".");
                          string fileNo= newFileName.Substring(indexNo1+1,indexNo2-indexNo1-1);
                          if (int.TryParse(fileNo,out int _fileno))
                          {
                              _fileno++;
                              newFileName=newFileName.Remove(indexNo1+1, indexNo2-indexNo1-1)
                                               .Insert(indexNo1+1, _fileno.ToString());
                          }
                          else
                          {
                              newFileName=$"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                          }
                       
                        
                      }
                  }


                  if (File.Exists($"{path}\\{newFileName}"))
                   return   await FileRenameAsync(path, newFileName, false);
                  else
                      return newFileName;
              });
            return newFileName;
        }

       
    }
}
