using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onirim
{
    public class CardProperties
    {
        public CardTypeEnum type;
        public int count;

        public CardProperties(CardTypeEnum type, int count)
        {
            this.type = type;
            this.count = count;
        }

        public CardProperties()
        {

        }

        public CardTypeEnum Type
        {
            get { return this.type; }
        }

        public int Count
        {
            get { return this.count; }
        }
    }
}
