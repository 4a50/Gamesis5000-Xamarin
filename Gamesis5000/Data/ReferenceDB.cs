using System;
using System.IO;

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gamesis5000.Data
{
  public class ReferenceDB
  {
    public ReferenceDB()
    {

    }
    public async Task<string> SampleFileRead()
    {
      //Local File.  Do Not use when accessing actual API
      string textOutput;
      using (var stream = await FileSystem.OpenAppPackageFileAsync(localFileName))
      {
        using (var reader = new StreamReader(stream))
        {
          textOutput = reader.ReadToEnd();
        }
      }
    }
}
