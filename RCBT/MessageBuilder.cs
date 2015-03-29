using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCBT
{
    public class MessageBuilder
    {
        private String messageBeginMarker;
        private String messageEndMarker;
        private String bufferedData;
        public MessageBuilder(String messageBeginMarker, String messageEndMarker)
        {
            if (messageBeginMarker == null)
            {
                throw new ArgumentNullException("messageBeginMarker");
            }

            if (messageEndMarker == null)
            {
                throw new ArgumentNullException("messageEndMarker");
            }

            this.messageBeginMarker = messageBeginMarker;
            this.messageEndMarker = messageEndMarker;

            bufferedData = "";
        }
        public void Append(String data)
        {
            if (data != null)
            {
                bufferedData += data;
            }
        }
        public String FindAndRemoveNextMessage()
        {
            int beginIndex = bufferedData.IndexOf(messageBeginMarker);
            if (beginIndex != -1)
            {
                int endIndex = bufferedData.IndexOf(messageEndMarker, beginIndex);
                if (endIndex != -1)
                {
                    String foundMessage = bufferedData.Substring(
                        beginIndex, (endIndex - beginIndex) + 1);
                    bufferedData = bufferedData.Substring(endIndex + 1);
                    return foundMessage;
                }
            }
            return null;
        }
        public void Clear()
        {
            bufferedData = "";
        }
    }
}
