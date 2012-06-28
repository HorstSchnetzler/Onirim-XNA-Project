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

        public const double cardShrink=0.5;

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

    public enum CardTypeEnum
    {
        REDDOOR,
        REDKEY,
        REDSUN,
        REDMOON,
        BLUEDOOR,
        BLUEKEY,
        BLUESUN,
        BLUEMOON,
        GREENDOOR,
        GREENKEY,
        GREENSUN,
        GREENMOON,
        ORANGEDOOR,
        ORANGEKEY,
        ORANGESUN,
        ORANGEMOON,
        NIGHTMARE
    }

    public enum CardTypeClassEnum
    {
        DOOR,
        KEY,
        MOON,
        SUN,
        NIGHTMARE
    }

    public enum CardTypeColorEnum
    {
        RED,
        GREEN,
        ORANGE,
        BLUE
    }
}
