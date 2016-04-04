using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace DownloadRom
{
    class RomServer
    {
        const int PORT_NUM = 6552;
        RomDownloader downloader;

        public RomServer(RomDownloader sender)
        {
            downloader = sender;
        }

        public void startServer()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback,PORT_NUM);
            listener.Start();
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream dataStream = client.GetStream();
            byte[] bytes = new byte[512];
            int numLeftToRead;
            string clientMessage = "";
            while ((numLeftToRead = dataStream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Translate data bytes to a ASCII string.
                clientMessage += System.Text.Encoding.ASCII.GetString(bytes, 0, numLeftToRead);
            }
        }

        public void processMessage(string messageToRead, NetworkStream dataStream)
        {
            string[] splits = new string[1];
            splits[0] = "<>";
            string[] partsOFMessage = messageToRead.Split(splits,StringSplitOptions.RemoveEmptyEntries);
        }

        public void processPart(string partToProcess, NetworkStream dataStream)
        {
            byte[] response = null;
            if(partToProcess == "PING")
            {
                response = System.Text.ASCIIEncoding.ASCII.GetBytes("PING");
            }
            if(partToProcess.Contains("DOWNLOAD SEARCH:"))
            {
                int startPos = partToProcess.IndexOf("DOWNLOAD SEARCH:") + 16;
                processDownloadSearch(partToProcess.Substring(startPos), dataStream);
            }
            if(partToProcess.Contains("GAME SEARCH:"))
            {
                int startPos = partToProcess.IndexOf("GAME SEARCH:") + 12;
            }
            if(response != null)
            {
                dataStream.Write(response, 0, response.Length);
            }
        }

        public void processDownloadSearch(string searchTerms, NetworkStream dataStream)
        {
            BinaryFormatter searlizer = new BinaryFormatter();
            List<romList> returnList = downloader.queryAllSites();
            searlizer.Serialize(dataStream, returnList);
        }

        public void processGameSearch(string searchTerms, NetworkStream dataStream)
        {
            List<romList> returnList = databaseHelper.queryRoms(searchTerms);
            BinaryFormatter searlizer = new BinaryFormatter();
            searlizer.Serialize(dataStream, returnList);    
        }
    }
}
